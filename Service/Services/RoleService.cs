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
    public class RoleService : IRoleService
    {
        private IRoleRepository roleRepository = new RoleRepository();

        public List<Role> GetRoles()
        {
            try
            {
                return roleRepository.GetRoles();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in RoleService.GetRoles", ex);
            }
        }

        public Role GetRoleById(int roleId)
        {
            try
            {
                return roleRepository.GetRoleById(roleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in RoleService.GetRoleById for ID {roleId}", ex);
            }
        }

        public void AddRole(Role role)
        {
            try
            {
                roleRepository.AddRole(role);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in RoleService.AddRole", ex);
            }
        }

        public void UpdateRole(Role role)
        {
            try
            {
                roleRepository.UpdateRole(role);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in RoleService.UpdateRole", ex);
            }
        }

        public void RemoveRole(int roleId)
        {
            try
            {
                roleRepository.RemoveRole(roleId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in RoleService.RemoveRole for ID {roleId}", ex);
            }
        }
    }
}