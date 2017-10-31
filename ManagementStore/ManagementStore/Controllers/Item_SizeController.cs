using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Common.Enums;
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
        #region ------------- Hàm lấy dữ liệu ---------------------------
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
            int size = (double)listItem_Sizes.CountData / (int)pageSize == 1 ? 1 : listItem_Sizes.CountData / (int)pageSize + 1;
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
            int size = (double)listItem_Size.CountData / (int)pageSize == 1 ? 1 : listItem_Size.CountData / (int)pageSize + 1;
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
        #endregion

        #region ------------- Hàm thêm mới size --------------------
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item_SizeModel item_size)
        {
            if (ModelState.IsValid)
            {
                var result = _item_SizeHandler.InsertItem_Size(item_size);
                if (result != null)
                {
                    return RedirectToAction("Index", "Item_Size");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới size không thành công");
                }
            }
            return View("Index");
        }
        #endregion -------------------------------------------------


        #region -------- Hàm cập nhật size hàng hóa -----------
        public ActionResult Update(int id)
        {
            var detail = _item_SizeHandler.GetItem_SizeByID(id);
            return View(detail.Data);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Item_SizeModel sizeModel)
        {
            if (ModelState.IsValid)
            {
                var result = _item_SizeHandler.UpdateItem_Size(sizeModel);
                if (result != null)
                {
                    return RedirectToAction("Index", "Item_Size");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật size không thành công");
                }
            }
            return View("Index");
        }
        #endregion --------------------------------------------

        [HttpPost]
        public JsonResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { RESULT = "500" });
            }
            var result = _item_SizeHandler.Delete((int)id);
            if (result.ResponseCode == (int)StatusResponses.Success)
            {
                return Json(new { RESULT = "200" });
            }
            return Json(new { RESULT = "500" });
        }
    }
}