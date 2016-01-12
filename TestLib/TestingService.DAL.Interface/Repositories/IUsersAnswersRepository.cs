﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Interface.Entities;

namespace TestingService.DAL.Interface.Repositories
{
    public interface IUsersAnswersRepository : IRepository<DalUsersAnswers>
    {
        IEnumerable<DalUsersAnswers> GetByUsersTest(DalUsersTest usersTest);
    }
}