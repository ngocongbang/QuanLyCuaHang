using log4net;
using ManagementStore.EntityFramwork.Responsitory;
using System;
using ManagementStore.Business.Common.Enums;
using ManagementStore.Business.Common.Constants;
using System.Collections.Generic;
using System.Linq;
using ManagementStore.EntityFramwork.DbContext;

namespace ManagementStore.Business.Items
{
    public class ItemHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<ItemModel> InsertItem(ItemModel ItemModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem = unitOfWorkStore.GetRepository<Item>();
                    Item ItemEntity = new Item();
                    ItemEntity.Item_ID = ItemModel.Item_ID;
                    ItemEntity.Item_Code = ItemModel.Item_Code;
                    ItemEntity.Name = ItemModel.Name;
                    ItemEntity.GroupItem_ID = ItemModel.GroupItem_ID;
                    ItemEntity.GroupItem_Sub_ID = ItemModel.GroupItem_Sub_ID;
                    ItemEntity.AmountSale = ItemModel.AmountSale;
                    ItemEntity.AmountOrigin = ItemModel.AmountOrigin;
                    ItemEntity.Blance = ItemModel.Blance;
                    ItemEntity.Item_Color_ID = ItemModel.Item_Color_ID;
                    ItemEntity.Item_Material_ID = ItemModel.Item_Material_ID;
                    ItemEntity.Item_Size_ID = ItemModel.Item_Size_ID;
                    ItemEntity.Unit = ItemModel.Unit;
                    ItemEntity.Note = ItemModel.Note;
                    ItemEntity.AmountShip = ItemModel.AmountShip;
                    rpItem.Add(ItemEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        ItemModel.Item_Code = ItemEntity.Item_Code;
                        return new Response<ItemModel>((int)StatusResponses.Success, MessageResConst.Success, ItemModel);
                    }
                    else
                    {
                        return new Response<ItemModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, ItemModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<ItemModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<ItemModel> UpdateItem(ItemModel ItemModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpItem = unitOfWorkStore.GetRepository<Item>();
                    Item ItemEntity = rpItem.GetById(ItemModel.Item_ID);
                    ItemEntity.Item_ID = ItemModel.Item_ID;
                    ItemEntity.Item_Code = ItemModel.Item_Code;
                    ItemEntity.Name = ItemModel.Name;
                    ItemEntity.GroupItem_ID = ItemModel.GroupItem_ID;
                    ItemEntity.GroupItem_Sub_ID = ItemModel.GroupItem_Sub_ID;
                    ItemEntity.AmountSale = ItemModel.AmountSale;
                    ItemEntity.AmountOrigin = ItemModel.AmountOrigin;
                    ItemEntity.Blance = ItemModel.Blance;
                    ItemEntity.Item_Color_ID = ItemModel.Item_Color_ID;
                    ItemEntity.Item_Material_ID = ItemModel.Item_Material_ID;
                    ItemEntity.Item_Size_ID = ItemModel.Item_Size_ID;
                    ItemEntity.Unit = ItemModel.Unit;
                    ItemEntity.Note = ItemModel.Note;
                    ItemEntity.AmountShip = ItemModel.AmountShip;
                    rpItem.Update(ItemEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        ItemModel.Item_Code = ItemEntity.Item_Code;
                        return new Response<ItemModel>((int)StatusResponses.Success, MessageResConst.Success, ItemModel);
                    }
                    else
                    {
                        return new Response<ItemModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, ItemModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<ItemModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<ItemModel> GetItemByID(int iItemID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem = unitOfWorkStore.GetRepository<Item>();
                    var obItem = rpItem.GetById(iItemID);

                    ItemModel ItemModel = new ItemModel()
                    {
                        Item_ID = obItem.Item_ID,
                        Item_Code = obItem.Item_Code,
                        Name = obItem.Name,
                        GroupItem_ID = obItem.GroupItem_ID,
                        GroupItem_Sub_ID = obItem.GroupItem_Sub_ID,
                        AmountSale = obItem.AmountSale,
                        AmountOrigin = obItem.AmountOrigin,
                        Blance = obItem.Blance,
                        Item_Color_ID = obItem.Item_Color_ID,
                        Item_Material_ID = obItem.Item_Material_ID,
                        Item_Size_ID = obItem.Item_Size_ID,
                        Unit = obItem.Unit,
                        Note = obItem.Note,
                        AmountShip = obItem.AmountShip,
                    };
                    return new Response<ItemModel>((int)StatusResponses.Success, MessageResConst.Success, ItemModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<ItemModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<ItemModel>> GetItems(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, ItemModel Item)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem = unitOfWorkStore.GetRepository<Item>();
                    var listItemEntity = rpItem.GetAll();
                    var listItemModel = (from obItem in listItemEntity
                                         select new ItemModel()
                                         {
                                             Item_ID = obItem.Item_ID,
                                             Item_Code = obItem.Item_Code,
                                             Name = obItem.Name,
                                             GroupItem_ID = obItem.GroupItem_ID,
                                             GroupItem_Sub_ID = obItem.GroupItem_Sub_ID,
                                             AmountSale = obItem.AmountSale,
                                             AmountOrigin = obItem.AmountOrigin,
                                             Blance = obItem.Blance,
                                             Item_Color_ID = obItem.Item_Color_ID,
                                             Item_Material_ID = obItem.Item_Material_ID,
                                             Item_Size_ID = obItem.Item_Size_ID,
                                             Unit = obItem.Unit,
                                             Note = obItem.Note,
                                             AmountShip = obItem.AmountShip,
                                         }).ToList();

                    // search
                    if (Item != null)
                    {
                        if (Item.Item_Code != null)
                        {
                            listItemModel = listItemModel.Where(x => x.Item_Code.Contains(Item.Item_Code)).ToList();
                        }
                        if (Item.Name != null)
                        {
                            listItemModel = listItemModel.Where(x => x.Name.ToLower().Contains(Item.Name.ToLower())).ToList();
                        }
                    }
                    int countData = listItemModel.Count;
                    listItemModel = listItemModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order
                    switch (orderid)
                    {
                        case "Item_Code":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listItemModel = listItemModel.OrderBy(x => x.Item_Code).ToList();
                            }
                            else
                            {
                                listItemModel = listItemModel.OrderByDescending(x => x.Item_Code).ToList();
                            }

                            break;
                        case "Name":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listItemModel = listItemModel.OrderBy(x => x.Name).ToList();
                            }
                            else
                            {
                                listItemModel = listItemModel.OrderByDescending(x => x.Name).ToList();
                            }
                            break;                       

                        default:
                            break;
                    }
                    return new Response<List<ItemModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listItemModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<ItemModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }

        public Response<ItemModel> Delete(int idItem)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpItem = unitOfWorkStore.GetRepository<Item>();
                    Item ItemEntity = rpItem.GetById(idItem);
                    rpItem.Delete(ItemEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<ItemModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<ItemModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<ItemModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
