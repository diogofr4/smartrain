using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class TaskController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Models.Task> GetTasks()
        {
            return _mapper.Map<IEnumerable<Models.Task>>(_taskService.GetTasks());
        }

        [HttpPost]
        public IActionResult CreateTask(Models.Task newTask)
        {
            try
            {
                _taskService.CreateTask(_mapper.Map<TaskEntity>(newTask));
                return Ok(newTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public IActionResult DeleteTask(int taskId)
        {
            try
            {
                _taskService.DeletePlant(taskId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public IActionResult EditTask(int taskId, Models.Task task)
        {
            try
            {
                task.Id = taskId;
                _taskService.UpdateTask(_mapper.Map<TaskEntity>(task));
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
