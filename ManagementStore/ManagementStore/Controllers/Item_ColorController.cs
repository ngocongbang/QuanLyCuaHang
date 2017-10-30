using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Item_Colors;
using ManagementStore.Models;
using ManagementStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementStore.Controllers
{
    public class Item_ColorController : Controller
    {
        Item_ColorHandler _item_ColorHandler = new Item_ColorHandler();
        // GET: Item_Color
        public ActionResult Index()
        {
            int? pageSize = null;
            int? pageCurrent = null;
            if (pageSize == null)
            {
                pageSize = MessageResConst.PageSize;
            }
            if (pageCurrent == null)
            {
                pageCurrent = 1;
            }
            ViewBag.PageSize = ListPageSize.GetListPageSize();
            var listItem_Colors = _item_ColorHandler.GetItem_Colors((int)pageSize, (int)pageCurrent, "Name", "increase", null);
            Item_ColorViewModel viewModel = new Item_ColorViewModel();
            viewModel.ListItem_ColorModel = listItem_Colors.Data;
            int size = listItem_Colors.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            ViewBag.Order = "increase";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(int? pageSize, int? pageCurrent, string column, string orderASCorDSC, Item_ColorModel item_colorModel)
        {
            if (pageSize == null)
            {
                pageSize = MessageResConst.PageSize;
            }
            if (pageCurrent == null)
            {
                pageCurrent = 1;
            }
            ViewBag.PageSize = ListPageSize.GetListPageSize();          

            var listItem_Color = _item_ColorHandler.GetItem_Colors((int)pageSize, (int)pageCurrent, column, orderASCorDSC, item_colorModel);
            Item_ColorViewModel viewModel = new Item_ColorViewModel();
            viewModel.ListItem_ColorModel = listItem_Color.Data;
            int size = listItem_Color.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            if (orderASCorDSC == MessageResConst.Increase)
            {
                ViewBag.Order = MessageResConst.Decrease;
            }
            else
            {
                ViewBag.Order = MessageResConst.Increase;
            }
            return View(viewModel);


        }
    }
}