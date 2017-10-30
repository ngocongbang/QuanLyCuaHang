using ManagementStore.Business.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementStore.Models
{
    public class ItemViewModel
    {
        public IEnumerable<ItemModel> ListItemModel { get; set; }
        public string DisplayPage { get; set; }
        public int CountPage { get; set; }
        public ItemModel ItemModel { get; set; }
    }
}