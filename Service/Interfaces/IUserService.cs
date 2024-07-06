using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace Service.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUserById(int userId);
        void AddUser(User user);
        void UpdateUser(User user);
        void RemoveUser(int userId);
        User GetUserByEmailAndPassword(string email, string password);
    }
}