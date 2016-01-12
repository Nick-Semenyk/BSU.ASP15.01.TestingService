using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TestingService.DAL.Interface.Entities;
using TestingService.DAL.Interface.Repositories;
using TestingService.ORM;

namespace TestingService.DAL.Repositories
{
    public class QuestionRepository:IQuestionRepository
    {
        private readonly DbContext context;

        public QuestionRepository(DbContext uow)
        { 
            this.context = uow;
        }

        public IEnumerable<DalQuestion> GetAll()
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<Questions>().Select(question => new DalQuestion()
            {
                Id = question.id,
                QuestionNumberInTest = question.QuestionNumberInTest,
                TestId = question.TestId,
                QuestionStructure = question.QuestionStructure
            });
        }

        public DalQuestion GetById(int key)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            var question = context.Set<Questions>().FirstOrDefault(q => q.id == key);
            return new DalQuestion()
            {
                Id = question.id,
                QuestionNumberInTest = question.QuestionNumberInTest,
                TestId = question.TestId,
                QuestionStructure = question.QuestionStructure
            };
        }

        public void Create(DalQuestion e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Questions question = new Questions()
            {
                id = e.Id,
                QuestionNumberInTest = e.QuestionNumberInTest,
                TestId = e.TestId,
                QuestionStructure = e.QuestionStructure
            };
            context.Set<Questions>().Add(question);
        }

        public void Delete(DalQuestion e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Questions question = context.Set<Questions>().FirstOrDefault(q => q.id == e.Id);
            context.Set<Questions>().Remove(question);
        }

        public void Update(DalQuestion entity)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Questions question = context.Set<Questions>().FirstOrDefault(q => q.id == entity.Id);
            question.QuestionNumberInTest = entity.QuestionNumberInTest;
            question.QuestionStructure = entity.QuestionStructure;
            question.TestId = entity.TestId;
        }

        public IEnumerable<DalQuestion> GetAllTestQuestions(DalTest test)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            IEnumerable<Questions> questions = context.Set<Questions>().Where(q => q.TestId == test.Id).AsEnumerable();
            List<DalQuestion> result = new List<DalQuestion>();
            foreach(Questions question in questions)
            {
                result.Add(new DalQuestion()
                {
                    Id = question.id,
                    QuestionNumberInTest = question.QuestionNumberInTest,
                    QuestionStructure = question.QuestionStructure,
                    TestId = question.TestId
                });
            }
            return result;
        }
    }
}
