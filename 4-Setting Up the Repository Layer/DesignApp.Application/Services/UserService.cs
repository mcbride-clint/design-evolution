using DesignApp.Application.Models;
using DesignApp.Infrastructure.Persistance;
using System.Collections.Generic;

namespace DesignApp.Application.Services
{
    /// <summary>
    /// Handles All Business Tasks and Actions Related to the User Data.
    /// Every Task/Action is represented as a function.
    /// </summary>
    public class UserService
    {
        public User FindUser(string userId)
        {
            UserRepository repo = new UserRepository();

            // Get Data from Repository
            User user = repo.Find(userId);

            return user;
        }

        public List<User> GetAllUsers()
        {
            UserRepository repo = new UserRepository();

            // Get Data from Repository
            List<User> users = repo.GetAll();

            return users;
        }

        public int SaveAllUsers(List<User> users)
        {
            UserRepository repo = new UserRepository();

            foreach (User user in users)
            {
                repo.Update(user);
            }
            return 1;
        }

        /*
         * Moving all the data access to a repository allows the core business rules to shine through in each action as pure C# Code. 
         * 
         * But because of the "UserRepository repo = new UserRepository();" UserRepository is a dependency of the service.  
         *      With it being 'newed' up in each function, every test that is written will always be reliant on the Database that UserRepository tells it should use.
         *      
         * In the next project we will:
         *      Make the Repository into an Interface that can be satisfied by different class Implementations so we can focus tests on the business Rules
         */
    }
}
