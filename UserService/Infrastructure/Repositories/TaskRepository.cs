﻿using Domain.Interface;
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
        public TaskRepository(IConfiguration configuration, string connectionStringName) : base(configuration, connectionStringName)
        {
        }
    }
}