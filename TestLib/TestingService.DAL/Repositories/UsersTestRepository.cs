using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TestingService.DAL.Interface.Entities;
using TestingService.DAL.Interface.Repositories;
using TestingService.ORM;

namespace TestingService.DAL.Repositories
{
    public class UsersTestRepository:IUsersTestRepository
    {
        private readonly DbContext context;

        public UsersTestRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalUsersTest> GetAll()
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<UsersTests>().Select(ut => new DalUsersTest()
            {
                Id = ut.id,
                BeginningTime = ut.BeginningTime,
                EndingTime = ut.EndingTime,
                MarkForTest = ut.MarkForTest,
                RightAnswersCount = ut.RightAnswersCount,
                TestId = ut.TestId,
                UserId = ut.UserId
            });
        }

        public DalUsersTest GetById(int key)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            var ut = context.Set<UsersTests>().FirstOrDefault(t => t.id == key);
            return new DalUsersTest()
            {
                Id = ut.id,
                BeginningTime = ut.BeginningTime,
                EndingTime = ut.EndingTime,
                MarkForTest = ut.MarkForTest,
                RightAnswersCount = ut.RightAnswersCount,
                TestId = ut.TestId,
                UserId = ut.UserId
            };
        }

        public void Create(DalUsersTest e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            UsersTests ut = new UsersTests()
            {
                id = e.Id,
                BeginningTime = e.BeginningTime,
                EndingTime = e.EndingTime,
                MarkForTest = e.MarkForTest,
                RightAnswersCount = e.RightAnswersCount,
                TestId = e.TestId,
                UserId = e.UserId
            };
            context.Set<UsersTests>().Add(ut);
        }

        public void Delete(DalUsersTest e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            UsersTests ut = context.Set<UsersTests>().
                FirstOrDefault(a => a.id == e.Id);
            context.Set<UsersTests>().Remove(ut);
        }

        public void Update(DalUsersTest entity)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            UsersTests ut = context.Set<UsersTests>().
                FirstOrDefault(a => a.id == entity.Id);
            ut.BeginningTime = entity.BeginningTime;
            ut.EndingTime = entity.EndingTime;
            ut.MarkForTest = entity.MarkForTest;
            ut.RightAnswersCount = entity.RightAnswersCount;
            ut.TestId = entity.TestId;
            ut.UserId = entity.UserId;
        }

        public bool IsUserTested(DalUser user)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<UsersTests>().Any(tests =>
                tests.EndingTime == null
                && tests.UserId == user.Id);
        }

        public void StartTest(DalUser user, DalTest test)
        {
            if (IsUserTested(user))
                return;
            Users ormuser = context.Set<Users>().FirstOrDefault(users => users.id == user.Id);        
            Tests ormtest = context.Set<Tests>().FirstOrDefault(tests => tests.id == test.Id);
            UsersTests startingTest = new UsersTests()
            {
                TestId = ormtest.id,
                BeginningTime = DateTime.Now,
                EndingTime = null,
                MarkForTest = null,
                RightAnswersCount = null,
                UserId = ormuser.id
            };
            context.Set<UsersTests>().Add(startingTest);
        }

        public void FinishTest(DalUser user)
        {
            if (!IsUserTested(user))
                return;
            Users ormuser = context.Set<Users>().FirstOrDefault(users => users.id == user.Id);
            UsersTests ormutest = context.Set<UsersTests>().FirstOrDefault(tests => 
                tests.UserId == user.Id
                && tests.EndingTime == null);
            Tests ormtest = context.Set<Tests>().FirstOrDefault(tests => tests.id == ormutest.TestId);
            //marks and other stuff will be in bll
            ormutest.EndingTime = DateTime.Now;
            
        }

        public DalUsersTest GetLastResult(DalUser user)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            UsersTests result = context.Set<UsersTests>().Where(tests =>
                tests.UserId == user.Id).OrderByDescending(tests => tests.BeginningTime).FirstOrDefault();
            return new DalUsersTest()
            {
                BeginningTime = result.BeginningTime,
                EndingTime = result.EndingTime,
                Id = result.id,
                MarkForTest = result.MarkForTest,
                RightAnswersCount = result.RightAnswersCount,
                TestId = result.TestId,
                UserId = result.UserId
            };
        }
    }
}
