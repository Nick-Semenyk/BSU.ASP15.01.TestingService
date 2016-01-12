using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Interface.Entities;

namespace TestingService.DAL.Interface.Repositories
{
    public interface IUserRepository:IRepository<DalUser>
    {
        DalUser GetByLogin(string login);
        bool ExistUser(string login);
        IEnumerable<DalUser> GetByRole(DalRole role);
        void SetRole(DalUser user, DalRole role);

        IEnumerable<DalUser> GetCoauthors(DalTest test);
    }
}
