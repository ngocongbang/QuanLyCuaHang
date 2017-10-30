using log4net;
using ManagementStore.EntityFramwork.Responsitory;
using System;
using ManagementStore.Business.Common.Enums;
using ManagementStore.Business.Common.Constants;
using System.Collections.Generic;
using System.Linq;
using ManagementStore.EntityFramwork.DbContext;

namespace ManagementStore.Business.Item_Materials
{
    public class Item_MaterialHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<Item_MaterialModel> InsertItem_Material(Item_MaterialModel Item_MaterialModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Material = unitOfWorkStore.GetRepository<Item_Material>();
                    Item_Material Item_MaterialEntity = new Item_Material();
                    Item_MaterialEntity.Item_Material_ID = Item_MaterialModel.Item_Material_ID;
                    Item_MaterialEntity.Name = Item_MaterialModel.Name;                   
                    rpItem_Material.Add(Item_MaterialEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        Item_MaterialModel.Item_Material_ID = Item_MaterialEntity.Item_Material_ID;
                        return new Response<Item_MaterialModel>((int)StatusResponses.Success, MessageResConst.Success, Item_MaterialModel);
                    }
                    else
                    {
                        return new Response<Item_MaterialModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, Item_MaterialModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_MaterialModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<Item_MaterialModel> UpdateItem_Material(Item_MaterialModel Item_MaterialModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpItem_Material = unitOfWorkStore.GetRepository<Item_Material>();
                    Item_Material Item_MaterialEntity = rpItem_Material.GetById(Item_MaterialModel.Item_Material_ID);
                    Item_MaterialEntity.Name = Item_MaterialModel.Name;                   
                    rpItem_Material.Update(Item_MaterialEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        Item_MaterialModel.Name = Item_MaterialEntity.Name;
                        return new Response<Item_MaterialModel>((int)StatusResponses.Success, MessageResConst.Success, Item_MaterialModel);
                    }
                    else
                    {
                        return new Response<Item_MaterialModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, Item_MaterialModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_MaterialModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<Item_MaterialModel> GetItem_MaterialByID(int iItem_MaterialID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Material = unitOfWorkStore.GetRepository<Item_Material>();
                    var obItem_Material = rpItem_Material.GetById(iItem_MaterialID);

                    Item_MaterialModel Item_MaterialModel = new Item_MaterialModel()
                    {
                        Item_Material_ID = obItem_Material.Item_Material_ID,
                        Name= obItem_Material.Name
                    };
                    return new Response<Item_MaterialModel>((int)StatusResponses.Success, MessageResConst.Success, Item_MaterialModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_MaterialModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<Item_MaterialModel>> GetItem_Materials(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, Item_MaterialModel item_Material)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Material = unitOfWorkStore.GetRepository<Item_Material>();
                    var listItem_MaterialEntity = rpItem_Material.GetAll();
                    var listItem_MaterialModel = (from Item_Material in listItem_MaterialEntity
                                             select new Item_MaterialModel()
                                             {
                                                 Item_Material_ID = Item_Material.Item_Material_ID,
                                                 Name = Item_Material.Name 
                                             }).ToList();

                    // search                   
                    int countData = listItem_MaterialModel.Count;
                    listItem_MaterialModel = listItem_MaterialModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order                   
                    listItem_MaterialModel = listItem_MaterialModel.OrderBy(x => x.Name).ToList();
                    return new Response<List<Item_MaterialModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listItem_MaterialModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Item_MaterialModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }

        public Response<Item_MaterialModel> Delete(int idItem_Material)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Material = unitOfWorkStore.GetRepository<Item_Material>();
                    Item_Material Item_MaterialEntity = rpItem_Material.GetById(idItem_Material);
                    rpItem_Material.Delete(Item_MaterialEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<Item_MaterialModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<Item_MaterialModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_MaterialModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
