using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Employees
{
   public class EmployeeModel
    {
        public int Employee_ID { get; set; }
        public string Employee_Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Possition { get; set; }
        public string Private_ID { get; set; }
        public Nullable<int> Branch_ID { get; set; }
    }
}
