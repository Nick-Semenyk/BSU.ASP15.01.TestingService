using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using TestingService.DAL.Interface.Entities;
using TestingService.DAL.Interface.Repositories;
using TestingService.ORM;

namespace TestingService.DAL.Repositories
{
    public class RoleRepository:IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }

        public IEnumerable<DalRole> GetAll()
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<Roles>().Select(role => new DalRole()
            {
                Id = role.id,
                Name = role.Name
            });
        }

        public DalRole GetById(int key)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            var role = context.Set<Roles>().FirstOrDefault(r => r.id == key);
            return new DalRole()
            {
                Id = role.id,
                Name = role.Name
            };
        }

        public void Create(DalRole e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Roles role = new Roles()
            {
                id = e.Id,
                Name = e.Name
            };
            context.Set<Roles>().Add(role);
        }

        public void Delete(DalRole e)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Roles role = context.Set<Roles>().FirstOrDefault(r => r.id == e.Id);
            context.Set<Roles>().Remove(role);
        }

        public void Update(DalRole entity)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            Roles role = context.Set<Roles>().FirstOrDefault(r => r.id == entity.Id);
            role.Name = entity.Name;
        }

        public IEnumerable<DalRole> GetUsersRoles(DalUser user)
        {
            if (context.Database.Connection.State != ConnectionState.Open)
                context.Database.Connection.Open();
            return context.Set<Roles>().
                Where(role => role.Users.Any(users => users.id == user.Id))
                .AsEnumerable()
                .Select(roles => new DalRole() {Id = roles.id, Name = roles.Name});
        }
    }
}
