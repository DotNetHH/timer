using System;

namespace Timer.Data.Abstractions
{
    public class TimerEvent
    {
        public DateTime TimeStamp { get; set; }
        public TimerEventType EventType { get; set; }
        public string Description { get; set; }
        public string TicketId { get; set; }
    }
}
