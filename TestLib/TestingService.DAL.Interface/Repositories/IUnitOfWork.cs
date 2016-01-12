using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.DAL.Interface.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
