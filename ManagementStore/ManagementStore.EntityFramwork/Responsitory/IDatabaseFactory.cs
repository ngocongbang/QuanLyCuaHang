using ManagementStore.EntityFramwork.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.EntityFramwork.Responsitory
{
    public interface IDatabaseFactory
    {
        BangAn_2017Entities GetDbContext();
    }
}
