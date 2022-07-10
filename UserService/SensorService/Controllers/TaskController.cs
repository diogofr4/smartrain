using AutoMapper;
using Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class TaskController
    {
        private readonly IMapper _mapper;

        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }
    }
}
