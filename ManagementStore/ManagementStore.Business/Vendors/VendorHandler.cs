using log4net;
using System;
using ManagementStore.EntityFramwork.DbContext;
using ManagementStore.Business.Common.Enums;
using ManagementStore.Business.Common.Constants;
using ManagementStore.EntityFramwork.Responsitory;
using System.Collections.Generic;
using System.Linq;

namespace ManagementStore.Business.Vendors
{
    public class VendorHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();
        // hàm insert dữ liệu
        public Response<VendorModel> InsertVendor(VendorModel vendorModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpVendor = unitOfWorkStore.GetRepository<Vendor>();
                    Vendor vendorEntity = new Vendor();
                    vendorEntity.Vendor_ID = vendorModel.Vendor_ID;
                    vendorEntity.Vendor_Code = vendorModel.Vendor_Code;
                    vendorEntity.Name = vendorModel.Name;
                    vendorEntity.Tax_Code = vendorModel.Tax_Code;
                    vendorEntity.Phone = vendorModel.Phone;
                    vendorEntity.Address = vendorModel.Address;
                    vendorEntity.Region = vendorModel.Region;
                    vendorEntity.CommuneWard = vendorModel.CommuneWard;
                    vendorEntity.Email = vendorModel.Email;
                    vendorEntity.Group_Vendor = vendorModel.Group_Vendor;
                    vendorEntity.Note = vendorModel.Note;
                    rpVendor.Add(vendorEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        vendorModel.Vendor_Code = vendorEntity.Vendor_Code;
                        return new Response<VendorModel>((int)StatusResponses.Success, MessageResConst.Success, vendorModel);
                    }
                    else
                    {
                        return new Response<VendorModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, vendorModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<VendorModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
        // Hàm update dữ liệu
        public Response<VendorModel> UpdateVendor(VendorModel vendorModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpVendor = unitOfWorkStore.GetRepository<Vendor>();
                    Vendor vendorEntity = rpVendor.GetById(vendorModel.Vendor_ID);
                    vendorEntity.Vendor_ID = vendorModel.Vendor_ID;
                    vendorEntity.Vendor_Code = vendorModel.Vendor_Code;
                    vendorEntity.Name = vendorModel.Name;
                    vendorEntity.Tax_Code = vendorModel.Tax_Code;
                    vendorEntity.Phone = vendorModel.Phone;
                    vendorEntity.Address = vendorModel.Address;
                    vendorEntity.Region = vendorModel.Region;
                    vendorEntity.CommuneWard = vendorModel.CommuneWard;
                    vendorEntity.Email = vendorModel.Email;
                    vendorEntity.Group_Vendor = vendorModel.Group_Vendor;
                    vendorEntity.Note = vendorModel.Note;
                    rpVendor.Update(vendorEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        vendorModel.Vendor_Code = vendorEntity.Vendor_Code;
                        return new Response<VendorModel>((int)StatusResponses.Success, MessageResConst.Success, vendorModel);
                    }
                    else
                    {
                        return new Response<VendorModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, vendorModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<VendorModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
        // hàm lấy dữ liệu theo id
        public Response<VendorModel> GetVendorByID(int iVendorID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpVendor = unitOfWorkStore.GetRepository<Vendor>();
                    var obRestaurant = rpVendor.GetById(iVendorID);

                    VendorModel vendorModel = new VendorModel()
                    {
                        Vendor_ID = obRestaurant.Vendor_ID,
                        Vendor_Code = obRestaurant.Vendor_Code,
                        Name = obRestaurant.Name,
                        Tax_Code = obRestaurant.Tax_Code,
                        Phone = obRestaurant.Phone,
                        Address = obRestaurant.Address,
                        Region = obRestaurant.Region,
                        CommuneWard = obRestaurant.CommuneWard,
                        Email = obRestaurant.Email,
                        Group_Vendor = obRestaurant.Group_Vendor,
                        Note = obRestaurant.Note                      
                    };
                    return new Response<VendorModel>((int)StatusResponses.Success, MessageResConst.Success, vendorModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<VendorModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
        // tạo hàm lấy tất cả dữ liệu
        public Response<List<VendorModel>> GetVendors()
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpVendor = unitOfWorkStore.GetRepository<Vendor>();
                    var listVendorEntity = rpVendor.GetAll();
                    var listVendorModel = (from vendor in listVendorEntity
                                           select new VendorModel()
                                          {
                                              Vendor_ID = vendor.Vendor_ID,
                                              Vendor_Code = vendor.Vendor_Code,
                                              Name = vendor.Name,
                                              Tax_Code = vendor.Tax_Code,
                                              Phone = vendor.Phone,
                                              Address = vendor.Address,
                                              Region = vendor.Region,
                                              CommuneWard = vendor.CommuneWard,
                                              Email = vendor.Email,
                                              Group_Vendor = vendor.Group_Vendor,
                                              Note = vendor.Note
                                          }).ToList();                    
                    return new Response<List<VendorModel>>((int)StatusResponses.Success, MessageResConst.Success, listVendorModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<VendorModel>>((int)StatusResponses.ErrorSystem, ex.Message, null);               
            }
        }
        // tạo hàm xóa 
        public Response<VendorModel> Delete (int iVendorID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpVendor = unitOfWorkStore.GetRepository<Vendor>();
                    Vendor vendorEntity = rpVendor.GetById(iVendorID);
                    
                    rpVendor.Delete(vendorEntity);
                    //unitOfWorkStore.Save();
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<VendorModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<VendorModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<VendorModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
