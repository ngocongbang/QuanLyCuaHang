using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.GroupItems;
using ManagementStore.Models;
using ManagementStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagementStore.Controllers
{
    public class Group_ItemController : Controller
    {
        GroupItemHandler _group_itemHandler = new GroupItemHandler();
        // GET: Group_Item
        #region ----------- Hàm lấy dữ liệu --------------------------
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
            var listGroup_Item = _group_itemHandler.GetGroupItems((int)pageSize, (int)pageCurrent, "Name", "increase", null);
            Group_ItemViewModel viewModel = new Group_ItemViewModel();
            viewModel.ListGroup_ItemModel = listGroup_Item.Data;
            int size = (double)listGroup_Item.CountData % (int)pageSize == 0 ? listGroup_Item.CountData / (int)pageSize : listGroup_Item.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            ViewBag.Order = "increase";
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(int? pageSize, int? pageCurrent, string column, string orderASCorDSC, GroupItemModel groupItemModel)
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

            var listGroup_Item = _group_itemHandler.GetGroupItems((int)pageSize, (int)pageCurrent, column, orderASCorDSC, groupItemModel);
            Group_ItemViewModel viewModel = new Group_ItemViewModel();
            viewModel.ListGroup_ItemModel = listGroup_Item.Data;
            int size = (double)listGroup_Item.CountData % (int)pageSize == 0 ? listGroup_Item.CountData / (int)pageSize : listGroup_Item.CountData / (int)pageSize + 1;
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
        #endregion ------------ kết thúc lấy dữ liệu --------------------------------------
        #region ------------ Hàm tạo mới nhóm hàng hóa -----------------------------
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Parent_Item = _group_itemHandler.GetGroupItemsForParent().Data;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupItemModel groupItem)
        {
            ViewBag.Parent_Item = _group_itemHandler.GetGroupItemsForParent().Data;
            if (ModelState.IsValid)
            {
                var result = _group_itemHandler.InsertGroupItem(groupItem);
                if (result != null)
                {
                    return RedirectToAction("Index", "Group_Item");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới nhóm hàng hóa không thành công!");
                }
            }
            return View("Index");
        }
        #endregion -----------------------------------------------------------------
    }
}