using Dapper;
using Domain.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
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

        public bool UpdatePlant<T>(T entity, List<string> searchKeys,string tableName = null)
        {
            var table = tableName ?? GetTableName(typeof(T));
            var columnsName = GetColumnsName(entity).Where(x => !searchKeys.Contains(x)).ToList();
            var propertiesName = entity.GetType().GetProperties().Where(x => !searchKeys.Contains(x.Name)).Select(x => x.Name).ToList();
            var keyColumnValue = new StringBuilder();
            var columnValue = new StringBuilder();
            for (int i = 0; i < columnsName.Count(); i++)
            {
                columnValue.Append($"{columnsName[i]} = @{propertiesName[i]}");
                if (i != columnsName.Count() - 1)
                    columnValue.Append(",");
            }

            for(int i = 0; i < searchKeys.Count(); i++)
            {
                keyColumnValue.Append($"{searchKeys[i]} = @{searchKeys[i]}");
                if (i != searchKeys.Count() - 1)
                    keyColumnValue.Append("AND");
            }

            var sql = $@"UPDATE [dbo].[{ table }] SET { columnValue.ToString() } WHERE {keyColumnValue.ToString()}";

            return sqlConnection.Execute(sql, entity) >= 1;
        }

        public IEnumerable<T> Get<T>(string sql, object @params = null) => sqlConnection.Query<T>(sql, @params);
        public bool Delete(string sql, object @params = null) => sqlConnection.Execute(sql, @params) >= 1;

        public async Task<int> Insert<T>(T entity, string tableName = null)
        {
            var table = tableName ?? GetTableName(typeof(T));
            var sql = $@"INSERT INTO [dbo].[{ table }] ({ string.Join(", ", GetColumnsName(entity)) })
                        OUTPUT INSERTED.Id
                        VALUES (@{ string.Join(", @", entity.GetType().GetProperties().Select(x => x.Name)) })";
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();

            var id = await sqlConnection.QuerySingleAsync<int>(sql, entity);
            sqlConnection.Close();

            return id;
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
