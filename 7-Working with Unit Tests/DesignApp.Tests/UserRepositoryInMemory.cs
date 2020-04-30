using DesignApp.Application.Interfaces;
using DesignApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignApp.Tests
{
    public class UserRepositoryInMemory : IUserRepository
    {
        private static List<User> _users = new List<User>();

        public UserRepositoryInMemory()
        {
            if(_users.Count == 0)
            {
                LoadSeedData();
            }
        }

        private void LoadSeedData()
        {
            _users.Add(new User() { UserId = "smith12", Owner = "A" });
            _users.Add(new User() { UserId = "smith13", Owner = "B" });
            _users.Add(new User() { UserId = "william", Owner = "A" });
            _users.Add(new User() { UserId = "abc1234", Owner = "C" });
        }

        public User Find(string userId)
        {
            return InMemoryHelpers.Clone(_users.SingleOrDefault(u => u.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase)));
        }

        public List<User> GetAll()
        {
            return InMemoryHelpers.Clone(_users);
        }

        public User Update(User user)
        {
            var existingUser = _users.SingleOrDefault(u => u.UserId.Equals(user.UserId, StringComparison.OrdinalIgnoreCase));

            if(existingUser == null)
            {
                throw new Exception("User Not Found.");
            }

            existingUser.Owner = user.Owner;

            return existingUser;
        }
    }
}
