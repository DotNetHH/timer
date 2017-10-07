using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private Timer.Abstractions.ICommandManager businessLayerCommandManager;

        // GET: api/tasks
        [HttpGet]
        public IEnumerable<TaskModel> Get()
        {
            return new List<TaskModel>()
            {
                new TaskModel () { DateTimeUtc = DateTime.Now }
            };
        }

        // GET api/tasks/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/tasks
        /// <summary>
        /// This method starts a task.
        /// This method accepts JSON object as parameter that contains UTC TimeStamp at least. Further properties may follow.
        /// </summary>
        /// <param name="jsonTask">Task object as JSON, containing UTC Timestamp at least</param>
        [HttpPost]
        public void Start([FromBody] TaskModel task)
        {
            if (task == null)
                throw new ArgumentNullException();

            businessLayerCommandManager.AddCommand(
                new Timer.Abstractions.StartTaskCommand() { TimeStamp = task.DateTimeUtc }
            );
        }

        // POST api/tasks
        /// <summary>
        /// This method stops a task.
        /// This method accepts JSON object as parameter that contains UTC TimeStamp at least. Further properties may follow.
        /// </summary>
        /// <param name="jsonTask">Task object as JSON, containing UTC Timestamp at least</param>
        [HttpPost]
        public void Stop([FromBody] TaskModel task)
        {
            if (task == null)
                throw new ArgumentNullException();

            businessLayerCommandManager.AddCommand(
                new Timer.Abstractions.StopTaskCommand() { TimeStamp = task.DateTimeUtc }
            );
        }

        // POST api/tasks
        /// <summary>
        /// This method interrupts a task. When the interrupt is terminated, the original task continues.
        /// This method accepts JSON object as parameter that contains UTC TimeStamp at least. Further properties may follow.
        /// </summary>
        /// <param name="jsonTask">Task object as JSON, containing UTC Timestamp at least</param>
        [HttpPost]
        public void Interrupt([FromBody] TaskModel task)
        {
            if (task == null)
                throw new ArgumentNullException();

            businessLayerCommandManager.AddCommand(
                new Timer.Abstractions.InterruptCommand() { TimeStamp = task.DateTimeUtc }
            );
        }
    }
}
