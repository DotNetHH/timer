using System;

namespace Timer.Business.Abstractions
{
    public abstract class WriterCommand:TimerCommand
    {
        public string Description { get; set; }
        public string TicketId { get; set; }
    }
}
