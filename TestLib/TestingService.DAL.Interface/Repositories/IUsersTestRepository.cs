using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Interface.Entities;

namespace TestingService.DAL.Interface.Repositories
{
    public interface IUsersTestRepository:IRepository<DalUsersTest>
    {
        bool IsUserTested(DalUser user);
        void StartTest(DalUser user, DalTest test);
        void FinishTest(DalUser user);
        DalUsersTest GetLastResult(DalUser user);
    }
}
