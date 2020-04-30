using DesignApp.Application.Interfaces;
using DesignApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DesignApp.Application.Services
{
    /// <summary>
    /// Handles All Business Tasks and Actions Related to the User Data.
    /// Every Task/Action is represented as a function.
    /// </summary>
    public class UserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public User FindUser(string userId)
        {
            // Get Data from Repository
            User user = _userRepo.Find(userId);

            return user;
        }

        public List<User> GetAllUsers()
        {
            // Get Data from Repository
            List<User> users = _userRepo.GetAll();

            return users;
        }

        public int UpdateUser(User user)
        {
            // Validate Changes
            if (!IsOwnerCodeValid(user.Owner))
            {
                throw new ArgumentOutOfRangeException($"Owner Code {user.Owner} is not valid for {user.UserId}");
            }

            _userRepo.Update(user);
            return 1;
        }

        private bool IsOwnerCodeValid(string owner)
        {
            string[] allowedOwners = new string[] { "A", "B", "C" };

            return allowedOwners.Contains(owner.ToUpper());
        }

        public int SaveAllUsers(List<User> users)
        {
            foreach (User user in users)
            {
                UpdateUser(user);
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
