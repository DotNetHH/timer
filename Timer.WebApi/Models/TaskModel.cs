using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timer.WebApi.Models
{
    public class TaskModel
    {
        public DateTime TimeStamp { get; set; }
        public string TicketId { get; set; }
        public string Description { get; set; }
    }
}
