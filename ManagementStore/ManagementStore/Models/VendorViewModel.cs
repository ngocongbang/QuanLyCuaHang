using ManagementStore.Business.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementStore.Models
{
    public class VendorViewModel
    {
        public IEnumerable<VendorModel> ListVendorModel { get; set; }
        public string DisplayPage { get; set; }
        public int CountPage { get; set; }
        public VendorModel VendorModel { get; set; }
    }
}