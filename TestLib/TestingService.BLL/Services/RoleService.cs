using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Mappers;
using TestingService.DAL.Interface.Repositories;

namespace TestingService.BLL.Services
{
    public class RoleService:IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;
         
        public RoleService(IUnitOfWork uow, IRoleRepository roleRepository)
        {
            this.uow = uow;
            this.roleRepository = roleRepository;
        }

        public IEnumerable<RoleEntity> GetAll()
        {
            return roleRepository.GetAll().Select(role => role.ToBllRole());
        }

        public RoleEntity GetById(int key)
        {
            return roleRepository.GetById(key).ToBllRole();
        }

        public void CreateRole(RoleEntity e)
        {
            roleRepository.Create(e.ToDalRole());
            uow.Commit();
        }

        public void DeleteRole(RoleEntity e)
        {
            roleRepository.Delete(e.ToDalRole());
            uow.Commit();
        }

        public void UpdateRole(RoleEntity e)
        {
            roleRepository.Update(e.ToDalRole());
            uow.Commit();
        }

        public IEnumerable<RoleEntity> GetUsersRoles(UserEntity e)
        {
            return roleRepository.GetUsersRoles(e.ToDalUser()).Select(role => role.ToBllRole());
        }
    }
}
