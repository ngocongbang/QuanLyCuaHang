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

namespace ManagementStore.Business.Images
{
   public class ImageHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<ImageModel> InsertImage(ImageModel ImageModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpImage = unitOfWorkStore.GetRepository<Image>();
                    Image ImageEntity = new Image();
                    ImageEntity.Image_ID = ImageModel.Image_ID;
                    ImageEntity.Name = ImageModel.Name;
                    ImageEntity.Url = ImageModel.Url;
                    rpImage.Add(ImageEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        ImageModel.Name = ImageEntity.Name;
                        return new Response<ImageModel>((int)StatusResponses.Success, MessageResConst.Success, ImageModel);
                    }
                    else
                    {
                        return new Response<ImageModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, ImageModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<ImageModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<ImageModel> UpdateImage(ImageModel ImageModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpImage = unitOfWorkStore.GetRepository<Image>();
                    Image ImageEntity = rpImage.GetById(ImageModel.Image_ID);
                    ImageEntity.Image_ID = ImageModel.Image_ID;
                    ImageEntity.Name = ImageModel.Name;
                    ImageEntity.Url = ImageModel.Url;
                    rpImage.Update(ImageEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        ImageModel.Name = ImageEntity.Name;
                        return new Response<ImageModel>((int)StatusResponses.Success, MessageResConst.Success, ImageModel);
                    }
                    else
                    {
                        return new Response<ImageModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, ImageModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<ImageModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<ImageModel> GetImageByID(int iImageID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpImage = unitOfWorkStore.GetRepository<Image>();
                    var obImage = rpImage.GetById(iImageID);

                    ImageModel ImageModel = new ImageModel()
                    {
                        Image_ID = obImage.Image_ID,
                        Name = obImage.Name,
                        Url = obImage.Url
                    };
                    return new Response<ImageModel>((int)StatusResponses.Success, MessageResConst.Success, ImageModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<ImageModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<ImageModel>> GetImages(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, ImageModel ImageModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpImage = unitOfWorkStore.GetRepository<Image>();
                    var listImageEntity = rpImage.GetAll();
                    var listImageModel = (from image in listImageEntity
                                            select new ImageModel()
                                            {
                                                Image_ID = image.Image_ID,
                                                Name = image.Name,
                                                Url = image.Url

                                            }).ToList();

                    // search
                    if (ImageModel != null)
                    {
                        if (ImageModel.Name != null)
                        {
                            listImageModel = listImageModel.Where(x => x.Name.Contains(ImageModel.Name)).ToList();
                        }
                    }
                    int countData = listImageModel.Count;
                    listImageModel = listImageModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order
                    switch (orderid)
                    {
                        case "Name":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listImageModel = listImageModel.OrderBy(x => x.Name).ToList();
                            }
                            else
                            {
                                listImageModel = listImageModel.OrderByDescending(x => x.Name).ToList();
                            }

                            break;
                    }
                    return new Response<List<ImageModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listImageModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<ImageModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }

        public Response<ImageModel> Delete(int idImage)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpImage = unitOfWorkStore.GetRepository<Image>();
                    Image ImageEntity = rpImage.GetById(idImage);
                    rpImage.Delete(ImageEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<ImageModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<ImageModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<ImageModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
