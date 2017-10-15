using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Vendors
{
   public class VendorModel
    {
        public int Vendor_ID { get; set; }
        [Display(Name = "Mã nhà cung cấp")]
        public string Vendor_Code { get; set; }
        [Display(Name ="Tên nhà cung cấp")]
        public string Name { get; set; }
        [Display(Name ="Tên công ty")]
        public string Company_Name { get; set; }
        [Display(Name ="Mã số thuế")]
        public string Tax_Code { get; set; }
        [Display(Name ="Điện thoại")]
        public string Phone { get; set; }
        public string Address { get; set; }
        [Display(Name ="Vùng miền")]
        public string Region { get; set; }
        [Display(Name ="Xã phường")]
        public string CommuneWard { get; set; }
        public string Email { get; set; }
        [Display(Name ="Nhóm nhà cung cấp")]
        public string Group_Vendor { get; set; }
        [Display(Name ="Ghi chú")]
        public string Note { get; set; }
    }
}
