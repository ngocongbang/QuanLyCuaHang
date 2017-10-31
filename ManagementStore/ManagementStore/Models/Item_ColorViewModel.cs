using ManagementStore.Business.Item_Colors;
using ManagementStore.Business.Item_Materials;
using ManagementStore.Business.Item_Sizes;
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
    public class Item_SizeViewModel
    {
        public IEnumerable<Item_SizeModel> ListItem_SizeModel { get; set; }
        public string DisplayPage { get; set; }
        public int CountPage { get; set; }
        public Item_SizeModel Item_SizeModel { get; set; }
    }
    public class Item_MaterialViewModel
    {
        public IEnumerable<Item_MaterialModel> ListItem_MaterialModel { get; set; }
        public string DisplayPage { get; set; }
        public int CountPage { get; set; }
        public Item_MaterialModel Item_MaterialModel { get; set; }
    }
}