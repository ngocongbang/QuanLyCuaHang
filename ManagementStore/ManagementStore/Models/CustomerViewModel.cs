using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManagementStore.Business.Customers;

namespace ManagementStore.Models
{
    public class CustomerViewModel
    {
        public IEnumerable<CustomerModel> ListCustomerModel { get; set; }
        public CustomerModel CustomerModel { get; set; }
    }
}