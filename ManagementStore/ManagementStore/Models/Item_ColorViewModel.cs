using ManagementStore.Business.Item_Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementStore.Models
{
    public class Item_ColorViewModel
    {
        public IEnumerable<Item_ColorModel> ListItem_ColorModel { get; set; }
        public string DisplayPage { get; set; }
        public int CountPage { get; set; }
        public Item_ColorModel Item_ColorModel { get; set; }
    }
}