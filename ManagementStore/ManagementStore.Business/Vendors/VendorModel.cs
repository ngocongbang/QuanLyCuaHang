using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Vendors
{
   public class VendorModel
    {
        public int Vendor_ID { get; set; }
        public string Vendor_Code { get; set; }
        public string Name { get; set; }
        public string Company_Name { get; set; }
        public string Tax_Code { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Region { get; set; }
        public string CommuneWard { get; set; }
        public string Email { get; set; }
        public string Group_Vendor { get; set; }
        public string Note { get; set; }
    }
}
