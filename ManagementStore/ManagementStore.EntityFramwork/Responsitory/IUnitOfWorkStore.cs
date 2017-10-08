using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.EntityFramwork.Responsitory
{
    public interface IUnitOfWorkStore : IDisposable
    {
        IRepository<T> GetRepository<T>()
            where T : class;
        int Save();
    }
}
