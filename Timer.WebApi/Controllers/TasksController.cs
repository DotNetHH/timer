using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Timer.WebApi.Models;
using Timer.Business.Abstractions;
using System.Linq;
using Timer.Data.Abstractions;

namespace WebApi.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private Timer.Business.Abstractions.ICommandManager businessLayerWriterCommandManager;
        private Timer.Business.Abstractions.IReaderManager businessLayerReaderManager;
        private readonly IMapper _mapper;

        public TasksController(Timer.Business.Abstractions.ICommandManager commandManager, IMapper mapper, IReaderManager readerManager)
        {
            this.businessLayerWriterCommandManager = commandManager;
            this.businessLayerReaderManager = readerManager;
            _mapper = mapper;
        }

        // GET: api/tasks
        [HttpGet]
        public IEnumerable<TaskModel> Get()
        {
            return businessLayerReaderManager.GetAll().Select(e => _mapper.Map<TimerEvent, TaskModel>(e));
        }

        // GET api/tasks/today
        [HttpGet("today")]
        public string GetAllHoursForToday()
        {
            return businessLayerReaderManager.GetAllHoursForToday().ToString();
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

            var startCommand = _mapper.Map<TaskModel, Timer.Business.Abstractions.StartTaskCommand>(task);
            businessLayerWriterCommandManager.AddWriterCommand(startCommand);

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

            var stopCommand = _mapper.Map<TaskModel, Timer.Business.Abstractions.StopTaskCommand>(task);
            businessLayerWriterCommandManager.AddWriterCommand(stopCommand);

            return Ok();
        }

        // POST api/tasks/interrupt
        /// <summary>
        /// This method interrupts a task. When the interrupt is terminated, the original task continues.
        /// This method accepts JSON object as parameter that contains UTC TimeStamp at least. Further properties may follow.
        /// </summary>
        /// <param name="jsonTask">Task object as JSON, containing UTC Timestamp at least</param>
        [HttpPost("interrupt")]
        public ActionResult Interrupt([FromBody] TaskModel task)
        {
            if (task == null)
                return BadRequest();

            var interruptCommand = _mapper.Map<TaskModel, Timer.Business.Abstractions.InterruptTaskCommand>(task);
            businessLayerWriterCommandManager.AddWriterCommand(interruptCommand);

            return Ok();
        }
    }
}
