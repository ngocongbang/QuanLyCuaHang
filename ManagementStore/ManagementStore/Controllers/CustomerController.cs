﻿using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Customers;
using ManagementStore.Models;
using System.Web.Mvc;
using Newtonsoft.Json;

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
            var listCustomer = _customerHandler.GetCustomers((int)pageSize, (int)pageCurrent);
            CustomerViewModel viewModel = new CustomerViewModel();
            viewModel.ListCustomerModel = listCustomer.Data;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(int? pageSize, int? pageCurrent, CustomerModel customerModel)
        {
            if (pageSize == null)
            {
                pageSize = MessageResConst.PageSize;
            }
            if (pageCurrent == null)
            {
                pageCurrent = 1;
            }
            var listCustomer = _customerHandler.GetCustomers((int)pageSize, (int)pageCurrent);
            CustomerViewModel viewModel = new CustomerViewModel();
            viewModel.ListCustomerModel = listCustomer.Data;           
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