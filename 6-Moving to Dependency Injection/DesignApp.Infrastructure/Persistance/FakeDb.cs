using System.Collections.Generic;
using System.Data;
namespace DesignApp.Infrastructure.Persistance
{
    public class FakeDb
    {
        public List<T> Query<T>(string sql, object parameters = null)
        {
            return new List<T>();
        }

        public T QuerySingle<T>(string sql, object parameters = null)
        {
            return default;
        }

        public int Execute(string sql, object parameters = null)
        {
            return 1;
        }
    }
}
