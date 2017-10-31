using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Common.Enums;
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
        #region --------------- Hàm lấy dữ liệu ---------------------
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
            var listItem_Color = _item_ColorHandler.GetItem_Colors((int)pageSize, (int)pageCurrent, "Name", "increase", null);
            Item_ColorViewModel viewModel = new Item_ColorViewModel();
            viewModel.ListItem_ColorModel = listItem_Color.Data;
            int size = (double)listItem_Color.CountData / (int)pageSize == 1 ? 1 : listItem_Color.CountData / (int)pageSize + 1;
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
            int size = (double)listItem_Color.CountData / (int)pageSize ==1 ? 1: listItem_Color.CountData / (int)pageSize + 1;
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

        #endregion---------------------
        #region  chức năng tạo mới thuộc tính màu sắc
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item_ColorModel item_color)
        {
            if (ModelState.IsValid)
            {
                var result = _item_ColorHandler.InsertItem_Color(item_color);
                if (result != null)
                {
                    return RedirectToAction("Index", "Item_Color");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới màu không thành công!");
                }
            }
            return View("Index");
        }
        #endregion

        #region ---------------- Chức năng cập nhật thuộc tính màu sắc ---------------------

        public ActionResult Update(int id)
        {

            var detail = _item_ColorHandler.GetItem_ColorByID(id);
            return View(detail.Data);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Item_ColorModel colorModel)
        {
            if (ModelState.IsValid)
            {
                var result = _item_ColorHandler.UpdateItem_Color(colorModel);
                if (result != null)
                {
                    return RedirectToAction("Index", "Item_Color");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật màu không thành công");
                }
            }
            return View("Index");
        }
        #endregion--------------------------------------------------
        #region ------ Chức năng xóa màu sắc ----------------------
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { RESULT = "500" });
            }
            var result = _item_ColorHandler.Delete((int)id);
            if (result.ResponseCode == (int)StatusResponses.Success)
            {
                return Json(new { RESULT = "200" });
            }
            return Json(new { RESULT = "500" });
        }
        #endregion ------------------------------------------------
    }
}