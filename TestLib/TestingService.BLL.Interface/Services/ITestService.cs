using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;

namespace TestingService.BLL.Interface.Services
{
    public interface ITestService
    {
        IEnumerable<TestEntity> GetAll();
        IEnumerable<TestEntity> GetByPredicate(Expression<Func<TestEntity, bool>> f);
        TestEntity GetById(int key);
        void CreateTest(TestEntity e);
        void DeleteTest(TestEntity e);
        void UpdateTest(TestEntity e);
        IEnumerable<TestEntity> SearchByString(string key);
        IEnumerable<TestEntity> GetByName(string name);
        IEnumerable<TestEntity> GetByAuthor(UserEntity user);
        IEnumerable<TestEntity> GetEarlierThanDate(DateTime date);
        IEnumerable<TestEntity> GetLaterThanDate(DateTime date);
        IEnumerable<TestEntity> GetInterviews();
        IEnumerable<TestEntity> GetTests();
        bool AllowedUser(TestEntity test, UserEntity user);
        void SetCoauthor(UserEntity user, TestEntity test);
    }
}
