using System.Collections.Generic;
using System.Data;
namespace DesignApp.Application.Persistance
{
    public class FakeDb
    {
        public List<T> Query<T>(string sql, object parameters = null)
        {
            return new List<T>();
        }

        public int Execute(string sql, object parameters = null)
        {
            return 1;
        }
    }
}
