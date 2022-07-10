using Domain.Entities;
using Domain.Interface;
using Infrastructure.Clients;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TaskRepository : DapperClient, ITaskRepository
    {
        public TaskRepository(IConfiguration configuration) : base(configuration, "SmartRainSensorDatabase")
        {
        }

        private const string SQL_GET_TASKS = @"
            SELECT
	            t.id,
	            t.title,
	            t.description,
	            t.responsibleUser,
	            t.startDateTime,
	            t.endDateTime
            FROM Task t";

        private const string SQL_DELETE_TASK = @"
            DELETE FROM
                Task
            WHERE
                id = @TaskId
        ";

        public IEnumerable<T> GetTasks<T>()
        {
            return Get<T>(SQL_GET_TASKS);
        }

        public async Task<int> InsertTask(TaskEntity taskEntity)
        {
            return await Insert(new { taskEntity.Title, taskEntity.Description, taskEntity.ResponsibleUser, taskEntity.StartDateTime, taskEntity.EndDateTime }, tableName: "Task");
        }

        public void DeleteTask(int taskId)
        {
            Delete(SQL_DELETE_TASK, new { TaskId = taskId });
        }

        public bool UpdateTask(TaskEntity taskEntity)
        {
            return UpdatePlant(
                new
                {
                    id = taskEntity.Id,
                    title = taskEntity.Title,
                    description = taskEntity.Description,
                    startDateTime = taskEntity.StartDateTime,
                    endDateTime = taskEntity.EndDateTime
                }, new List<string>
                {
                    "id"
                }, 
                tableName: "Task");
        }
    }
}
