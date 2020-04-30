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
        /// This would simulate our "Grid Page".  
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
        /// Simulates one of our "Save Pages"
        /// Receives All the Data From Front End to Save
        /// </summary>
        /// <param name="row"></param>
        public void OnPost(DataTable data)
        {
            FakeDb db = new FakeDb();

            // Not Exactly how we do it but should be enough to get the idea
            foreach (DataRow row in data.Rows)
            {
                // Store the data into individual variables
                string userid = row.Field<string>(0);
                string newOwner = row.Field<string>(1);

                // Build Sql
                var sql = "Update UserId Set Owner = '" + newOwner + "' Where UserId = '" + userid + "'";

                // Execute Update
                db.RunSql(sql);
            }
        }

        /*
         * Pros:
         * Everything is right here.
         * No Chance of breaking other pages since everything is Silo'd(sp?)
         * 
         * Cons:
         * All the Code is very rigid and stuck to the UI
         * No way to verify the code is working correctly without starting the website and testing every possible input
         */
    }
}