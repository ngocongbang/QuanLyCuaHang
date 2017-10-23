using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày sinh")]
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Category { get; set; }
        public string Company_Name { get; set; }
        public string Tax_Code { get; set; }
        public string Url { get; set; }
        public string Sex { get; set; }
        public string Note { get; set; }
    }
}
