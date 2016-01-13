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
    public class UsersAnswersRepository:IUsersAnswersRepository
    {
        private readonly DbContext context;

        public UsersAnswersRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalUsersAnswers> GetAll()
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<UsersAnswers>().Select(ua => new DalUsersAnswers()
            {
                Id = 0,
                BeginningTime = ua.BeginningTime,
                AnswerStructure = ua.AnswerStructure,
                EndingTime = ua.EndingTime,
                QuestionId = ua.QuestionId,
                UserTestId = ua.UsersTestId
            });
        }

        public IEnumerable<DalUsersAnswers> GetByPredicate(Expression<Func<DalUsersAnswers, bool>> f)
        {
            Func<DalUsersAnswers, bool> func = f.Compile();
            IEnumerable<DalUsersAnswers> users = context.Set<UsersAnswers>().Where(u => true).AsEnumerable().Select(ua => new DalUsersAnswers()
            {
                Id = 0,
                BeginningTime = ua.BeginningTime,
                AnswerStructure = ua.AnswerStructure,
                EndingTime = ua.EndingTime,
                QuestionId = ua.QuestionId,
                UserTestId = ua.UsersTestId
            }).AsEnumerable();
            List<DalUsersAnswers> result = users.Where(user => func(user)).ToList();
            return result;
        }

        public DalUsersAnswers GetById(int key)
        {
            throw new NotImplementedException();
        }

        public void Create(DalUsersAnswers e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            UsersAnswers ua = new UsersAnswers()
            {
                AnswerStructure = e.AnswerStructure,
                BeginningTime = e.BeginningTime,
                EndingTime = e.EndingTime,
                QuestionId = e.QuestionId,
                UsersTestId = e.UserTestId
            };
            context.Set<UsersAnswers>().Add(ua);
        }

        public void Delete(DalUsersAnswers e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            UsersAnswers ua = context.Set<UsersAnswers>().
                FirstOrDefault(a => a.UsersTestId == e.UserTestId
                && a.QuestionId == e.QuestionId 
                && a.BeginningTime == e.BeginningTime);
            context.Set<UsersAnswers>().Remove(ua);
        }

        public void Update(DalUsersAnswers entity)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            UsersAnswers ua = context.Set<UsersAnswers>().
                FirstOrDefault(a => a.UsersTestId == entity.UserTestId
                && a.QuestionId == entity.QuestionId
                && a.BeginningTime == entity.BeginningTime);
            ua.AnswerStructure = entity.AnswerStructure;
            ua.EndingTime = entity.EndingTime;
        }

        public IEnumerable<DalUsersAnswers> GetByUsersTest(DalUsersTest usersTest)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            IEnumerable<UsersAnswers> ua = context.Set<UsersAnswers>().
                Where(a => a.UsersTestId == usersTest.Id);
            List<DalUsersAnswers> result = new List<DalUsersAnswers>();
            foreach(UsersAnswers u in ua)
            {
                result.Add(new DalUsersAnswers()
                {
                    AnswerStructure = u.AnswerStructure,
                    BeginningTime = u.BeginningTime,
                    EndingTime = u.EndingTime,
                    QuestionId = u.QuestionId,
                    UserTestId = u.UsersTestId
                });
            }
            return result;
        }
    }
}
