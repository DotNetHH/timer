using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using WebApi.Models;

namespace WebApi.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private Timer.Abstractions.ICommandManager businessLayerCommandManager;

        // GET: api/tasks
        [HttpGet]
        public IEnumerable<TaskModel> Get()
        {
            {{}}            return new List<TaskModel>()
            {
                new TaskModel () { DateTimeUtc = DateTime.Now, Description = "War voll fleißig am Coden, Testen, Deployen", Ticket = "ABC-1234" }
            };
        }

        // GET api/tasks/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/tasks/start
        /// <summary>
        /// This method starts a task.
        /// This method accepts JSON object as parameter that contains UTC TimeStamp at least. Further properties may follow.
        /// </summary>
        /// <param name="jsonTask">Task object as JSON, containing UTC Timestamp at least</param>
        [HttpPost("start")]
        public ActionResult Start([FromBody] TaskModel task)
        {
            if (task == null)
                return BadRequest();

            var startCommand = AutoMapper.Mapper.Map<TaskModel, Timer.Abstractions.StartTaskCommand>(task);
            businessLayerCommandManager.AddCommand(startCommand);

            return Ok();
        }

        // POST api/tasks/stop
        /// <summary>
        /// This method stops a task.
        /// This method accepts JSON object as parameter that contains UTC TimeStamp at least. Further properties may follow.
        /// </summary>
        /// <param name="jsonTask">Task object as JSON, containing UTC Timestamp at least</param>
        [HttpPost("stop")]
        public ActionResult Stop([FromBody] TaskModel task)
        {
            if (task == null)
                return BadRequest();

            var stopCommand = AutoMapper.Mapper.Map<TaskModel, Timer.Abstractions.StopTaskCommand>(task);
            businessLayerCommandManager.AddCommand(stopCommand);

            return Ok();
        }

        // POST api/tasks/interrupt
        /// <summary>
        /// This method interrupts a task. When the interrupt is terminated, the original task continues.
        /// This method accepts JSON object as parameter that contains UTC TimeStamp at least. Further properties may follow.
        /// </summary>
        /// <param name="jsonTask">Task object as JSON, containing UTC Timestamp at least</param>
        [HttpPost]
        public ActionResult Interrupt([FromBody] TaskModel task)
        {
            if (task == null)
                return BadRequest();

            var interruptCommand = AutoMapper.Mapper.Map<TaskModel, Timer.Abstractions.InterruptCommand>(task);
            businessLayerCommandManager.AddCommand(interruptCommand);

            return Ok();
        }
    }
}
