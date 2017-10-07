using System;

namespace Timer.Abstractions
{
    public abstract class WriterCommand:ICommand
    {
        public DateTime TimeStamp { get; set; }
        public string Description { get; set; }
        public string TicketId { get; set; }
    }
}
