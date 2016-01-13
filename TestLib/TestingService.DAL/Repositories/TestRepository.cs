using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TestingService.DAL.Interface.Entities;
using TestingService.DAL.Interface.Repositories;
using TestingService.ORM;

namespace TestingService.DAL.Repositories
{
    public class TestRepository:ITestRepository
    {
        private readonly DbContext context;

        public TestRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalTest> GetAll()
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<Tests>().Select(test => new DalTest()
            {
                Id = test.id,
                AuthorId = test.AuthorId,
                Name = test.Name,
                Description = test.Description,
                AllowedTime = test.AllowedTime,
                Anonymous = test.Anonymous,
                CreationDate = test.CreationDate,
                GlobalAvailability = test.GlobalAvailability,
                Interview = test.Interview,
                QuestionCount = test.QuestionCount
            });
        }

        public IEnumerable<DalTest> GetByPredicate(Expression<Func<DalTest, bool>> f)
        {
            Func<DalTest, bool> func = f.Compile();
            IEnumerable<DalTest> users = context.Set<Tests>().Where(u => true).AsEnumerable().Select(test => new DalTest()
            {
                Id = test.id,
                AuthorId = test.AuthorId,
                Name = test.Name,
                Description = test.Description,
                AllowedTime = test.AllowedTime,
                Anonymous = test.Anonymous,
                CreationDate = test.CreationDate,
                GlobalAvailability = test.GlobalAvailability,
                Interview = test.Interview,
                QuestionCount = test.QuestionCount
            }).AsEnumerable();
            List<DalTest> result = users.Where(user => func(user)).ToList();
            return result;
        }

        public DalTest GetById(int key)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            var test = context.Set<Tests>().FirstOrDefault(t => t.id == key);
            return new DalTest()
            {
                Id = test.id,
                AuthorId = test.AuthorId,
                Name = test.Name,
                Description = test.Description,
                AllowedTime = test.AllowedTime,
                Anonymous = test.Anonymous,
                CreationDate = test.CreationDate,
                GlobalAvailability = test.GlobalAvailability,
                Interview = test.Interview,
                QuestionCount = test.QuestionCount
            };
        }

        public void Create(DalTest e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Tests test = new Tests()
            {
                id = e.Id,
                AuthorId = e.AuthorId,
                Name = e.Name,
                Description = e.Description,
                AllowedTime = e.AllowedTime,
                Anonymous = e.Anonymous,
                CreationDate = e.CreationDate,
                GlobalAvailability = e.GlobalAvailability,
                Interview = e.Interview,
                QuestionCount = e.QuestionCount
            };
            context.Set<Tests>().Add(test);
        }

        public void Delete(DalTest e)
        {
            context.Database.Connection.Open();
            Tests test = context.Set<Tests>().FirstOrDefault(tests => tests.id == e.Id);
            context.Set<Tests>().Remove(test);
        }

        public void Update(DalTest entity)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Tests test = context.Set<Tests>().FirstOrDefault(tests => tests.id == entity.Id);
            //check existing
            test.Name = entity.Name;
            test.AllowedTime = entity.AllowedTime;
            test.Anonymous = entity.Anonymous;
            test.AuthorId = entity.AuthorId;
            test.CreationDate = entity.CreationDate;
            test.Description = entity.Description;
            test.GlobalAvailability = entity.GlobalAvailability;
            test.Interview = entity.Interview;
            test.QuestionCount = entity.QuestionCount;
            test.RequiredQuestionCount = entity.RequiredQuestionCount;
        }

        public IEnumerable<DalTest> SearchByString(string key)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            IEnumerable<Tests> tests = context.Set<Tests>().Where(t => t.Name.Contains(key) || t.Description.Contains(key)).AsEnumerable();
            List<DalTest> result = new List<DalTest>();
            foreach(Tests test in tests)
            {
                result.Add(new DalTest()
                {
                    Id = test.id,
                    AuthorId = test.AuthorId,
                    Name = test.Name,
                    Description = test.Description,
                    AllowedTime = test.AllowedTime,
                    Anonymous = test.Anonymous,
                    CreationDate = test.CreationDate,
                    GlobalAvailability = test.GlobalAvailability,
                    Interview = test.Interview,
                    QuestionCount = test.QuestionCount
                });
            }
            return result;
        }

        public IEnumerable<DalTest> GetByName(string name)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            IEnumerable<Tests> tests = context.Set<Tests>().Where(t => t.Name.Contains(name)).AsEnumerable();
            List<DalTest> result = new List<DalTest>();
            foreach (Tests test in tests)
            {
                result.Add(new DalTest()
                {
                    Id = test.id,
                    AuthorId = test.AuthorId,
                    Name = test.Name,
                    Description = test.Description,
                    AllowedTime = test.AllowedTime,
                    Anonymous = test.Anonymous,
                    CreationDate = test.CreationDate,
                    GlobalAvailability = test.GlobalAvailability,
                    Interview = test.Interview,
                    QuestionCount = test.QuestionCount
                });
            }
            return result;
        }

        public IEnumerable<DalTest> GetByAuthor(DalUser user)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            IEnumerable<Tests> tests = context.Set<Tests>().Where(t => t.AuthorId == user.Id).AsEnumerable();
            List<DalTest> result = new List<DalTest>();
            foreach (Tests test in tests)
            {
                result.Add(new DalTest()
                {
                    Id = test.id,
                    AuthorId = test.AuthorId,
                    Name = test.Name,
                    Description = test.Description,
                    AllowedTime = test.AllowedTime,
                    Anonymous = test.Anonymous,
                    CreationDate = test.CreationDate,
                    GlobalAvailability = test.GlobalAvailability,
                    Interview = test.Interview,
                    QuestionCount = test.QuestionCount
                });
            }
            return result;
        }

        public IEnumerable<DalTest> GetEarlierThanDate(DateTime date)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            IEnumerable<Tests> tests = context.Set<Tests>().Where(t => t.CreationDate <= date).AsEnumerable();
            List<DalTest> result = new List<DalTest>();
            foreach (Tests test in tests)
            {
                result.Add(new DalTest()
                {
                    Id = test.id,
                    AuthorId = test.AuthorId,
                    Name = test.Name,
                    Description = test.Description,
                    AllowedTime = test.AllowedTime,
                    Anonymous = test.Anonymous,
                    CreationDate = test.CreationDate,
                    GlobalAvailability = test.GlobalAvailability,
                    Interview = test.Interview,
                    QuestionCount = test.QuestionCount
                });
            }
            return result;
        }

        public IEnumerable<DalTest> GetLaterThanDate(DateTime date)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            IEnumerable<Tests> tests = context.Set<Tests>().Where(t => t.CreationDate >= date).AsEnumerable();
            List<DalTest> result = new List<DalTest>();
            foreach (Tests test in tests)
            {
                result.Add(new DalTest()
                {
                    Id = test.id,
                    AuthorId = test.AuthorId,
                    Name = test.Name,
                    Description = test.Description,
                    AllowedTime = test.AllowedTime,
                    Anonymous = test.Anonymous,
                    CreationDate = test.CreationDate,
                    GlobalAvailability = test.GlobalAvailability,
                    Interview = test.Interview,
                    QuestionCount = test.QuestionCount
                });
            }
            return result;
        }

        public IEnumerable<DalTest> GetInterviews()
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            IEnumerable<Tests> tests = context.Set<Tests>().Where(t => t.Interview).AsEnumerable();
            List<DalTest> result = new List<DalTest>();
            foreach (Tests test in tests)
            {
                result.Add(new DalTest()
                {
                    Id = test.id,
                    AuthorId = test.AuthorId,
                    Name = test.Name,
                    Description = test.Description,
                    AllowedTime = test.AllowedTime,
                    Anonymous = test.Anonymous,
                    CreationDate = test.CreationDate,
                    GlobalAvailability = test.GlobalAvailability,
                    Interview = test.Interview,
                    QuestionCount = test.QuestionCount
                });
            }
            return result;
        }

        public IEnumerable<DalTest> GetTests()
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            IEnumerable<Tests> tests = context.Set<Tests>().Where(t => !t.Interview).AsEnumerable();
            List<DalTest> result = new List<DalTest>();
            foreach (Tests test in tests)
            {
                result.Add(new DalTest()
                {
                    Id = test.id,
                    AuthorId = test.AuthorId,
                    Name = test.Name,
                    Description = test.Description,
                    AllowedTime = test.AllowedTime,
                    Anonymous = test.Anonymous,
                    CreationDate = test.CreationDate,
                    GlobalAvailability = test.GlobalAvailability,
                    Interview = test.Interview,
                    QuestionCount = test.QuestionCount
                });
            }
            return result;
        }

        public bool AllowedUser(DalTest test, DalUser user)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Users ormuser = context.Set<Users>().FirstOrDefault(users => users.id == user.Id);
            return context.Set<Tests>().Any(t => t.AllowedUsers.Contains(ormuser) && t.id == test.Id);
        }

        public void SetCoauthor(DalUser user, DalTest test)
        {
            context.Database.Connection.Open();
            Users ormuser = context.Set<Users>().FirstOrDefault(users => users.id == user.Id);
            Tests ormtest = context.Set<Tests>().FirstOrDefault(tests => tests.id == test.Id);
            ormtest.CoAuthors.Add(ormuser);
        }
    }
}
