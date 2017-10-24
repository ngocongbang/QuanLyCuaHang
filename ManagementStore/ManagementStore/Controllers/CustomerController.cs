using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Customers;
using ManagementStore.Models;
using System.Web.Mvc;
using Newtonsoft.Json;
using ManagementStore.Utilities;
using ManagementStore.Business.Common.Enums;
using System.IO;
using System;
using System.Net;

namespace ManagementStore.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        CustomerHandler _customerHandler = new CustomerHandler();
        // GET: Customers
        //[HttpGet]
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
            var listCustomer = _customerHandler.GetCustomers((int)pageSize, (int)pageCurrent,"Name", "increase",null);
            CustomerViewModel viewModel = new CustomerViewModel();
            viewModel.ListCustomerModel = listCustomer.Data;
            int size = listCustomer.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(int? pageSize, int? pageCurrent, string column, string orderASCorDSC, CustomerModel customerModel)
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
            var listCustomer = _customerHandler.GetCustomers((int)pageSize, (int)pageCurrent, column, orderASCorDSC, customerModel);
            CustomerViewModel viewModel = new CustomerViewModel();
            viewModel.ListCustomerModel = listCustomer.Data;
            int size = listCustomer.CountData / (int)pageSize + 1;
            viewModel.DisplayPage = pageCurrent.ToString() + "/" + size.ToString();
            viewModel.CountPage = size;
            return View(viewModel);


        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                var result = _customerHandler.InsertCustomer(customerModel);
                if (result.ResponseCode == (int)StatusResponses.Success)
                {                    
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới khách hàng không thành công");
                }
            }
            return View("Index");

        }
        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            var detail = _customerHandler.GetCustomerByID(id);
            return View(detail.Data);
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                // xy ly file
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                    try
                    {
                        string path = System.IO.Path.Combine(Server.MapPath("~/assets/images/customers"),
                           Path.GetFileName(file.FileName));
                          
                        file.SaveAs(path);
                        customerModel.Url = "~/assets/images/customers/" + file.FileName;
                        ViewBag.Message = "Your message for success";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "Please select file";
                }            

                var result = _customerHandler.UpdateCustomer(customerModel);
                if (result != null)
                {
                    //SetAlert("Sửa thành công", "seccess");
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật khách hàng thành công");
                }
            }
            return View("Index");
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var detail = _customerHandler.GetCustomerByID((int)id);
            return View(detail.Data);
        }
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            if (id==null)
            {
                return Json(new { RESULT = "500" });
            }
            var result = _customerHandler.Delete((int)id);
            if (result.ResponseCode == (int)StatusResponses.Success)
            {
                return Json(new { RESULT = "200" });
            }
            return Json(new { RESULT = "500" });
        }
        [HttpPost]
        public JsonResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
                //if (result.Succeeded)
                //{
                //     //return RedirectToLocal(returnUrl);
                //}

                ModelState.AddModelError("", "Identifiant ou mot de passe invalide");
                return Json("error-model-wrong");
            }

            // If we got this far, something failed, redisplay form
            return Json("error-mode-not-valid");
        }
    }
}