using Dapper;
using Domain.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Clients
{
    public class DapperClient : IDapperClient
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection sqlConnection;

        public DapperClient(IConfiguration configuration, string connectionStringName)
        {
            _configuration = configuration;
            sqlConnection = new SqlConnection(_configuration.GetConnectionString(connectionStringName));
        }

        public IEnumerable<T> Get<T>(string sql, object @params = null) => sqlConnection.Query<T>(sql, @params);

        public void Insert<T>(T entity)
        {
            var sql = $@"INSERT INTO [dbo].[{ GetTableName(typeof(T)) }] ({ string.Join(", ", GetColumnsName(entity)) })
                        VALUES (@{ string.Join(", @", entity.GetType().GetProperties().Select(x => x.Name)) })";

            var affectedRows = sqlConnection.ExecuteAsync(sql, entity);
        }

        #region Utils

        private dynamic GetTableName(Type type)
        {
            dynamic classTableAttributte = type.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name.Equals("TableAttribute"));

            return classTableAttributte.Name ?? type.Name;
        }

        private IList<string> GetColumnsName<T>(T entity)
        {
            IList<string> entityColumns = new List<string>();

            var properties = entity.GetType().GetProperties().AsList();

            properties.ForEach(x => { entityColumns.Add(x.GetCustomAttribute<ColumnAttribute>()?.Name ?? x.Name); });

            return entityColumns;
        }

        #endregion
    }
}
