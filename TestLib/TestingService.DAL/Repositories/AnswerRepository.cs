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
    public class AnswerRepository:IAnswerRepository
    {
        private readonly DbContext context;

        public AnswerRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalAnswer> GetAll()
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<Answers>().Select(answer => new DalAnswer()
            { 
                Id = answer.id,
                AnswerStructure = answer.AnswerStructure,
                AnswerValue = answer.AnswerValue,
                QuestionId = answer.QuestionId
            });
        }

        public IEnumerable<DalAnswer> GetByPredicate(Expression<Func<DalAnswer, bool>> f)
        {
            Func<DalAnswer, bool> func = f.Compile();
            IEnumerable<DalAnswer> users = context.Set<Answers>().Where(u => true).AsEnumerable().Select(answer => new DalAnswer()
            {
                Id = answer.id,
                AnswerStructure = answer.AnswerStructure,
                AnswerValue = answer.AnswerValue,
                QuestionId = answer.QuestionId
            }).AsEnumerable();
            List<DalAnswer> result = users.Where(user => func(user)).ToList();
            return result;
        }

        public DalAnswer GetById(int key)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            var answer = context.Set<Answers>().FirstOrDefault(a => a.id == key);
            if (answer == null)
                return null;
            return new DalAnswer()
            {
                Id = answer.id,
                AnswerStructure = answer.AnswerStructure,
                AnswerValue = answer.AnswerValue,
                QuestionId = answer.QuestionId
            };
        }

        public void Create(DalAnswer e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Answers answer = new Answers()
            {
                id = e.Id,
                AnswerStructure = e.AnswerStructure,
                AnswerValue = e.AnswerValue,
                QuestionId = e.QuestionId
            };
            context.Set<Answers>().Add(answer);
        }

        public void Delete(DalAnswer e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Answers answer = context.Set<Answers>().FirstOrDefault(a => a.id == e.Id);
            context.Set<Answers>().Remove(answer);
        }

        public void Update(DalAnswer entity)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Answers answer = context.Set<Answers>().FirstOrDefault(a => a.id == entity.Id);
            answer.AnswerStructure = entity.AnswerStructure;
            answer.AnswerValue = entity.AnswerValue;
            answer.QuestionId = entity.QuestionId;
        }

        public bool ExistAnswer(DalQuestion question, DalAnswer answer)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<Answers>()
                .Any(answers => answers.AnswerStructure == answer.AnswerStructure 
                && answers.QuestionId == question.Id);
        }

        public IEnumerable<DalAnswer> GetAllAnswers(DalQuestion question)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            IEnumerable<Answers> ormanswers = context.Set<Answers>().Where(answers => answers.QuestionId == question.Id).AsEnumerable();
            List<DalAnswer> result = new List<DalAnswer>();
            foreach(Answers a in ormanswers)
            {
                result.Add(new DalAnswer()
                {
                    AnswerStructure = a.AnswerStructure,
                    AnswerValue = a.AnswerValue,
                    Id = a.id,
                    QuestionId = a.QuestionId
                });
            }
            return result;
        }
    }
}
