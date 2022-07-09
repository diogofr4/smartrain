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

        void Insert<T>(T entity);
    }
}
