using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ITaskService
    {
        IEnumerable<TaskEntity> GetTasks();
        void CreateTask(TaskEntity taskEntity);
        void DeletePlant(int taskId);
        void UpdateTask(TaskEntity taskEntity);
    }
}
