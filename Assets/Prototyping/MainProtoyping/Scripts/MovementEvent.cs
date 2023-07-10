namespace Prototyping.MainProtoyping.Scripts
{
    public class MovementEvent : Event
    {
        private EventType eventType;

        public MovementEvent(EventType eventType)
        {
            this.eventType = eventType;
        }

        public EventType getEventType()
        {
            return this.eventType;
        }
    }
}