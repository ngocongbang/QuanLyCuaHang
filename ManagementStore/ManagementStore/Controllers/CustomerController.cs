﻿using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Customers;
using ManagementStore.Models;
using System.Web.Mvc;
using Newtonsoft.Json;
using ManagementStore.Utilities;

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