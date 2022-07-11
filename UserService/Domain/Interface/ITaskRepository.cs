using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ITaskRepository
    {
        Task<int> InsertTask(TaskEntity taskEntity);
        IEnumerable<T> GetTasks<T>();
        void DeleteTask(int taskId);
        bool UpdateTask(TaskEntity taskEntity);
    }
}
