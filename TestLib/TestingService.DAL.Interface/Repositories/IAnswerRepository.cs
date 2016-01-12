using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Interface.Entities;

namespace TestingService.DAL.Interface.Repositories
{
    public interface IAnswerRepository:IRepository<DalAnswer>
    {
        bool ExistAnswer(DalQuestion question, DalAnswer answer);
        IEnumerable<DalAnswer> GetAllAnswers(DalQuestion question);
    }
}
