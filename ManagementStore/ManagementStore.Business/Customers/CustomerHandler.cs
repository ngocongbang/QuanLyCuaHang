﻿using log4net;
using System;
using ManagementStore.EntityFramwork.DbContext;
using ManagementStore.Business.Common.Enums;
using ManagementStore.Business.Common.Constants;
using ManagementStore.EntityFramwork.Responsitory;
using System.Collections.Generic;
using System.Linq;
using ManagementStore.Business.Vendors;

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
                        Birthday = obCustomer.Birthday,
                        Category = obCustomer.Category,
                        Company_Name = obCustomer.Company_Name
                    };
                    return new Response<CustomerModel>((int)StatusResponses.Success, MessageResConst.Success, CustomerModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<CustomerModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<CustomerModel>> GetCustomers()
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
                                                 Company_Name = Customer.Company_Name
                                             }).ToList();
                    return new Response<List<CustomerModel>>((int)StatusResponses.Success, MessageResConst.Success, listCustomerModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<CustomerModel>>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
