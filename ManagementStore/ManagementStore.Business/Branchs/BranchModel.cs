using ManagementStore.Business.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Branchs
{
    public class BranchModel
    {
        public int Branch_ID { get; set; }
        public string Branch_Name { get; set; }
        public string Region { get; set; }
        public string CommuneWard { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public  IList<EmployeeModel> Employees { get; set; }
    }
}
