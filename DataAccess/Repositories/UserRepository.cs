using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Dao;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetUsers() => UserDAO.Instance.GetUsers();
        public User GetUserById(int userId) => UserDAO.Instance.GetUserById(userId);
        public void AddUser(User user) => UserDAO.Instance.AddUser(user);
        public void UpdateUser(User user) => UserDAO.Instance.UpdateUser(user);
        public void RemoveUser(int userId) => UserDAO.Instance.RemoveUser(userId);
        public User GetUserByEmailAndPassword(string email, string password) => UserDAO.Instance.GetUserByEmailAndPassword(email, password);
    }

}