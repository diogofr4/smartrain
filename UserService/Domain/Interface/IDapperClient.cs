using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IDapperClient
    {
        IEnumerable<T> Get<T>(string sql, object @params = null);

        Task<int> Insert<T>(T entity, string tableName = null);

        bool UpdatePlant<T>(T entity, List<string> searchKeys, string tableName = null);

        bool Delete(string sql, object @params = null);
    }
}
