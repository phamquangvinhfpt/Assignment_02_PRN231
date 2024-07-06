using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Dao;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public List<Role> GetRoles() => RoleDAO.Instance.GetRoles();
        public Role GetRoleById(int roleId) => RoleDAO.Instance.GetRoleById(roleId);
        public void AddRole(Role role) => RoleDAO.Instance.AddRole(role);
        public void UpdateRole(Role role) => RoleDAO.Instance.UpdateRole(role);
        public void RemoveRole(int roleId) => RoleDAO.Instance.RemoveRole(roleId);
    }
}