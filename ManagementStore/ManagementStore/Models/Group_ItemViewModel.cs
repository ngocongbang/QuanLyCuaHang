using ManagementStore.Business.GroupItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementStore.Models
{
    public class Group_ItemViewModel
    {
        public IEnumerable<GroupItemModel> ListGroup_ItemModel { get; set; }
        public string DisplayPage { get; set; }
        public int CountPage { get; set; }
        public GroupItemModel Group_ItemModel { get; set; }
    }
}