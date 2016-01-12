using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Mappers;
using TestingService.DAL.Interface.Repositories;

namespace TestingService.BLL.Services
{
    public class UsersAnswersService:IUsersAnswersService
    {
        private readonly IUnitOfWork uow; 
        private readonly IUsersAnswersRepository usersAnswersRepository;

        public UsersAnswersService(IUnitOfWork uow, IUsersAnswersRepository usersAnswersRepository)
        {
            this.uow = uow;
            this.usersAnswersRepository = usersAnswersRepository;
        }

        public IEnumerable<UsersAnswersEntity> GetAll()
        {
            return usersAnswersRepository.GetAll().Select(answers => answers.ToBllUsersAnswers());
        }

        public UsersAnswersEntity GetById(int key)
        {
            return usersAnswersRepository.GetById(key).ToBllUsersAnswers();
        }

        public void AddAnswer(UsersAnswersEntity e)
        {
            usersAnswersRepository.Create(e.ToDalUsersAnswers());
            uow.Commit();
        }

        public void DeleteAnswer(UsersAnswersEntity e)
        {
            usersAnswersRepository.Delete(e.ToDalUsersAnswers());
            uow.Commit();
        }

        public void UpdateAnswer(UsersAnswersEntity e)
        {
            usersAnswersRepository.Update(e.ToDalUsersAnswers());
            uow.Commit();
        }

        public IEnumerable<UsersAnswersEntity> GetByUsersTest(UsersTestsEntity usersTest)
        {
            return
                usersAnswersRepository.GetByUsersTest(usersTest.ToDalUsersTest())
                    .Select(answers => answers.ToBllUsersAnswers());
        }
    }
}
