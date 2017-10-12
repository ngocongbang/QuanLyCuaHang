using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ManagementStore.Business;
using ManagementStore.Business.Vendors;

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
            var obj = vendorHand.GetVendors();
            return View(obj.Data);
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
            var result = vendorHand.InsertVendor(obj);
            return View(result);
        }
        #endregion==================
    }
}