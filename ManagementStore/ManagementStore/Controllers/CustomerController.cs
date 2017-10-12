using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Customers;
using System.Web.Mvc;

namespace ManagementStore.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        CustomerHandler _customerHandler = new CustomerHandler();
        // GET: Customers
        public ActionResult Index(int? pageSize, int? pageCurrent)
        {
            if (pageSize ==null)
            {
                pageSize = MessageResConst.PageSize;
            }
            if (pageCurrent == null)
            {
                pageCurrent = 1;
            }
            var listCustomer = _customerHandler.GetCustomers((int)pageSize, (int)pageCurrent);
            return View(listCustomer.Data);
        }
    }
}