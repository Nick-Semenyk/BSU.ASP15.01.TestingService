using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Mappers;
using TestingService.DAL.Interface.Entities;
using TestingService.DAL.Interface.Repositories;

namespace TestingService.BLL.Services
{
    public class TestService:ITestService
    {
        private readonly IUnitOfWork uow;
        private readonly ITestRepository testRepository;

        public TestService(IUnitOfWork uow, ITestRepository testRepository)
        {
            this.uow = uow;
            this.testRepository = testRepository;
        }
        
        public IEnumerable<TestEntity> GetAll()
        {
            return testRepository.GetAll().Select(test => test.ToBllTest());
        }

        public TestEntity GetById(int key)
        {
            return testRepository.GetById(key).ToBllTest();
        }

        public void CreateTest(TestEntity e)
        {
            testRepository.Create(e.ToDalTest());
            uow.Commit();
        }

        public void DeleteTest(TestEntity e)
        {
            testRepository.Delete(e.ToDalTest());
            uow.Commit();
        }

        public void UpdateTest(TestEntity e)
        {
            testRepository.Update(e.ToDalTest());
            uow.Commit();
        }

        public IEnumerable<TestEntity> SearchByString(string key)
        {
            return testRepository.SearchByString(key).Select(test => test.ToBllTest());
        }

        public IEnumerable<TestEntity> GetByName(string name)
        {
            return testRepository.GetByName(name).Select(test => test.ToBllTest());
        }

        public IEnumerable<TestEntity> GetByAuthor(UserEntity user)
        {
            return testRepository.GetByAuthor(user.ToDalUser()).Select(test => test.ToBllTest());
        }

        public IEnumerable<TestEntity> GetEarlierThanDate(DateTime date)
        {
            return testRepository.GetEarlierThanDate(date).Select(test => test.ToBllTest());
        }

        public IEnumerable<TestEntity> GetLaterThanDate(DateTime date)
        {
            return testRepository.GetLaterThanDate(date).Select(test => test.ToBllTest());
        }

        public IEnumerable<TestEntity> GetInterviews()
        {
            return testRepository.GetInterviews().Select(test => test.ToBllTest());
        }

        public IEnumerable<TestEntity> GetTests()
        {
            return testRepository.GetTests().Select(test => test.ToBllTest());
        }

        public bool AllowedUser(TestEntity test, UserEntity user)
        {
            return testRepository.AllowedUser(test.ToDalTest(), user.ToDalUser());
        }

        public void SetCoauthor(UserEntity user, TestEntity test)
        {
            testRepository.SetCoauthor(user.ToDalUser(), test.ToDalTest());
            uow.Commit();
        }
    }
}
