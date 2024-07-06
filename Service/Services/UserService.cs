using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Service.Interfaces;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository = new UserRepository();

        public List<User> GetUsers()
        {
            try
            {
                return userRepository.GetUsers();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UserService.GetUsers", ex);
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                return userRepository.GetUserById(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in UserService.GetUserById for ID {userId}", ex);
            }
        }

        public void AddUser(User user)
        {
            try
            {
                userRepository.AddUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UserService.AddUser", ex);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UserService.UpdateUser", ex);
            }
        }

        public void RemoveUser(int userId)
        {
            try
            {
                userRepository.RemoveUser(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in UserService.RemoveUser for ID {userId}", ex);
            }
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                return userRepository.GetUserByEmailAndPassword(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in UserService.GetUserByEmailAndPassword for email {email}", ex);
            }
        }
    }
}