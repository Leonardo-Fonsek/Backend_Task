using Microsoft.AspNetCore.Mvc;
using Tasks.Models;
using System.Collections.Generic;
using System.Linq;

namespace Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        public static List<Task> tasks = new List<Task>();

        [HttpPost]
        public ActionResult<List<Task>> AddTask(Task task)
        {
            if (task.Description.Length < 10)
            {
                return BadRequest("Need more characters");
            }

            task.Id = tasks.Count > 0 ? tasks[tasks.Count - 1].Id + 1 : 1;

            tasks.Add(task);
            return Ok(tasks);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var taskDelete = tasks.FirstOrDefault(x => x.Id == id);
            if (taskDelete == null)
            {
                return NotFound("This task doesn´t exist");
            }

            tasks.Remove(taskDelete);

            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<Task> GetTask(int id)
        {
            var task = tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return NotFound(new { message = "Tarefa não encontrada!" });
            }

            return Ok(task);
        }



    }
}

namespace Tasks.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
