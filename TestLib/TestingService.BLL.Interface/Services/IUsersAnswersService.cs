using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;

namespace TestingService.BLL.Interface.Services
{
    public interface IUsersAnswersService
    {
        IEnumerable<UsersAnswersEntity> GetAll();
        IEnumerable<UsersAnswersEntity> GetByPredicate(Expression<Func<UsersAnswersEntity, bool>> f);
        UsersAnswersEntity GetById(int key);
        void AddAnswer(UsersAnswersEntity e);
        void DeleteAnswer(UsersAnswersEntity e);
        void UpdateAnswer(UsersAnswersEntity e);
        IEnumerable<UsersAnswersEntity> GetByUsersTest(UsersTestsEntity usersTest);
    }
}
