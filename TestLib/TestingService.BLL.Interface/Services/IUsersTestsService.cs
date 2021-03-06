﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;

namespace TestingService.BLL.Interface.Services
{
    public interface IUsersTestsService
    {
        IEnumerable<UsersTestsEntity> GetAll();
        IEnumerable<UsersTestsEntity> GetByPredicate(Expression<Func<UsersTestsEntity, bool>> f);
        UsersTestsEntity GetById(int key);
        void AddTesting(UsersTestsEntity e);
        void DeleteTesting(UsersTestsEntity e);
        void UpdateTesting(UsersTestsEntity e);
        bool IsUserTested(UserEntity user);
        void StartTest(UserEntity user, TestEntity test);
        void FinishTest(UserEntity user);
        UsersTestsEntity GetLastResult(UserEntity user);
    }
}
