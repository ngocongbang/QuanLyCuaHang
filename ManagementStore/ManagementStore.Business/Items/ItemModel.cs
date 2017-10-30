using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Items
{
    public class ItemModel
    {
        public int Item_ID { get; set; }
        public string Item_Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> GroupItem_ID { get; set; }
        public Nullable<int> GroupItem_Sub_ID { get; set; }
        public Nullable<decimal> AmountSale { get; set; }
        public Nullable<decimal> AmountOrigin { get; set; }
        public Nullable<int> Blance { get; set; }
        public Nullable<int> Item_Color_ID { get; set; }
        public Nullable<int> Item_Material_ID { get; set; }
        public Nullable<int> Item_Size_ID { get; set; }
        public string Unit { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> AmountShip { get; set; }
    }
}
