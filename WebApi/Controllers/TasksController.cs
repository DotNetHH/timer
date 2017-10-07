using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        /// This method starts a task.
        /// This method accepts JSON object as parameter that contains UTC TimeStamp at least. Further properties may follow.
        /// </summary>
        /// <param name="jsonTask">Task object as JSON, containing UTC Timestamp at least</param>
        [HttpPost]
        public void Start([FromBody]string jsonTask)
        {
            
        }

        // POST api/values
        /// <summary>
        /// This method stops a task.
        /// This method accepts JSON object as parameter that contains UTC TimeStamp at least. Further properties may follow.
        /// </summary>
        /// <param name="jsonTask">Task object as JSON, containing UTC Timestamp at least</param>
        [HttpPost]
        public void Stop([FromBody]string jsonTask)
        {

        }
    }
}
