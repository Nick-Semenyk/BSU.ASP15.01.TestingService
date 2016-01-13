using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;

namespace TestingService.BLL.Interface.Services
{
    public interface IRoleService
    {
        IEnumerable<RoleEntity> GetAll();
        IEnumerable<RoleEntity> GetByPredicate(Expression<Func<RoleEntity, bool>> f);
        RoleEntity GetById(int key);
        void CreateRole(RoleEntity e);
        void DeleteRole(RoleEntity e);
        void UpdateRole(RoleEntity e);
        IEnumerable<RoleEntity> GetUsersRoles(UserEntity e);
    }
}
