using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Items;
using ManagementStore.Models;
using ManagementStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementStore.Controllers
{
    public class ItemController : Controller
    {
        ItemHandler itemHander = new ItemHandler();
        // GET: Item
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
            var lstItem = itemHander.GetItems((int)pageSize, (int)pageCurrent, "Name", "increase", null);
            ItemViewModel viewModel = new ItemViewModel();
            viewModel.ListItemModel = lstItem.Data;
            int size = lstItem.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(int? pageSize,int? pageCurrent,string column,string orderASCorDSC, ItemModel itemModel)
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
            var listItem = itemHander.GetItems((int)pageSize, (int)pageCurrent, column, orderASCorDSC, itemModel);
            ItemViewModel viewModel = new ItemViewModel();
            viewModel.ListItemModel = listItem.Data;
            int size = listItem.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            return View(viewModel);
        }
    }
}