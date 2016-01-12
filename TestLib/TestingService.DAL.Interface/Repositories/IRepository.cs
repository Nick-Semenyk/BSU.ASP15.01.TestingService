using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TestingService.DAL.Interface.Entities;

namespace TestingService.DAL.Interface.Repositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        void Create(TEntity e);
        void Delete(TEntity e);
        void Update(TEntity entity);
    }
}
