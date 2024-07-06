using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace Service.Interfaces
{
    public interface IRoleService
    {
        List<Role> GetRoles();
        Role GetRoleById(int roleId);
        void AddRole(Role role);
        void UpdateRole(Role role);
        void RemoveRole(int roleId);
    }
}