using log4net;
using System;
using ManagementStore.EntityFramwork.DbContext;
using ManagementStore.Business.Common.Enums;
using ManagementStore.Business.Common.Constants;
using ManagementStore.EntityFramwork.Responsitory;
using System.Collections.Generic;
using System.Linq;
using ManagementStore.Business.Customers;

namespace ManagementStore.Business.Customers
{
    public class CustomerHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<CustomerModel> InsertCustomer(CustomerModel CustomerModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpCustomer = unitOfWorkStore.GetRepository<Customer>();
                    Customer CustomerEntity = new Customer();
                    CustomerEntity.Customer_ID = CustomerModel.Customer_ID;
                    CustomerEntity.Customer_Code = CustomerModel.Customer_Code;
                    CustomerEntity.Name = CustomerModel.Name;
                    CustomerEntity.Phone = CustomerModel.Phone;
                    CustomerEntity.Email = CustomerModel.Email;
                    CustomerEntity.Address = CustomerModel.Address;
                    CustomerEntity.Birthday = CustomerModel.Birthday;
                    CustomerEntity.Category = CustomerModel.Category;
                    CustomerEntity.Company_Name = CustomerModel.Company_Name;
                    CustomerEntity.Tax_Code = CustomerModel.Tax_Code;
                    CustomerEntity.Url = CustomerModel.Url;
                    CustomerEntity.Sex = CustomerModel.Sex;
                    CustomerEntity.Note = CustomerModel.Note;
                    rpCustomer.Add(CustomerEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        CustomerModel.Customer_Code = CustomerEntity.Customer_Code;
                        return new Response<CustomerModel>((int)StatusResponses.Success, MessageResConst.Success, CustomerModel);
                    }
                    else
                    {
                        return new Response<CustomerModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, CustomerModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<CustomerModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<CustomerModel> UpdateCustomer(CustomerModel CustomerModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpCustomer = unitOfWorkStore.GetRepository<Customer>();
                    Customer CustomerEntity = rpCustomer.GetById(CustomerModel.Customer_ID);
                    CustomerEntity.Customer_ID = CustomerModel.Customer_ID;
                    CustomerEntity.Customer_Code = CustomerModel.Customer_Code;
                    CustomerEntity.Name = CustomerModel.Name;
                    CustomerEntity.Phone = CustomerModel.Phone;
                    CustomerEntity.Email = CustomerModel.Email;
                    CustomerEntity.Address = CustomerModel.Address;
                    CustomerEntity.Birthday = CustomerModel.Birthday;
                    CustomerEntity.Category = CustomerModel.Category;
                    CustomerEntity.Company_Name = CustomerModel.Company_Name;
                    CustomerEntity.Tax_Code = CustomerModel.Tax_Code;
                    CustomerEntity.Url = CustomerModel.Url==null? CustomerEntity.Url : CustomerModel.Url;
                    CustomerEntity.Sex = CustomerModel.Sex;
                    CustomerEntity.Note = CustomerModel.Note;
                    rpCustomer.Update(CustomerEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        CustomerModel.Customer_Code = CustomerEntity.Customer_Code;
                        return new Response<CustomerModel>((int)StatusResponses.Success, MessageResConst.Success, CustomerModel);
                    }
                    else
                    {
                        return new Response<CustomerModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, CustomerModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<CustomerModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<CustomerModel> GetCustomerByID(int iCustomerID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpCustomer = unitOfWorkStore.GetRepository<Customer>();
                    var obCustomer = rpCustomer.GetById(iCustomerID);

                    CustomerModel CustomerModel = new CustomerModel()
                    {
                        Customer_ID = obCustomer.Customer_ID,
                        Customer_Code = obCustomer.Customer_Code,
                        Name = obCustomer.Name,
                        Phone = obCustomer.Phone,
                        Email = obCustomer.Email,
                        Address = obCustomer.Address,
                        //Birthday = obCustomer.Birthday.t
                        Category = obCustomer.Category.Trim(),
                        Company_Name = obCustomer.Company_Name,
                        Tax_Code = obCustomer.Tax_Code,
                        Url = obCustomer.Url == null? "#": obCustomer.Url,
                        Sex = obCustomer.Sex == null? "1":obCustomer.Sex,
                        Note = obCustomer.Note,
                    };
                    return new Response<CustomerModel>((int)StatusResponses.Success, MessageResConst.Success, CustomerModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<CustomerModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<CustomerModel>> GetCustomers(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, CustomerModel customer)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpCustomer = unitOfWorkStore.GetRepository<Customer>();
                    var listCustomerEntity = rpCustomer.GetAll();
                    var listCustomerModel = (from Customer in listCustomerEntity
                                             select new CustomerModel()
                                             {
                                                 Customer_ID = Customer.Customer_ID,
                                                 Customer_Code = Customer.Customer_Code,
                                                 Name = Customer.Name,
                                                 Phone = Customer.Phone,
                                                 Email = Customer.Email,
                                                 Address = Customer.Address,
                                                 Birthday = Customer.Birthday,
                                                 Category = Customer.Category,
                                                 Company_Name = Customer.Company_Name,
                                                 Tax_Code = Customer.Tax_Code,
                                                 Url = Customer.Url,
                                                 Sex = Customer.Sex,
                                                 Note = Customer.Note
                                             }).ToList();

                    // search
                    if (customer != null)
                    {
                        if (customer.Customer_Code != null)
                        {
                            listCustomerModel = listCustomerModel.Where(x => x.Customer_Code.Contains(customer.Customer_Code)).ToList();
                        }
                        if (customer.Name != null)
                        {
                            listCustomerModel = listCustomerModel.Where(x => x.Name.ToLower().Contains(customer.Name.ToLower())).ToList();
                        }
                    }
                    int countData = listCustomerModel.Count;
                    listCustomerModel = listCustomerModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order
                    switch (orderid)
                    {
                        case "Customer_Code":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listCustomerModel = listCustomerModel.OrderBy(x => x.Customer_Code).ToList();
                            }
                            else
                            {
                                listCustomerModel = listCustomerModel.OrderByDescending(x => x.Customer_Code).ToList();
                            }

                            break;
                        case "Name":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listCustomerModel = listCustomerModel.OrderBy(x => x.Name).ToList();
                            }
                            else
                            {
                                listCustomerModel = listCustomerModel.OrderByDescending(x => x.Name).ToList();
                            }
                            break;
                        case "Phone":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listCustomerModel = listCustomerModel.OrderBy(x => x.Phone).ToList();
                            }
                            else
                            {
                                listCustomerModel = listCustomerModel.OrderByDescending(x => x.Phone).ToList();
                            }
                            break;
                        case "Email":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listCustomerModel = listCustomerModel.OrderBy(x => x.Email).ToList();
                            }
                            else
                            {
                                listCustomerModel = listCustomerModel.OrderByDescending(x => x.Email).ToList();
                            }
                            break;
                        case "Address":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listCustomerModel = listCustomerModel.OrderBy(x => x.Address).ToList();
                            }
                            else
                            {
                                listCustomerModel = listCustomerModel.OrderByDescending(x => x.Address).ToList();
                            }
                            break;

                        default:
                            break;
                    }
                    return new Response<List<CustomerModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listCustomerModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<CustomerModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }
    }
}
