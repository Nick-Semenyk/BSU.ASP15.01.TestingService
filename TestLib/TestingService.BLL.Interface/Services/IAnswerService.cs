using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;

namespace TestingService.BLL.Interface.Services
{
    public interface IAnswerService
    {
        AnswerEntity GetUserEntity(int id); 
        IEnumerable<AnswerEntity> GetAllUsers();
        void CreateAnswer(AnswerEntity user);
        void DeleteAnswer(AnswerEntity user);
        void UpdateAnswer(AnswerEntity user);
        bool ExistAnswer(QuestionEntity question, AnswerEntity answer);
        IEnumerable<AnswerEntity> GetAllAnswers(QuestionEntity question);
    }
}
