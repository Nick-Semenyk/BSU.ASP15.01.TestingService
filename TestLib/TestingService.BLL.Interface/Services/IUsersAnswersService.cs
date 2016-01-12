using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;

namespace TestingService.BLL.Interface.Services
{
    public interface IUsersAnswersService
    {
        IEnumerable<UsersAnswersEntity> GetAll();
        UsersAnswersEntity GetById(int key);
        void AddAnswer(UsersAnswersEntity e);
        void DeleteAnswer(UsersAnswersEntity e);
        void UpdateAnswer(UsersAnswersEntity e);
        IEnumerable<UsersAnswersEntity> GetByUsersTest(UsersTestsEntity usersTest);
    }
}
