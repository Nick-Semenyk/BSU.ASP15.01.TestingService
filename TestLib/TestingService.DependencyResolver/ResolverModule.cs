using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Web.Common;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Services;
using TestingService.DAL;
using TestingService.DAL.Interface.Repositories;
using TestingService.DAL.Repositories;
using TestingService.ORM;

namespace TestingService.DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<TestServiceDatabaseEntities>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<TestServiceDatabaseEntities>().InSingletonScope();
            }
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IAnswerRepository>().To<AnswerRepository>();
            kernel.Bind<IAnswerService>().To<AnswerService>();
            kernel.Bind<ITestRepository>().To<TestRepository>();
            kernel.Bind<ITestService>().To<TestService>();
            kernel.Bind<IQuestionRepository>().To<QuestionRepository>();
            kernel.Bind<IQuestionService>().To<QuestionService>();
            kernel.Bind<IUsersTestRepository>().To<UsersTestRepository>();
            kernel.Bind<IUsersTestsService>().To<UsersTestsService>();
            kernel.Bind<IUsersAnswersRepository>().To<UsersAnswersRepository>();
            kernel.Bind<IUsersAnswersService>().To<UsersAnswersService>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IRoleService>().To<RoleService>();
        }
    }
}
