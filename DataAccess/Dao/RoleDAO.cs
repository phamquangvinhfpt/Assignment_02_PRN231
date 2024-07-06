using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Dao
{
    public class RoleDAO
    {
        private static RoleDAO instance = null;
        private static readonly object instanceLock = new object();
        private readonly EBookStoreContext context;

        private RoleDAO()
        {
            context = new EBookStoreContext();
        }

        public static RoleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                            instance = new RoleDAO();
                    }
                }
                return instance;
            }
        }

        public List<Role> GetRoles()
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.Roles.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting roles", ex);
            }
        }

        public Role GetRoleById(int roleId)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    return context.Roles.Find(roleId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting role with ID {roleId}", ex);
            }
        }

        public void AddRole(Role role)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.Roles.Add(role);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding role", ex);
            }
        }

        public void UpdateRole(Role role)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    context.Entry(role).State = EntityState.Modified;
                    context.SaveChanges();
                    context.Entry(role).State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating role", ex);
            }
        }

        public void RemoveRole(int roleId)
        {
            try
            {
                using (var context = new EBookStoreContext())
                {
                    var role = context.Roles.Find(roleId);
                    if (role == null)
                    {
                        throw new KeyNotFoundException($"Role with ID {roleId} not found.");
                    }
                    context.Roles.Remove(role);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing role with ID {roleId}", ex);
            }
        }
    }
}