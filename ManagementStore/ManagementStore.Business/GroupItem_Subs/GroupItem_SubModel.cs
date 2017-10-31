using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.GroupItem_Subs
{
  public  class GroupItem_SubModel
    {
        public int GroupItemSub_ID { get; set; }
        public string GroupItemSub_Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> GroupItem_ID { get; set; }     
    }
}
