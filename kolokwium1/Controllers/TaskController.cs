using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolokwium1.Models;
using kolokwium1.Request;
using kolokwium1.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium1.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase{
        private IDbService dbService;

        public TaskController(IDbService dbService) {
            this.dbService = dbService;
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id){

            ProjectTasks projectTasks = dbService.GetTask(id);
            if (projectTasks == null) {
                return NotFound();
            }

            return Ok(projectTasks.listTask);
        }

        [HttpPut]
        public IActionResult AddTask(AddTask addTask) {

            dbService.AddTaskToDb(addTask);
            return Ok();
        }
    }
}