using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class TaskModel
    {
        public DateTime DateTimeUtc { get; set; }
        public string Ticket { get; set; }
        public string Description { get; set; }
    }
}
