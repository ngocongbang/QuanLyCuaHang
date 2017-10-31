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

namespace ManagementStore.Business.GroupItems
{
  public  class GroupItemHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<GroupItemModel> InsertGroupItem(GroupItemModel GroupItemModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpGroupItem = unitOfWorkStore.GetRepository<GroupItem>();
                    GroupItem GroupItemEntity = new GroupItem();
                    GroupItemEntity.GroupItem_ID = GroupItemModel.GroupItem_ID;
                    GroupItemEntity.GroupItem_Code = GroupItemModel.GroupItem_Code;
                    GroupItemEntity.Name = GroupItemModel.Name;                   
                    rpGroupItem.Add(GroupItemEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        GroupItemModel.Name = GroupItemEntity.Name;
                        return new Response<GroupItemModel>((int)StatusResponses.Success, MessageResConst.Success, GroupItemModel);
                    }
                    else
                    {
                        return new Response<GroupItemModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, GroupItemModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<GroupItemModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<GroupItemModel> UpdateGroupItem(GroupItemModel GroupItemModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpGroupItem = unitOfWorkStore.GetRepository<GroupItem>();
                    GroupItem GroupItemEntity = rpGroupItem.GetById(GroupItemModel.GroupItem_ID);
                    GroupItemEntity.GroupItem_ID = GroupItemModel.GroupItem_ID;
                    GroupItemEntity.GroupItem_Code = GroupItemModel.GroupItem_Code;
                    GroupItemEntity.Name = GroupItemModel.Name;
                    GroupItemEntity.GroupItem_ID = GroupItemModel.GroupItem_ID;
                    rpGroupItem.Update(GroupItemEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        GroupItemModel.Name = GroupItemEntity.Name;
                        return new Response<GroupItemModel>((int)StatusResponses.Success, MessageResConst.Success, GroupItemModel);
                    }
                    else
                    {
                        return new Response<GroupItemModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, GroupItemModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<GroupItemModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<GroupItemModel> GetGroupItemByID(int iGroupItemID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpGroupItem = unitOfWorkStore.GetRepository<GroupItem>();
                    var obGroupItem = rpGroupItem.GetById(iGroupItemID);

                    GroupItemModel GroupItemModel = new GroupItemModel()
                    {
                        GroupItem_ID = obGroupItem.GroupItem_ID,
                        GroupItem_Code = obGroupItem.GroupItem_Code,
                        Name = obGroupItem.Name                       
                    };
                    return new Response<GroupItemModel>((int)StatusResponses.Success, MessageResConst.Success, GroupItemModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<GroupItemModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<GroupItemModel>> GetGroupItems(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, GroupItemModel GroupItemModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpGroupItem = unitOfWorkStore.GetRepository<GroupItem>();
                    var listGroupItemEntity = rpGroupItem.GetAll();
                    var listGroupItemModel = (from obGroupItem in listGroupItemEntity
                                                  select new GroupItemModel()
                                                  {
                                                      GroupItem_ID = obGroupItem.GroupItem_ID,
                                                      GroupItem_Code = obGroupItem.GroupItem_Code,
                                                      Name = obGroupItem.Name
                                                  }).ToList();

                    // search
                    if (GroupItemModel != null)
                    {
                        if (GroupItemModel.Name != null)
                        {
                            listGroupItemModel = listGroupItemModel.Where(x => x.Name.Contains(GroupItemModel.Name)).ToList();
                        }
                    }
                    int countData = listGroupItemModel.Count;
                    listGroupItemModel = listGroupItemModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order
                    switch (orderid)
                    {
                        case "Name":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listGroupItemModel = listGroupItemModel.OrderBy(x => x.Name).ToList();
                            }
                            else
                            {
                                listGroupItemModel = listGroupItemModel.OrderByDescending(x => x.Name).ToList();
                            }

                            break;
                    }
                    return new Response<List<GroupItemModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listGroupItemModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<GroupItemModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }

        public Response<GroupItemModel> Delete(int idGroupItem)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpGroupItem = unitOfWorkStore.GetRepository<GroupItem>();
                    GroupItem GroupItemEntity = rpGroupItem.GetById(idGroupItem);
                    rpGroupItem.Delete(GroupItemEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<GroupItemModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<GroupItemModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<GroupItemModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
