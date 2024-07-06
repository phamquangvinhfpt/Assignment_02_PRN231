using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Dao
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        private readonly EBookStoreContext context;

        private UserDAO()
        {
            context = new EBookStoreContext();
        }

        public static UserDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                            instance = new UserDAO();
                    }
                }
                return instance;
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting users", ex);
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.Users.Find(userId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user with ID {userId}", ex);
            }
        }

        public void AddUser(User user)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    if (context.Users.Any(u => u.EmailAddress == user.EmailAddress))
                        throw new Exception("User with this email already exists");
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user", ex);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    if (context.Users.Find(user.UserId) == null)
                        throw new Exception("User not found");
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                    context.Entry(user).State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user", ex);
            }
        }

        public void RemoveUser(int userId)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    if (context.Users.Find(userId) == null)
                        throw new Exception("User not found");
                    var user = context.Users.Find(userId);
                    if (user != null)
                    {
                        context.Users.Remove(user);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing user with ID {userId}", ex);
            }
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.Users.FirstOrDefault(u => u.EmailAddress == email && u.Password == password);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting user by email and password", ex);
            }
        }
    }
}