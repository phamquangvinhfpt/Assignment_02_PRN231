using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Interfaces
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
        Role GetRoleById(int roleId);
        void AddRole(Role role);
        void UpdateRole(Role role);
        void RemoveRole(int roleId);
    }
}