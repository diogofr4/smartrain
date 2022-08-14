using Domain.Entities;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        { 
            _taskRepository = taskRepository;
        }

        public async void CreateTask(TaskEntity taskEntity)
        {
            taskEntity.Id = await _taskRepository.InsertTask(taskEntity);
        }

        public void DeletePlant(int taskId)
        {
            _taskRepository.DeleteTask(taskId);
        }

        public IEnumerable<TaskEntity> GetTasks()
        {
            return _taskRepository.GetTasks<TaskEntity>();
        }

        public void UpdateTask(TaskEntity taskEntity)
        {
            _taskRepository.UpdateTask(taskEntity);
        }
    }

