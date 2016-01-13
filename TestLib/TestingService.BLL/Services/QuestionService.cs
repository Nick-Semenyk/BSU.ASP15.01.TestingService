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
    public class QuestionService:IQuestionService
    {
        private readonly IUnitOfWork uow; 
        private readonly IQuestionRepository questionRepository;

        public QuestionService(IUnitOfWork uow, IQuestionRepository questionRepository)
        {
            this.uow = uow;
            this.questionRepository = questionRepository;
        }

        public IEnumerable<QuestionEntity> GetAll()
        {
            return questionRepository.GetAll().Select(question => question.ToBllQuestion());
        }

        public IEnumerable<QuestionEntity> GetByPredicate(Expression<Func<QuestionEntity, bool>> f)
        {
            Func<QuestionEntity, bool> func = f.Compile();
            return questionRepository.GetByPredicate(question => func(question.ToBllQuestion())).Select(question => question.ToBllQuestion());
        }

        public QuestionEntity GetById(int key)
        {
            return questionRepository.GetById(key).ToBllQuestion();
        }

        public void CreateQuestion(QuestionEntity e)
        {
            questionRepository.Create(e.ToDalQuestion());
            uow.Commit();
        }

        public void DeleteQuestion(QuestionEntity e)
        {
            questionRepository.Delete(e.ToDalQuestion());
            uow.Commit();
        }

        public void UpdateQuestion(QuestionEntity e)
        {
            questionRepository.Update(e.ToDalQuestion());
            uow.Commit();
        }

        public IEnumerable<QuestionEntity> GetAllTestQuestions(TestEntity test)
        {
            return questionRepository.GetAllTestQuestions(test.ToDalTest()).Select(question => question.ToBllQuestion());
        }
    }
}
