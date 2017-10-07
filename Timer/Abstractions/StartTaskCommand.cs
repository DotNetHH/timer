using System;
using System.Collections.Generic;
using System.Text;

namespace Timer.Abstractions
{
    public class StartTaskCommand
    {
        public DateTime StartTime { get; set; }
        public string Description { get; set; }
        public string TicketId { get; set; }
    }
}
