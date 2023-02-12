using System;
using System.Collections.Generic;

namespace Frosthold
{
    public enum EventType
    {
        EveryXFrames,
        AtFrame
    }

    public class FrameEvent
    {
        public int Frame { get; }
        public Action FrameAction { get; }
        public EventType Type { get; }

        public FrameEvent(int frame, Action frameAction, EventType type)
        {
            Frame = frame;
            FrameAction = frameAction;
            Type = type;
        }
    }

    public class EventSystem
    {
        private List<FrameEvent> Events;

        public EventSystem()
        {
            Events = new List<FrameEvent>();
        }

        public void AddEvent(int frame, Action frameAction, EventType type)
        {
            Events.Add(new FrameEvent(frame, frameAction, type));
        }

        public void Update(int currentFrame)
        {
            foreach (FrameEvent e in Events)
            {
                switch (e.Type)
                {
                    case EventType.EveryXFrames:
                        if (currentFrame % e.Frame == 0)
                        {
                            e.FrameAction.Invoke();
                        }
                        break;

                    case EventType.AtFrame:
                        if (currentFrame == e.Frame)
                        {
                            e.FrameAction.Invoke();
                            // Events.RemoveAt(e.Frame);
                        }
                        break;
                }
            }
        }
    }
}