using DesignApp.Domain.Models;
using System.Collections.Generic;

namespace DesignApp.Application.Interfaces
{
    public interface IUserRepository
    {
        User Find(string userId);
        List<User> GetAll();
        User Update(User user);
    }
}