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

namespace ManagementStore.Business.GroupItem_Subs
{
   public class GroupItem_SubHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<GroupItem_SubModel> InsertGroupItem_Sub(GroupItem_SubModel GroupItem_SubModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpGroupItem_Sub = unitOfWorkStore.GetRepository<GroupItem_Sub>();
                    GroupItem_Sub GroupItem_SubEntity = new GroupItem_Sub();
                    GroupItem_SubEntity.GroupItemSub_ID = GroupItem_SubModel.GroupItemSub_ID;
                    GroupItem_SubEntity.GroupItemSub_Code = GroupItem_SubModel.GroupItemSub_Code;
                    GroupItem_SubEntity.Name = GroupItem_SubModel.Name;
                    GroupItem_SubEntity.GroupItem_ID = GroupItem_SubModel.GroupItem_ID;
                    rpGroupItem_Sub.Add(GroupItem_SubEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        GroupItem_SubModel.Name = GroupItem_SubEntity.Name;
                        return new Response<GroupItem_SubModel>((int)StatusResponses.Success, MessageResConst.Success, GroupItem_SubModel);
                    }
                    else
                    {
                        return new Response<GroupItem_SubModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, GroupItem_SubModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<GroupItem_SubModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<GroupItem_SubModel> UpdateGroupItem_Sub(GroupItem_SubModel GroupItem_SubModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpGroupItem_Sub = unitOfWorkStore.GetRepository<GroupItem_Sub>();
                    GroupItem_Sub GroupItem_SubEntity = rpGroupItem_Sub.GetById(GroupItem_SubModel.GroupItemSub_ID);
                    GroupItem_SubEntity.GroupItemSub_ID = GroupItem_SubModel.GroupItemSub_ID;
                    GroupItem_SubEntity.GroupItemSub_Code = GroupItem_SubModel.GroupItemSub_Code;
                    GroupItem_SubEntity.Name = GroupItem_SubModel.Name;
                    GroupItem_SubEntity.GroupItem_ID = GroupItem_SubModel.GroupItem_ID;
                    rpGroupItem_Sub.Update(GroupItem_SubEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        GroupItem_SubModel.Name = GroupItem_SubEntity.Name;
                        return new Response<GroupItem_SubModel>((int)StatusResponses.Success, MessageResConst.Success, GroupItem_SubModel);
                    }
                    else
                    {
                        return new Response<GroupItem_SubModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, GroupItem_SubModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<GroupItem_SubModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<GroupItem_SubModel> GetGroupItem_SubByID(int iGroupItem_SubID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpGroupItem_Sub = unitOfWorkStore.GetRepository<GroupItem_Sub>();
                    var obGroupItem_Sub = rpGroupItem_Sub.GetById(iGroupItem_SubID);

                    GroupItem_SubModel GroupItem_SubModel = new GroupItem_SubModel()
                    {
                        GroupItemSub_ID = obGroupItem_Sub.GroupItemSub_ID,
                        GroupItemSub_Code = obGroupItem_Sub.GroupItemSub_Code,
                        Name = obGroupItem_Sub.Name,
                        GroupItem_ID = obGroupItem_Sub.GroupItem_ID
                    };
                    return new Response<GroupItem_SubModel>((int)StatusResponses.Success, MessageResConst.Success, GroupItem_SubModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<GroupItem_SubModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<GroupItem_SubModel>> GetGroupItem_Subs(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, GroupItem_SubModel GroupItem_SubModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpGroupItem_Sub = unitOfWorkStore.GetRepository<GroupItem_Sub>();
                    var listGroupItem_SubEntity = rpGroupItem_Sub.GetAll();
                    var listGroupItem_SubModel = (from groupItem_Sub in listGroupItem_SubEntity
                                            select new GroupItem_SubModel()
                                            {
                                                GroupItemSub_ID = groupItem_Sub.GroupItemSub_ID,
                                                GroupItemSub_Code = groupItem_Sub.GroupItemSub_Code,
                                                Name = groupItem_Sub.Name,
                                                GroupItem_ID = groupItem_Sub.GroupItem_ID

                                            }).ToList();

                    // search
                    if (GroupItem_SubModel != null)
                    {
                        if (GroupItem_SubModel.Name != null)
                        {
                            listGroupItem_SubModel = listGroupItem_SubModel.Where(x => x.Name.Contains(GroupItem_SubModel.Name)).ToList();
                        }
                    }
                    int countData = listGroupItem_SubModel.Count;
                    listGroupItem_SubModel = listGroupItem_SubModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order
                    switch (orderid)
                    {
                        case "Name":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listGroupItem_SubModel = listGroupItem_SubModel.OrderBy(x => x.Name).ToList();
                            }
                            else
                            {
                                listGroupItem_SubModel = listGroupItem_SubModel.OrderByDescending(x => x.Name).ToList();
                            }

                            break;
                    }
                    return new Response<List<GroupItem_SubModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listGroupItem_SubModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<GroupItem_SubModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }

        public Response<GroupItem_SubModel> Delete(int idGroupItem_Sub)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpGroupItem_Sub = unitOfWorkStore.GetRepository<GroupItem_Sub>();
                    GroupItem_Sub GroupItem_SubEntity = rpGroupItem_Sub.GetById(idGroupItem_Sub);
                    rpGroupItem_Sub.Delete(GroupItem_SubEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<GroupItem_SubModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<GroupItem_SubModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<GroupItem_SubModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
