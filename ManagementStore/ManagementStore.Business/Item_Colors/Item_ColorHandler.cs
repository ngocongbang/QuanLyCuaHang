using log4net;
using ManagementStore.EntityFramwork.Responsitory;
using System;
using ManagementStore.Business.Common.Enums;
using ManagementStore.Business.Common.Constants;
using System.Collections.Generic;
using System.Linq;
using ManagementStore.EntityFramwork.DbContext;

namespace ManagementStore.Business.Item_Colors
{
    public class Item_ColorHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<Item_ColorModel> InsertItem_Color(Item_ColorModel Item_ColorModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Color = unitOfWorkStore.GetRepository<Item_Color>();
                    Item_Color Item_ColorEntity = new Item_Color();
                    Item_ColorEntity.Item_Color_ID = Item_ColorModel.Item_Color_ID;
                    Item_ColorEntity.Name = Item_ColorModel.Name;                   
                    rpItem_Color.Add(Item_ColorEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        Item_ColorModel.Item_Color_ID = Item_ColorEntity.Item_Color_ID;
                        return new Response<Item_ColorModel>((int)StatusResponses.Success, MessageResConst.Success, Item_ColorModel);
                    }
                    else
                    {
                        return new Response<Item_ColorModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, Item_ColorModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_ColorModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<Item_ColorModel> UpdateItem_Color(Item_ColorModel Item_ColorModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpItem_Color = unitOfWorkStore.GetRepository<Item_Color>();
                    Item_Color Item_ColorEntity = rpItem_Color.GetById(Item_ColorModel.Item_Color_ID);
                    Item_ColorEntity.Name = Item_ColorModel.Name;                   
                    rpItem_Color.Update(Item_ColorEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        Item_ColorModel.Name = Item_ColorEntity.Name;
                        return new Response<Item_ColorModel>((int)StatusResponses.Success, MessageResConst.Success, Item_ColorModel);
                    }
                    else
                    {
                        return new Response<Item_ColorModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, Item_ColorModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_ColorModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<Item_ColorModel> GetItem_ColorByID(int iItem_ColorID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Color = unitOfWorkStore.GetRepository<Item_Color>();
                    var obItem_Color = rpItem_Color.GetById(iItem_ColorID);

                    Item_ColorModel Item_ColorModel = new Item_ColorModel()
                    {
                        Item_Color_ID = obItem_Color.Item_Color_ID,
                        Name= obItem_Color.Name
                    };
                    return new Response<Item_ColorModel>((int)StatusResponses.Success, MessageResConst.Success, Item_ColorModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_ColorModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<Item_ColorModel>> GetItem_Colors(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, Item_ColorModel item_Color)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Color = unitOfWorkStore.GetRepository<Item_Color>();
                    var listItem_ColorEntity = rpItem_Color.GetAll();
                    var listItem_ColorModel = (from Item_Color in listItem_ColorEntity
                                             select new Item_ColorModel()
                                             {
                                                 Item_Color_ID = Item_Color.Item_Color_ID,
                                                 Name = Item_Color.Name 
                                             }).ToList();

                    // search                   
                    int countData = listItem_ColorModel.Count;
                    listItem_ColorModel = listItem_ColorModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order                   
                    listItem_ColorModel = listItem_ColorModel.OrderBy(x => x.Name).ToList();
                    return new Response<List<Item_ColorModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listItem_ColorModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<Item_ColorModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }

        public Response<Item_ColorModel> Delete(int idItem_Color)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem_Color = unitOfWorkStore.GetRepository<Item_Color>();
                    Item_Color Item_ColorEntity = rpItem_Color.GetById(idItem_Color);
                    rpItem_Color.Delete(Item_ColorEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<Item_ColorModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<Item_ColorModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<Item_ColorModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
