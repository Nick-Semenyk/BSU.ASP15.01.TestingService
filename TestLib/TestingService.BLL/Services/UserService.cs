using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Interface.Services;
using TestingService.DAL.Interface.Repositories;
using static TestingService.BLL.Mappers.BllEntityMappers;

namespace TestingService.BLL.Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork uow, IUserRepository userRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
        }

        public UserEntity GetUserEntity(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public IEnumerable<UserEntity> GetByPredicate(Expression<Func<UserEntity, bool>> f)
        {
            Func<UserEntity, bool> func = f.Compile();
            return userRepository.GetByPredicate(t => func(t.ToBllUser())).Select(t => t.ToBllUser());
        }

        public void CreateUser(UserEntity user)
        {
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void DeleteUser(UserEntity user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public void UpdateUser(UserEntity user)
        {
            userRepository.Update(user.ToDalUser());
            uow.Commit();
        }

        public string GetHash(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public bool ValidAuthentication(string login, string passwordHash)
        {
            UserEntity e;
            if ((e = GetByLogin(login)) != null)
            {
                return e.PasswordHash == passwordHash;
            }
            return false;
        }

        public IEnumerable<UserEntity> GetCoauthors(TestEntity test)
        {
            return userRepository.GetCoauthors(test.ToDalTest()).Select(user => user.ToBllUser());
        }

        public UserEntity GetByLogin(string login)
        {
            return userRepository.GetByLogin(login).ToBllUser();
        }

        public bool ExistUser(string login)
        {
            return userRepository.ExistUser(login);
        }

        public IEnumerable<UserEntity> GetByRole(RoleEntity role)
        {
            return userRepository.GetByRole(role.ToDalRole()).Select(user => user.ToBllUser());
        }

        public void SetRole(UserEntity user, RoleEntity role)
        {
            userRepository.SetRole(user.ToDalUser(), role.ToDalRole());
            uow.Commit();
        }
    }
}
