using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Item_Sizes;
using ManagementStore.Models;
using ManagementStore.Utilities;
using System.Web.Mvc;

namespace ManagementStore.Controllers
{
    public class Item_SizeController : Controller
    {
        Item_SizeHandler _item_SizeHandler = new Item_SizeHandler();
        // GET: Item_Size
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
            var listItem_Sizes = _item_SizeHandler.GetItem_Sizes((int)pageSize, (int)pageCurrent, "Name", "increase", null);
            Item_SizeViewModel viewModel = new Item_SizeViewModel();
            viewModel.ListItem_SizeModel = listItem_Sizes.Data;
            int size = listItem_Sizes.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            ViewBag.Order = "increase";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(int? pageSize, int? pageCurrent, string column, string orderASCorDSC, Item_SizeModel item_SizeModel)
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

            var listItem_Size = _item_SizeHandler.GetItem_Sizes((int)pageSize, (int)pageCurrent, column, orderASCorDSC, item_SizeModel);
            Item_SizeViewModel viewModel = new Item_SizeViewModel();
            viewModel.ListItem_SizeModel = listItem_Size.Data;
            int size = listItem_Size.CountData / (int)pageSize + 1;
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