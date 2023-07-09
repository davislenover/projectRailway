namespace Prototyping.MainProtoyping.Scripts
{
    public class WalkingEvent : Event
    {

        private string eventData;

        public WalkingEvent(string eventData)
        {
            this.eventData = eventData;
        }
        
        public EventType getEventType()
        {
            return EventType.WALKING;
        }

        public string getEventData()
        {
            return this.eventData;
        }
    }
}