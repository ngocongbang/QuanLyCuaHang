using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Common.Enums;
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
        ItemHandler _itemHander = new ItemHandler();
        // GET: Item  
        #region --------------------- Tìm kiếm hàng hóa -------------------------
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
            var lstItem = _itemHander.GetItems((int)pageSize, (int)pageCurrent, "Name", "increase", null);
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
            var listItem = _itemHander.GetItems((int)pageSize, (int)pageCurrent, column, orderASCorDSC, itemModel);
            ItemViewModel viewModel = new ItemViewModel();
            viewModel.ListItemModel = listItem.Data;
            int size = listItem.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            return View(viewModel);
        }
        #endregion ----------------

        #region ------------- Tạo mới hàng hóa ------------------------
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                var result = _itemHander.InsertItem(itemModel);
                if (result != null)
                {
                    return RedirectToAction("Index", "Item");
                }else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View("Index");
        }
        #endregion ----------------------------------------------------

        #region ---------------- Update hàng hóa ---------------------
        public ActionResult Update(int id)
        {
            var detail = _itemHander.GetItemByID(id);
            return View(detail.Data);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                var result = _itemHander.UpdateItem(itemModel);
                if (result != null)
                {
                    return RedirectToAction("Index", "Item");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }
        #endregion ---------------------------------------------------

        #region ---------- Xóa hàng hóa ----------------------------
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { RESULT = "500" });
            }
            var result = _itemHander.Delete((int)id);
            if (result.ResponseCode == (int)StatusResponses.Success)
            {
                return Json(new { RESULT = "200" });
            }
            return Json(new { RESULT = "500" });
        }
        #endregion -------------------------------------------------
    }
}