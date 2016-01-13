using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Mappers;
using TestingService.DAL.Interface.Repositories;

namespace TestingService.BLL.Services
{
    public class UsersTestsService:IUsersTestsService
    {
        private readonly IUnitOfWork uow;
        private readonly IUsersTestRepository usersTestsRepository;

        public UsersTestsService(IUnitOfWork uow, IUsersTestRepository usersTestsRepository)
        {
            this.uow = uow;
            this.usersTestsRepository = usersTestsRepository;
        }

        public IEnumerable<UsersTestsEntity> GetAll()
        {
            return usersTestsRepository.GetAll().Select(test => test.ToBllUsersTest());
        }

        public IEnumerable<UsersTestsEntity> GetByPredicate(Expression<Func<UsersTestsEntity, bool>> f)
        {
            Func<UsersTestsEntity, bool> func = f.Compile();
            return usersTestsRepository.GetByPredicate(t => func(t.ToBllUsersTest())).Select(t => t.ToBllUsersTest());
        }

        public UsersTestsEntity GetById(int key)
        {
            return usersTestsRepository.GetById(key).ToBllUsersTest();
        }

        public void AddTesting(UsersTestsEntity e)
        {
            usersTestsRepository.Create(e.ToDalUsersTest());
            uow.Commit();
        }

        public void DeleteTesting(UsersTestsEntity e)
        {
            usersTestsRepository.Delete(e.ToDalUsersTest());
            uow.Commit();
        }

        public void UpdateTesting(UsersTestsEntity e)
        {
            usersTestsRepository.Update(e.ToDalUsersTest());
            uow.Commit();
        }

        public bool IsUserTested(UserEntity user)
        {
            return usersTestsRepository.IsUserTested(user.ToDalUser());
        }

        public void StartTest(UserEntity user, TestEntity test)
        {
            usersTestsRepository.StartTest(user.ToDalUser(), test.ToDalTest());
            uow.Commit();
        }

        public void FinishTest(UserEntity user)
        {
            usersTestsRepository.FinishTest(user.ToDalUser());
            uow.Commit();
        }

        public UsersTestsEntity GetLastResult(UserEntity user)
        {
            return usersTestsRepository.GetLastResult(user.ToDalUser()).ToBllUsersTest();
        }
    }
}
