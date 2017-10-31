using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Common.Enums;
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
        #region ------------- GetAll dữ liệu ------------------
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
        #endregion --------------------------------------------

        #region --------------- Thêm mới ----------------------------
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item_MaterialModel materialModel)
        {
            if (ModelState.IsValid)
            {
                var result = _item_MaterialHandler.InsertItem_Material(materialModel);
                if (result != null)
                {
                    return RedirectToAction("Index", "Item_Material");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới không thành công");
                }
            }
            return View("Index");
        }
        #endregion --------------------------------------------------

        #region ---- Tạo hàm update dữ liệu ---------------
        public ActionResult Update(int id)
        {
            var detail = _item_MaterialHandler.GetItem_MaterialByID(id);
            return View(detail.Data);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Item_MaterialModel materialModel)
        {
            if (ModelState.IsValid)
            {
                var result = _item_MaterialHandler.UpdateItem_Material(materialModel);
                if (result != null)
                {
                    return RedirectToAction("Index", "Item_Meterial");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }
        #endregion ----------------------------------------

        #region --- Tạo hàm xóa dữ liệu ---------------------
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { RESULT = "500" });
            }
            var result = _item_MaterialHandler.Delete((int)id);
            if (result.ResponseCode == (int)StatusResponses.Success)
            {
                return Json(new { RESULT = "200" });
            }
            return Json(new { RESULT = "500" });
        }
        #endregion
    }

}
