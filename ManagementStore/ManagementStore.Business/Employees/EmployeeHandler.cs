using log4net;
using ManagementStore.Business.Common.Constants;
using ManagementStore.Business.Common.Enums;
using ManagementStore.EntityFramwork.DbContext;
using ManagementStore.EntityFramwork.Responsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Employees
{
   public class EmployeeHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<EmployeeModel> InsertEmployee(EmployeeModel EmployeeModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpEmployee = unitOfWorkStore.GetRepository<Employee>();
                    Employee EmployeeEntity = new Employee();
                    EmployeeEntity.Employee_ID = EmployeeModel.Employee_ID;
                    EmployeeEntity.Employee_Code = EmployeeModel.Employee_Code;
                    EmployeeEntity.Name = EmployeeModel.Name;
                    EmployeeEntity.Address = EmployeeModel.Address;                   
                    EmployeeEntity.Phone = EmployeeModel.Phone;
                    EmployeeEntity.Possition = EmployeeModel.Possition;
                    EmployeeEntity.Private_ID = EmployeeModel.Private_ID;
                    EmployeeEntity.Branch_ID = EmployeeModel.Branch_ID;                    
                    rpEmployee.Add(EmployeeEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        EmployeeModel.Employee_Code = EmployeeEntity.Employee_Code;
                        return new Response<EmployeeModel>((int)StatusResponses.Success, MessageResConst.Success, EmployeeModel);
                    }
                    else
                    {
                        return new Response<EmployeeModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, EmployeeModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<EmployeeModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<EmployeeModel> UpdateEmployee(EmployeeModel EmployeeModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpEmployee = unitOfWorkStore.GetRepository<Employee>();
                    Employee EmployeeEntity = rpEmployee.GetById(EmployeeModel.Employee_ID);
                    EmployeeEntity.Employee_ID = EmployeeModel.Employee_ID;
                    EmployeeEntity.Employee_Code = EmployeeModel.Employee_Code;
                    EmployeeEntity.Name = EmployeeModel.Name;
                    EmployeeEntity.Address = EmployeeModel.Address;
                    EmployeeEntity.Phone = EmployeeModel.Phone;
                    EmployeeEntity.Possition = EmployeeModel.Possition;
                    EmployeeEntity.Private_ID = EmployeeModel.Private_ID;
                    EmployeeEntity.Branch_ID = EmployeeModel.Branch_ID;
                    rpEmployee.Update(EmployeeEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        EmployeeModel.Employee_Code = EmployeeEntity.Employee_Code;
                        return new Response<EmployeeModel>((int)StatusResponses.Success, MessageResConst.Success, EmployeeModel);
                    }
                    else
                    {
                        return new Response<EmployeeModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, EmployeeModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<EmployeeModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<EmployeeModel> GetEmployeeByID(int iEmployeeID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpEmployee = unitOfWorkStore.GetRepository<Employee>();
                    var obEmployee = rpEmployee.GetById(iEmployeeID);

                    EmployeeModel EmployeeModel = new EmployeeModel()
                    {
                        Employee_ID = obEmployee.Employee_ID,
                        Employee_Code = obEmployee.Employee_Code,
                        Name = obEmployee.Name,
                        Address = obEmployee.Address,
                        Phone = obEmployee.Phone,
                        Possition = obEmployee.Possition,
                        Private_ID = obEmployee.Private_ID,                        
                        Branch_ID = obEmployee.Branch_ID,                       
                    };
                    return new Response<EmployeeModel>((int)StatusResponses.Success, MessageResConst.Success, EmployeeModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<EmployeeModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<EmployeeModel>> GetEmployees(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, EmployeeModel employeeModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpEmployee = unitOfWorkStore.GetRepository<Employee>();
                    var listEmployeeEntity = rpEmployee.GetAll();
                    var listEmployeeModel = (from employee in listEmployeeEntity
                                             select new EmployeeModel()
                                             {
                                                 Employee_ID = employee.Employee_ID,
                                                 Employee_Code = employee.Employee_Code,
                                                 Name = employee.Name,
                                                 Address = employee.Address,
                                                 Phone = employee.Phone,
                                                 Possition = employee.Possition,
                                                 Private_ID = employee.Private_ID,
                                                 Branch_ID = employee.Branch_ID,
                                             }).ToList();

                    // search
                    if (employeeModel != null)
                    {
                        if (employeeModel.Employee_Code != null)
                        {
                            listEmployeeModel = listEmployeeModel.Where(x => x.Employee_Code.Contains(employeeModel.Employee_Code)).ToList();
                        }
                        if (employeeModel.Name != null)
                        {
                            listEmployeeModel = listEmployeeModel.Where(x => x.Name.ToLower().Contains(employeeModel.Name.ToLower())).ToList();
                        }
                    }
                    int countData = listEmployeeModel.Count;
                    listEmployeeModel = listEmployeeModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order
                    switch (orderid)
                    {
                        case "Employee_Code":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listEmployeeModel = listEmployeeModel.OrderBy(x => x.Employee_Code).ToList();
                            }
                            else
                            {
                                listEmployeeModel = listEmployeeModel.OrderByDescending(x => x.Employee_Code).ToList();
                            }

                            break;
                        case "Name":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listEmployeeModel = listEmployeeModel.OrderBy(x => x.Name).ToList();
                            }
                            else
                            {
                                listEmployeeModel = listEmployeeModel.OrderByDescending(x => x.Name).ToList();
                            }
                            break;
                        case "Phone":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listEmployeeModel = listEmployeeModel.OrderBy(x => x.Phone).ToList();
                            }
                            else
                            {
                                listEmployeeModel = listEmployeeModel.OrderByDescending(x => x.Phone).ToList();
                            }
                            break;                        
                        case "Address":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listEmployeeModel = listEmployeeModel.OrderBy(x => x.Address).ToList();
                            }
                            else
                            {
                                listEmployeeModel = listEmployeeModel.OrderByDescending(x => x.Address).ToList();
                            }
                            break;

                        default:
                            break;
                    }
                    return new Response<List<EmployeeModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listEmployeeModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<EmployeeModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }

        public Response<EmployeeModel> Delete(int idEmployee)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpEmployee = unitOfWorkStore.GetRepository<Employee>();
                    Employee EmployeeEntity = rpEmployee.GetById(idEmployee);
                    rpEmployee.Delete(EmployeeEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<EmployeeModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<EmployeeModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<EmployeeModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
