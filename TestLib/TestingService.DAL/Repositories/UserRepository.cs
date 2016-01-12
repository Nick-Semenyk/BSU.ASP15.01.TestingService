using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TestingService.DAL.Interface.Entities;
using TestingService.DAL.Interface.Repositories;
using TestingService.ORM;

namespace TestingService.DAL.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            this.context = uow;
        }
        
        public IEnumerable<DalUser> GetAll()
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<Users>().Select(user => new DalUser()
            {
                Id = user.id,
                Email = user.Email,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                ThirdName = user.ThirdName,
                Login = user.Login,
                PasswordHash = user.PasswordHash
            });
        }

        public DalUser GetById(int key)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            var ormuser = context.Set<Users>().FirstOrDefault(user => user.id == key);
            return new DalUser()
            {
                Id = ormuser.id,
                Email = ormuser.Email,
                FirstName = ormuser.FirstName,
                SecondName = ormuser.SecondName,
                ThirdName = ormuser.ThirdName,
                Login = ormuser.Login,
                PasswordHash = ormuser.PasswordHash
            };
        }
        
        public void Create(DalUser e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Users user = new Users()
            {
                Login = e.Login,
                Email = e.Email,
                PasswordHash = e.PasswordHash,
                FirstName = e.FirstName,
                SecondName = e.SecondName,
                ThirdName = e.ThirdName
            };
            context.Set<Users>().Add(user);
        }

        public void Delete(DalUser e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Users user = context.Set<Users>().FirstOrDefault(users => users.id == e.Id);
            context.Set<Users>().Remove(user);
        }

        public void Update(DalUser entity)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Users user = context.Set<Users>().FirstOrDefault(users => users.id == entity.Id);
            //check existing
            user.Login = entity.Login;
            user.Email = entity.Email;
            user.FirstName = entity.FirstName;
            user.SecondName = entity.SecondName;
            user.ThirdName = entity.ThirdName;
            user.PasswordHash = entity.PasswordHash;
        }

        public DalUser GetByLogin(string login)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Users user = context.Set<Users>().FirstOrDefault(u => u.Login == login);
            return new DalUser()
            {
                Id = user.id,
                Email = user.Email,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                ThirdName = user.ThirdName,
                Login = user.Login,
                PasswordHash = user.PasswordHash
            };
        }

        public bool ExistUser(string login)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<Users>().Where(user => user.Login == login).AsEnumerable().Any();
        }

        public IEnumerable<DalUser> GetByRole(DalRole role)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Roles r = context.Set<Roles>().FirstOrDefault(roles => roles.id == role.Id);
            IEnumerable<Users> users = context.Set<Users>().Where(user => user.Roles.Contains(r)).AsEnumerable();
            List<DalUser> result = new List<DalUser>();
            foreach (Users u in users)
            {
                result.Add(new DalUser()
                {
                    Id = u.id,
                    Login = u.Login,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    SecondName = u.SecondName,
                    ThirdName = u.ThirdName,
                    PasswordHash = u.PasswordHash
                });
            }
            return result;
        }

        public void SetRole(DalUser user, DalRole role)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Users ormuser = context.Set<Users>().FirstOrDefault(users => users.id == user.Id);
            Roles ormrole = context.Set<Roles>().FirstOrDefault(roles => roles.id == role.Id);
            ormuser.Roles.Add(ormrole);
        }

        public IEnumerable<DalUser> GetCoauthors(DalTest test)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Tests ormtest = context.Set<Tests>().FirstOrDefault(tests => tests.id == test.Id);
            IEnumerable<Users> users = ormtest.CoAuthors.AsEnumerable();
            List<DalUser> result = new List<DalUser>();
            foreach (Users u in users)
            {
                result.Add(new DalUser()
                {
                    Id = u.id,
                    Login = u.Login,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    SecondName = u.SecondName,
                    ThirdName = u.ThirdName,
                    PasswordHash = u.PasswordHash
                });
            }
            return result;
        }
    }
}
