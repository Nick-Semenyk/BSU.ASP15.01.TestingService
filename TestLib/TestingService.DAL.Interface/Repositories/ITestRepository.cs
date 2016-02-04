using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Interface.Entities;

namespace TestingService.DAL.Interface.Repositories
{
    public interface ITestRepository:IRepository<DalTest>
    {
        IEnumerable<DalTest> SearchByString(string key);
        IEnumerable<DalTest> GetByName(string name);
        IEnumerable<DalTest> GetByAuthor(DalUser user);
        IEnumerable<DalTest> GetEarlierThanDate(DateTime date);
        IEnumerable<DalTest> GetLaterThanDate(DateTime date);
        IEnumerable<DalTest> GetInterviews();
        IEnumerable<DalTest> GetTests();

        bool AllowedUser(DalTest test, DalUser user);
        void SetCoauthor(DalUser user, DalTest test);
    }
}
