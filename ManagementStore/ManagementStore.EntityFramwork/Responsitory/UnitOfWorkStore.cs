using ManagementStore.EntityFramwork.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.EntityFramwork.Responsitory
{
    public class UnitOfWorkStore : IUnitOfWorkStore
    {
        private readonly IDatabaseFactory databaseFactory;
        private BangAn_2017Entities dataContext;
        private bool disposed;

        public UnitOfWorkStore(IDatabaseFactory databaseFactory)
        {
            // _databaseFactory = databaseFactory;
            this.databaseFactory = new DatabaseFactory();
            dataContext = this.databaseFactory.GetDbContext();
        }

        public UnitOfWorkStore()
        {
            databaseFactory = new DatabaseFactory();
            dataContext = databaseFactory.GetDbContext();
        }

        public BangAn_2017Entities DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.GetDbContext()); }
        }

        public IRepository<T> GetRepository<T>()
            where T : class
        {
            return new Repository<T>(dataContext);
        }

        public int Save()
        {
            if (dataContext.GetValidationErrors().Any())
            {
                throw new Exception(dataContext.GetValidationErrors().ToList()[0].ValidationErrors.ToList()[0].ErrorMessage);
            }

            return DataContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dataContext.Dispose();
                    disposed = true;
                }
            }

            disposed = false;
        }
    }
}
