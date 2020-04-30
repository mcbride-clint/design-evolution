using DesignApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignApp.Infrastructure.Persistance
{
    public class UserRepository
    {
        public User Find(string userId)
        {
            FakeDb db = new FakeDb();

            // Build Sql
            string sql = "Select UserId, Owner From UserId Where UserId = :UserId";

            // Get Data and store in Property
            User user = db.QuerySingle<User>(sql, new { UserId = userId });

            return user;
        }

        public List<User> GetAll()
        {
            FakeDb db = new FakeDb();

            // Build Sql
            string sql = "Select UserId, Owner From UserId";

            // Get Data and store in Property
            List<User> users = db.Query<User>(sql);

            return users;
        }

        public User Update(User user)
        {
            FakeDb db = new FakeDb();

            // Build Sql with Parameters to minimize risk of Sql Injection
            var sql = "Update UserId Set Owner = :Owner Where UserId = :UserId ";

            // Because of the Parameters Dapper can automatically bind the Properties of User to the Sql
            db.Execute(sql, user);

            // returns the saved user in case there were any changes
            return user;
        }
    }
}
