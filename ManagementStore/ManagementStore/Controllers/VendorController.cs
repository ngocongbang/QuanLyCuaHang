using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementStore.Business;
using ManagementStore.Business.Vendors;
using ManagementStore.Business.Common.Constants;
using ManagementStore.Models;

namespace ManagementStore.Controllers
{
    public class VendorController : Controller
    {

        // GET: Vendor
        VendorHandler vendorHand = new VendorHandler();
        // Chức năng hiển thị vendor
        #region Chức năng hiển thị danh sách vendor
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
            var lstVendor = vendorHand.GetVendors((int)pageSize, (int)pageCurrent, "Name");
            VendorViewModel viewModel = new VendorViewModel();
            viewModel.ListVendorModel = lstVendor.Data;
            int size = lstVendor.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(int? pageSize, int? pageCurrent, string orderId, VendorModel vendorModel)
        {
            if (pageSize == null)
            {
                pageSize = MessageResConst.PageSize;
            }
            if (pageCurrent == null)
            {
                pageCurrent = 1;
            }
            var lstVendor = vendorHand.GetVendors((int)pageSize, (int)pageCurrent, orderId);
            VendorViewModel viewModel = new VendorViewModel();
            viewModel.ListVendorModel = lstVendor.Data;
            int size = lstVendor.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            return View(viewModel);
        }
        #endregion
        // Chức năng tạo mới vendor
        #region  chức năng tạo mới vendor
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VendorModel vendor)
        {
            var obj = new VendorModel();
            obj.Vendor_Code = vendor.Vendor_Code;
            obj.Name = vendor.Name;
            obj.Company_Name = vendor.Company_Name;
            obj.Tax_Code = vendor.Tax_Code;
            obj.Phone = vendor.Phone;
            obj.Region = vendor.Region;
            obj.CommuneWard = vendor.CommuneWard;
            obj.Email = vendor.Email;
            obj.Group_Vendor = vendor.Group_Vendor;
            obj.Note = vendor.Note;
            
            if (ModelState.IsValid)
            {
                var result = vendorHand.InsertVendor(obj);
                if (result != null)
                {
                    //SetAlert("Sửa thành công", "seccess");
                    return RedirectToAction("Index", "Vendor");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới vendor không thành công");
                }
            }
            return View("Index");

        }
        #endregion==================
        // chức năng xem chi tiết sản phẩm
        #region ------------- Chức năng xem chi tiết sản phẩm -------------------
        public ActionResult Detail(int id)
        {
            var detail = vendorHand.GetVendorByID(id);
            return View(detail.Data);
        }
        #endregion --------------------------------------------------------------
        // Tạo hàm update vendor
        #region  ---- Update -----
        public ActionResult Update(int id)
        {
            var detail = vendorHand.GetVendorByID(id);
            return View(detail.Data);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Update(VendorModel vender)
        {
            if (ModelState.IsValid)
            {
                var result = vendorHand.UpdateVendor(vender);
                if (result != null)
                {
                    //SetAlert("Sửa thành công", "seccess");
                    return RedirectToAction("Index", "Vendor");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user không thành công");
                }
            }
            return View("Index");
        }
        #endregion --------------

        // tạo hàm xóa vendor
        #region ---- Delete ----
        public ActionResult Delete(int id)
        {
            var result = vendorHand.Delete(id);
            return RedirectToAction("Index", "Vendor");
        }
        #endregion-----------
    }
}