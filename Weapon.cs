using Newtonsoft.Json;

namespace Frosthold
{
    public enum WeaponType
    {
        Ranged,
        Melee
    }

    public class Weapon : Item
    {
        private static readonly Random rand = new Random();

        public int Range { get; set; }
        public int Damage { get; set; }
        public WeaponType WeaponType;

        [JsonIgnore]
        private GameController gc = GameController.Instance;

        [JsonConstructor]
        public Weapon(string name, string description, int damage, int range, string mark, WeaponType weapontype) : base(name, description, 1, 1, mark, () => GameController.Instance.messageLog.AddMessage($"Cannot use {name}"), new Position(1, 1), ConsoleColor.DarkRed)
        {
            Name = name;
            Description = description;
            Damage = damage;
            WeaponType = weapontype;
        }

        public Weapon(string name, string description, int damage, int range, string mark, Position pos, WeaponType weapontype) : base(name, description, 1, 1, mark, () => GameController.Instance.messageLog.AddMessage($"Cannot use {name}"), pos, ConsoleColor.DarkRed)
        {
            Name = name;
            Description = description;
            Damage = damage;
            WeaponType = weapontype;
        }

        public override void Attack(Entity e)
        {
            switch (WeaponType)
            {
                case WeaponType.Melee:
                    MeleeAttack(e);
                    break;

                case WeaponType.Ranged:
                    RangedAttack();
                    break;

                default:
                    break;
            }
        }

        public override void Attack(Player p, Entity attacker)
        {
            if (rand.Next(0, 100) < 50)
            {
                gc.messageLog.AddMessage($"{Screen.redColor}{attacker.name} hits you for {Damage} Damage with {name}{Screen.resetColor}");
                gc.player.TakeDamage(Damage);
                return;
            }
            else
            {
                gc.messageLog.AddMessage($"{Screen.redColor}{attacker.name} misses you with {name}{Screen.resetColor}.");
            }
        }

        private void RangedAttack()
        {
            throw new NotImplementedException();
        }

        private void MeleeAttack(Entity e)
        {
            var en = (Monster)e;
            var hitChance = 50 + (gc.player.Dexterity - 10) * 5;

            if (rand.Next(0, 100) < hitChance)
            {
                en.TakeDamage(gc.player.Strength + Damage);
                gc.messageLog.AddMessage($"{Screen.greenColor}you hit {en.name} for {gc.player.Strength + Damage} it has {en.Health} hp left{Screen.resetColor}");
            }
            else
            {
                gc.messageLog.AddMessage($"{Screen.greenColor}you miss {en.name}{Screen.resetColor}");
            }
        }

        private void MeleeAttack(Player p)
        {
        }

        public override void UseItem()
        {
            UseAction.Invoke();
        }
    }
}