using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Mappers;
using TestingService.DAL.Interface.Entities;
using TestingService.DAL.Interface.Repositories;

namespace TestingService.BLL.Services
{
    public class AnswerService:IAnswerService
    {
        private readonly IUnitOfWork uow;
        private readonly IAnswerRepository answerRepository;
         
        public AnswerService(IUnitOfWork uow, IAnswerRepository answerRepository)
        {
            this.uow = uow;
            this.answerRepository = answerRepository;
        }

        public AnswerEntity GetAnswerEntity(int id)
        {
            return answerRepository.GetById(id).ToBllAnswer();
        }

        public IEnumerable<AnswerEntity> GetAllAnswers()
        {
            return answerRepository.GetAll().Select(answer => answer.ToBllAnswer());
        }

        public IEnumerable<AnswerEntity> GetByPredicate(Expression<Func<AnswerEntity, bool>> f)
        {
            Func<AnswerEntity, bool> func = f.Compile();
            return answerRepository.GetByPredicate(answer => func(answer.ToBllAnswer())).Select(answer => answer.ToBllAnswer());
        }

        public void CreateAnswer(AnswerEntity answer)
        {
            answerRepository.Create(answer.ToDalAnswer());
            uow.Commit();
        }

        public void DeleteAnswer(AnswerEntity answer)
        {
            answerRepository.Delete(answer.ToDalAnswer());
            uow.Commit();
        }

        public void UpdateAnswer(AnswerEntity answer)
        {
            answerRepository.Update(answer.ToDalAnswer());
            uow.Commit();
        }

        public bool ExistAnswer(QuestionEntity question, AnswerEntity answer)
        {
            return answerRepository.ExistAnswer(question.ToDalQuestion(), answer.ToDalAnswer());
        }

        public IEnumerable<AnswerEntity> GetAllAnswers(QuestionEntity question)
        {
            return answerRepository.GetAllAnswers(question.ToDalQuestion()).Select(answer => answer.ToBllAnswer());
        }
    }
}
