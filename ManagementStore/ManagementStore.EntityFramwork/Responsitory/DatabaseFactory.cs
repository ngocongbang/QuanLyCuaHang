using ManagementStore.EntityFramwork.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.EntityFramwork.Responsitory
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly BangAn_2017Entities dataContext;
        public DatabaseFactory()
        {
            dataContext = new BangAn_2017Entities();
        }

        public BangAn_2017Entities GetDbContext()
        {
            return dataContext;
        }
    }
}
