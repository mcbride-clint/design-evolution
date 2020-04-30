using System.Collections.Generic;
using System.Data;
namespace DesignApp
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
