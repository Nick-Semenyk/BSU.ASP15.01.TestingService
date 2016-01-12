using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.Interface.Entities;
using TestingService.DAL.Interface.Entities;

namespace TestingService.BLL.Mappers
{
    public static class BllEntityMappers
    {
        public static DalUser ToDalUser(this UserEntity e)
        {
            return new DalUser()
            {
                Id = e.Id,
                Email = e.Email,
                Login = e.Login,
                FirstName = e.FirstName,
                SecondName = e.SecondName,
                ThirdName = e.ThirdName,
                PasswordHash = e.PasswordHash
            };
        }

        public static UserEntity ToBllUser(this DalUser e)
        {
            return new UserEntity()
            {
                Id = e.Id,
                Email = e.Email,
                Login = e.Login,
                FirstName = e.FirstName,
                SecondName = e.SecondName,
                ThirdName = e.ThirdName,
                PasswordHash = e.PasswordHash
            };
        }

        public static TestEntity ToBllTest(this DalTest e)
        {
            return new TestEntity()
            {
                Id = e.Id,
                AllowedTime = e.AllowedTime,
                Anonymous = e.Anonymous,
                AuthorId = e.AuthorId,
                CreationDate = e.CreationDate,
                Description = e.Description,
                GlobalAvailability = e.GlobalAvailability,
                Interview = e.Interview,
                Name = e.Name,
                QuestionCount = e.QuestionCount
            };
        }

        public static DalTest ToDalTest(this TestEntity e)
        {
            return new DalTest()
            {
                Id = e.Id,
                AllowedTime = e.AllowedTime,
                Anonymous = e.Anonymous,
                AuthorId = e.AuthorId,
                CreationDate = e.CreationDate,
                Description = e.Description,
                GlobalAvailability = e.GlobalAvailability,
                Interview = e.Interview,
                Name = e.Name,
                QuestionCount = e.QuestionCount
            };
        }

        public static DalAnswer ToDalAnswer(this AnswerEntity e)
        {
            return new DalAnswer()
            {
                Id = e.Id,
                AnswerStructure = e.AnswerStructure,
                AnswerValue = e.AnswerValue,
                QuestionId = e.QuestionId
            };
        }

        public static AnswerEntity ToBllAnswer(this DalAnswer e)
        {
            return new AnswerEntity()
            {
                Id = e.Id,
                AnswerStructure = e.AnswerStructure,
                AnswerValue = e.AnswerValue,
                QuestionId = e.QuestionId
            };
        }

        public static QuestionEntity ToBllQuestion(this DalQuestion e)
        {
            return new QuestionEntity()
            {
                Id = e.Id,
                QuestionNumberInTest = e.QuestionNumberInTest,
                QuestionStructure = e.QuestionStructure,
                TestId = e.TestId
            };
        }

        public static DalQuestion ToDalQuestion(this QuestionEntity e)
        {
            return new DalQuestion()
            {
                Id = e.Id,
                QuestionNumberInTest = e.QuestionNumberInTest,
                QuestionStructure = e.QuestionStructure,
                TestId = e.TestId
            };
        }

        public static RoleEntity ToBllRole(this DalRole e)
        {
            return new RoleEntity()
            {
                Id = e.Id,
                Name = e.Name
            };
        }

        public static DalRole ToDalRole(this RoleEntity e)
        {
            return new DalRole()
            {
                Id = e.Id,
                Name = e.Name
            };
        }

        public static UsersAnswersEntity ToBllUsersAnswers(this DalUsersAnswers e)
        {
            return new UsersAnswersEntity()
            {
                QuestionId = e.QuestionId,
                AnswerStructure = e.AnswerStructure,
                BeginningTime = e.BeginningTime,
                EndingTime = e.EndingTime,
                UserTestId = e.UserTestId
            };
        }

        public static DalUsersAnswers ToDalUsersAnswers(this UsersAnswersEntity e)
        {
            return new DalUsersAnswers()
            {
                QuestionId = e.QuestionId,
                AnswerStructure = e.AnswerStructure,
                BeginningTime = e.BeginningTime,
                EndingTime = e.EndingTime,
                UserTestId = e.UserTestId
            };
        }

        public static UsersTestsEntity ToBllUsersTest(this DalUsersTest e)
        {
            return new UsersTestsEntity()
            {
                Id = e.Id,
                BeginningTime = e.BeginningTime,
                EndingTime = e.EndingTime,
                MarkForTest = e.MarkForTest,
                RightAnswersCount = e.RightAnswersCount,
                TestId = e.TestId,
                UserId = e.UserId
            };
        }

        public static DalUsersTest ToDalUsersTest(this UsersTestsEntity e)
        {
            return new DalUsersTest()
            {
                Id = e.Id,
                BeginningTime = e.BeginningTime,
                EndingTime = e.EndingTime,
                MarkForTest = e.MarkForTest,
                RightAnswersCount = e.RightAnswersCount,
                TestId = e.TestId,
                UserId = e.UserId
            };
        }
    }
}
