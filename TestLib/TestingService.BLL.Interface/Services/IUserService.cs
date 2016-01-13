using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;

namespace TestingService.BLL.Interface.Services
{
    public interface IUserService
    {
        UserEntity GetUserEntity(int id);
        IEnumerable<UserEntity> GetAllUsers();
        IEnumerable<UserEntity> GetByPredicate(Expression<Func<UserEntity, bool>> f);
        void CreateUser(UserEntity user);
        void DeleteUser(UserEntity user);
        void UpdateUser(UserEntity user);
        string GetHash(string password);
        bool ValidAuthentication(string login, string passwordHash);
        IEnumerable<UserEntity> GetCoauthors(TestEntity test);
        UserEntity GetByLogin(string login);
        bool ExistUser(string login);
        IEnumerable<UserEntity> GetByRole(RoleEntity role);
        void SetRole(UserEntity user, RoleEntity role);
    }
}
