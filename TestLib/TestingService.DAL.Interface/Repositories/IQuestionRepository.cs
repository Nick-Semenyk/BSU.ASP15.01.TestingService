using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Interface.Entities;

namespace TestingService.DAL.Interface.Repositories
{
    public interface IQuestionRepository:IRepository<DalQuestion>
    {
        IEnumerable<DalQuestion> GetAllTestQuestions(DalTest test);
    }
}
