using log4net;
using ManagementStore.EntityFramwork.Responsitory;
using System;
using ManagementStore.Business.Common.Enums;
using ManagementStore.Business.Common.Constants;
using System.Collections.Generic;
using System.Linq;
using ManagementStore.EntityFramwork.DbContext;

namespace ManagementStore.Business.Item_Sizes
{
    public class Item_SizeHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<Item_SizeModel> InsertItem_Size(Item_SizeModel Item_SizeModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Size = unitOfWorkStore.GetRepository<Item_Size>();
                    Item_Size Item_SizeEntity = new Item_Size();
                    Item_SizeEntity.Item_Size1 = Item_SizeModel.Item_Size1;
                    Item_SizeEntity.Name = Item_SizeModel.Name;                   
                    rpItem_Size.Add(Item_SizeEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        Item_SizeModel.Item_Size1 = Item_SizeEntity.Item_Size1;
                        return new Response<Item_SizeModel>((int)StatusResponses.Success, MessageResConst.Success, Item_SizeModel);
                    }
                    else
                    {
                        return new Response<Item_SizeModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, Item_SizeModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_SizeModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<Item_SizeModel> UpdateItem_Size(Item_SizeModel Item_SizeModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpItem_Size = unitOfWorkStore.GetRepository<Item_Size>();
                    Item_Size Item_SizeEntity = rpItem_Size.GetById(Item_SizeModel.Item_Size1);
                    Item_SizeEntity.Name = Item_SizeModel.Name;                   
                    rpItem_Size.Update(Item_SizeEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        Item_SizeModel.Name = Item_SizeEntity.Name;
                        return new Response<Item_SizeModel>((int)StatusResponses.Success, MessageResConst.Success, Item_SizeModel);
                    }
                    else
                    {
                        return new Response<Item_SizeModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, Item_SizeModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_SizeModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<Item_SizeModel> GetItem_SizeByID(int iItem_SizeID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Size = unitOfWorkStore.GetRepository<Item_Size>();
                    var obItem_Size = rpItem_Size.GetById(iItem_SizeID);

                    Item_SizeModel Item_SizeModel = new Item_SizeModel()
                    {
                        Item_Size1 = obItem_Size.Item_Size1,
                        Name= obItem_Size.Name
                    };
                    return new Response<Item_SizeModel>((int)StatusResponses.Success, MessageResConst.Success, Item_SizeModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_SizeModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<Item_SizeModel>> GetItem_Sizes(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, Item_SizeModel Item_Size)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Size = unitOfWorkStore.GetRepository<Item_Size>();
                    var listItem_SizeEntity = rpItem_Size.GetAll();
                    var listItem_SizeModel = (from Item_Size1 in listItem_SizeEntity
                                             select new Item_SizeModel()
                                             {
                                                 Item_Size1 = Item_Size1.Item_Size1,
                                                 Name = Item_Size1.Name 
                                             }).ToList();

                    // search 
                    if (Item_Size != null)
                    {
                        if (Item_Size.Name != null)
                        {
                            listItem_SizeModel = listItem_SizeModel.Where(x => x.Name.Contains(Item_Size.Name)).ToList();
                        }
                    }
                    int countData = listItem_SizeModel.Count;
                    listItem_SizeModel = listItem_SizeModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order                   
                    
                    if (sortDecOrInc == MessageResConst.Increase)
                    {
                        listItem_SizeModel = listItem_SizeModel.OrderBy(x => x.Name).ToList();

                    }
                    else
                    {
                        listItem_SizeModel = listItem_SizeModel.OrderByDescending(x => x.Name).ToList();

                    }
                    return new Response<List<Item_SizeModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listItem_SizeModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Item_SizeModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }

        public Response<Item_SizeModel> Delete(int idItem_Size)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Size = unitOfWorkStore.GetRepository<Item_Size>();
                    Item_Size Item_SizeEntity = rpItem_Size.GetById(idItem_Size);
                    rpItem_Size.Delete(Item_SizeEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<Item_SizeModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<Item_SizeModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_SizeModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
