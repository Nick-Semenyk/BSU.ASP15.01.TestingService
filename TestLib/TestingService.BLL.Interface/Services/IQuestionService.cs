using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;

namespace TestingService.BLL.Interface.Services
{
    public interface IQuestionService
    {
        IEnumerable<QuestionEntity> GetAll();
        IEnumerable<QuestionEntity> GetByPredicate(Expression<Func<QuestionEntity, bool>> f);
        QuestionEntity GetById(int key);
        void CreateQuestion(QuestionEntity e);
        void DeleteQuestion(QuestionEntity e);
        void UpdateQuestion(QuestionEntity e);
        IEnumerable<QuestionEntity> GetAllTestQuestions(TestEntity test);
    }
}
