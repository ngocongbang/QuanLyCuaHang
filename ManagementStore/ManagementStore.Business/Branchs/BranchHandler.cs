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

namespace ManagementStore.Business.Branchs
{
    public class BranchHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<BranchModel> InsertBranch(BranchModel BranchModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpBranch = unitOfWorkStore.GetRepository<Branch>();
                    Branch BranchEntity = new Branch();
                    BranchEntity.Branch_ID = BranchModel.Branch_ID;
                    BranchEntity.Branch_Name = BranchModel.Branch_Name;
                    BranchEntity.Region = BranchModel.Region;
                    BranchEntity.CommuneWard = BranchModel.CommuneWard;
                    BranchEntity.Email = BranchModel.Email;
                    BranchEntity.Phone = BranchModel.Phone;
                    BranchEntity.Address = BranchModel.Address;                   
                    rpBranch.Add(BranchEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        BranchModel.Branch_Name = BranchEntity.Branch_Name;
                        return new Response<BranchModel>((int)StatusResponses.Success, MessageResConst.Success, BranchModel);
                    }
                    else
                    {
                        return new Response<BranchModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, BranchModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<BranchModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<BranchModel> UpdateBranch(BranchModel BranchModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpBranch = unitOfWorkStore.GetRepository<Branch>();
                    Branch BranchEntity = rpBranch.GetById(BranchModel.Branch_ID);
                    BranchEntity.Branch_ID = BranchModel.Branch_ID;
                    BranchEntity.Branch_Name = BranchModel.Branch_Name;
                    BranchEntity.Region = BranchModel.Region;
                    BranchEntity.CommuneWard = BranchModel.CommuneWard;
                    BranchEntity.Email = BranchModel.Email;
                    BranchEntity.Phone = BranchModel.Phone;
                    BranchEntity.Address = BranchModel.Address;
                    rpBranch.Update(BranchEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        BranchModel.Branch_Name = BranchEntity.Branch_Name;
                        return new Response<BranchModel>((int)StatusResponses.Success, MessageResConst.Success, BranchModel);
                    }
                    else
                    {
                        return new Response<BranchModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, BranchModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<BranchModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<BranchModel> GetBranchByID(int iBranchID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpBranch = unitOfWorkStore.GetRepository<Branch>();
                    var obBranch = rpBranch.GetById(iBranchID);

                    BranchModel BranchModel = new BranchModel()
                    {
                        Branch_ID = obBranch.Branch_ID,
                        Branch_Name = obBranch.Branch_Name,
                        Region = obBranch.Region,
                        CommuneWard = obBranch.CommuneWard,
                        Email = obBranch.Email,
                        Phone = obBranch.Phone,
                        Address = obBranch.Address,                      
                    };
                    return new Response<BranchModel>((int)StatusResponses.Success, MessageResConst.Success, BranchModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<BranchModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<BranchModel>> GetBranchs(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, BranchModel BranchModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpBranch = unitOfWorkStore.GetRepository<Branch>();
                    var listBranchEntity = rpBranch.GetAll();
                    var listBranchModel = (from branch in listBranchEntity
                                             select new BranchModel()
                                             {
                                                 Branch_ID = branch.Branch_ID,
                                                 Branch_Name = branch.Branch_Name,
                                                 Region = branch.Region,
                                                 CommuneWard = branch.CommuneWard,
                                                 Email = branch.Email,
                                                 Phone = branch.Phone,
                                                 Address = branch.Address,
                                             }).ToList();

                    // search
                    if (BranchModel != null)
                    {
                        if (BranchModel.Branch_Name != null)
                        {
                            listBranchModel = listBranchModel.Where(x => x.Branch_Name.Contains(BranchModel.Branch_Name)).ToList();
                        }                       
                    }
                    int countData = listBranchModel.Count;
                    listBranchModel = listBranchModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order
                    switch (orderid)
                    {
                        case "Branch_Name":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listBranchModel = listBranchModel.OrderBy(x => x.Branch_Name).ToList();
                            }
                            else
                            {
                                listBranchModel = listBranchModel.OrderByDescending(x => x.Branch_Name).ToList();
                            }

                            break;                        
                        case "Phone":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listBranchModel = listBranchModel.OrderBy(x => x.Phone).ToList();
                            }
                            else
                            {
                                listBranchModel = listBranchModel.OrderByDescending(x => x.Phone).ToList();
                            }
                            break;
                        case "Address":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listBranchModel = listBranchModel.OrderBy(x => x.Address).ToList();
                            }
                            else
                            {
                                listBranchModel = listBranchModel.OrderByDescending(x => x.Address).ToList();
                            }
                            break;

                        default:
                            break;
                    }
                    return new Response<List<BranchModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listBranchModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<BranchModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }

        public Response<BranchModel> Delete(int idBranch)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpBranch = unitOfWorkStore.GetRepository<Branch>();
                    Branch BranchEntity = rpBranch.GetById(idBranch);
                    rpBranch.Delete(BranchEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<BranchModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<BranchModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<BranchModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
