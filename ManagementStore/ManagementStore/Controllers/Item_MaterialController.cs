using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Item_Materials;
using ManagementStore.Business.Item_Sizes;
using ManagementStore.Models;
using ManagementStore.Utilities;
using System.Web.Mvc;

namespace ManagementStore.Controllers
{
    public class Item_MaterialController : Controller
    {
        Item_MaterialHandler _item_MaterialHandler = new Item_MaterialHandler();
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
            var listItem_Materials = _item_MaterialHandler.GetItem_Materials((int)pageSize, (int)pageCurrent, "Name", "increase", null);
            Item_MaterialViewModel viewModel = new Item_MaterialViewModel();
            viewModel.ListItem_MaterialModel = listItem_Materials.Data;
            int size = listItem_Materials.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            ViewBag.Order = "increase";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(int? pageSize, int? pageCurrent, string column, string orderASCorDSC, Item_MaterialModel item_SizeModel)
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

            var listItem_Size = _item_MaterialHandler.GetItem_Materials((int)pageSize, (int)pageCurrent, column, orderASCorDSC, item_SizeModel);
            Item_MaterialViewModel viewModel = new Item_MaterialViewModel();
            viewModel.ListItem_MaterialModel = listItem_Size.Data;
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