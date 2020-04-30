using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace DesignApp.Pages.Users
{
    public class UserMaintModel : PageModel
    {
        /*
         * Simulates our current structure in our Application with a basic page.
         * It can see all the UserIds in the Database and Edit one that is sent back.
         * This is not a working example.
         */

        /// <summary>
        /// This would be our "Grid Page".  
        /// Runs Sql to get the full list and sends it to the front end.
        /// </summary>
        /// <returns></returns>
        public DataTable OnGet()
        {
            FakeDb db = new FakeDb();

            // Build Sql
            string sql = "Select UserId, Owner From UserId";

            // Get Data
            DataTable data = db.RunSql(sql);

            return data;
        }

        /// <summary>
        /// Receives Data From Front End to Save
        /// </summary>
        /// <param name="row"></param>
        public void OnPost(DataRow row)
        {
            FakeDb db = new FakeDb();

            // Get the data back from the page
            string userid = row.Field<string>(0);
            string newOwner = row.Field<string>(1);

            // Build Sql
            var sql = "Update UserId Set Owner = '" + newOwner + "' Where UserId = '" + userid + "'";

            // Execute Update
            db.RunSql(sql);
        }
    }
}