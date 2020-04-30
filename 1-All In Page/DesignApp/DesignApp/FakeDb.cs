using System.Data;
namespace DesignApp
{
    public class FakeDb
    {
        public DataTable RunSql(string sql)
        {
            return new DataTable();
        }
    }
}
