using log4net;
using System;
using ManagementStore.EntityFramwork.DbContext;
using ManagementStore.Business.Common.Enums;
using ManagementStore.Business.Common.Constants;
using ManagementStore.EntityFramwork.Responsitory;

namespace ManagementStore.Business.Vendors
{
    public class VendorHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

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
    }
}
