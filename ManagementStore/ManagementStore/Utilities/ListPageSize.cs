using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementStore.Utilities
{
    /// <summary>
    /// Doi tuong trang
    /// </summary>
    public class ObjectPage
    {
        public string NumberID { get; set; }
        public int NumberValue { get; set; }
        public ObjectPage(string numberID, int numberValue)
        {
            NumberID = numberID;
            NumberValue = numberValue;
        }
    }
    public class ListPageSize
    {
       public static List<ObjectPage> GetListPageSize()
        {
            List<ObjectPage> list = new List<Utilities.ObjectPage>();
            list.Add(new ObjectPage("10", 10));
            list.Add(new ObjectPage("20", 20));
            list.Add(new ObjectPage("30", 30));
            list.Add(new ObjectPage("40", 40));
            list.Add(new ObjectPage("50", 50));
            return list;
        }
    }
}