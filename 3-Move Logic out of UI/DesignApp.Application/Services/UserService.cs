using DesignApp.Application.Models;
using DesignApp.Application.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignApp.Application.Services
{
    /// <summary>
    /// Handles All Business Tasks and Actions Related to the User Data.
    /// Every Task/Action is represented as a function.
    /// </summary>
    public class UserService
    {
        public List<User> GetAllUsers()
        {
            FakeDb db = new FakeDb();

            // Build Sql
            string sql = "Select UserId, Owner From UserId";

            // Get Data and store in Property
            List<User> users = db.Query<User>(sql);

            return users;
        }

        public int SaveAllUsers(List<User> users)
        {
            FakeDb db = new FakeDb();

            // Build Sql with Parameters to minimize risk of Sql Injection
            var sql = "Update UserId Set Owner = :Owner Where UserId = :UserId ";

            // Go through All the Data to Save
            // Ideally there would be a changed indicator to not save everything
            foreach (User user in users)
            {
                // Because of the Parameters Dapper can automatically bind the Properties of User to the Sql
                db.Execute(sql, user);
            }

            return 1;
        }

        /*
         * Because this code is in a seperate project it could now be triggered by programs other than the Website.  
         *      This allows for Automated Testing.  
         * 
         * But because of the "FakeDb db = new FakeDb();" FakeDb is a dependency of the service.  With it being 'newed' up in each function, 
         *      every test that is written will always be reliant on the Database that FakeDB tells it should use.
         *      
         * Also each function contains the SQL that is needs to run so it very tied to the business logic.  
         *      This makes it harder to create tests for data that is not in the database and could allow SQL to spread through other parts of our Applicaiton.
         *      
         * In the next project we will:
         *      Move the FakeDb Dependency to the UI layer so the Service wont be responsible for it anymore.
         *      We will also move the SQL code to a Repository layer to keep it from spreading through all layers of our application.
         */
    }
}
