using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Customers
{
   public class CustomerModel
    {
        public int Customer_ID { get; set; }
        public string Customer_Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Category { get; set; }
        public string Company_Name { get; set; }
    }
}
