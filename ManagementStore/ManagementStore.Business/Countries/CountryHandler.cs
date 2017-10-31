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

namespace ManagementStore.Business.Countries
{
  public  class CountryHandler
    {
        private ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IDatabaseFactory dbFactory = new DatabaseFactory();

        public Response<CountryModel> InsertCountry(CountryModel CountryModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpCountry = unitOfWorkStore.GetRepository<Country>();
                    Country CountryEntity = new Country();
                    CountryEntity.Country_ID = CountryModel.Country_ID;
                    CountryEntity.Name = CountryModel.Name;                    
                    rpCountry.Add(CountryEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        CountryModel.Name = CountryEntity.Name;
                        return new Response<CountryModel>((int)StatusResponses.Success, MessageResConst.Success, CountryModel);
                    }
                    else
                    {
                        return new Response<CountryModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, CountryModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<CountryModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<CountryModel> UpdateCountry(CountryModel CountryModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {

                    var rpCountry = unitOfWorkStore.GetRepository<Country>();
                    Country CountryEntity = rpCountry.GetById(CountryModel.Country_ID);
                    CountryEntity.Country_ID = CountryModel.Country_ID;
                    CountryEntity.Name = CountryModel.Name;                  
                    rpCountry.Update(CountryEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        CountryModel.Name = CountryEntity.Name;
                        return new Response<CountryModel>((int)StatusResponses.Success, MessageResConst.Success, CountryModel);
                    }
                    else
                    {
                        return new Response<CountryModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, CountryModel);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<CountryModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<CountryModel> GetCountryByID(int iCountryID)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpCountry = unitOfWorkStore.GetRepository<Country>();
                    var obCountry = rpCountry.GetById(iCountryID);

                    CountryModel CountryModel = new CountryModel()
                    {
                        Country_ID = obCountry.Country_ID,
                        Name = obCountry.Name,                       
                    };
                    return new Response<CountryModel>((int)StatusResponses.Success, MessageResConst.Success, CountryModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<CountryModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }

        public Response<List<CountryModel>> GetCountrys(int pageSize, int pageCurrent, string orderid, string sortDecOrInc, CountryModel CountryModel)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpCountry = unitOfWorkStore.GetRepository<Country>();
                    var listCountryEntity = rpCountry.GetAll();
                    var listCountryModel = (from Country in listCountryEntity
                                           select new CountryModel()
                                           {
                                               Country_ID = Country.Country_ID,
                                               Name = Country.Name,
                                             
                                           }).ToList();

                    // search
                    if (CountryModel != null)
                    {
                        if (CountryModel.Name != null)
                        {
                            listCountryModel = listCountryModel.Where(x => x.Name.Contains(CountryModel.Name)).ToList();
                        }
                    }
                    int countData = listCountryModel.Count;
                    listCountryModel = listCountryModel.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                    // order
                    switch (orderid)
                    {
                        case "Name":
                            if (sortDecOrInc == MessageResConst.Increase)
                            {
                                listCountryModel = listCountryModel.OrderBy(x => x.Name).ToList();
                            }
                            else
                            {
                                listCountryModel = listCountryModel.OrderByDescending(x => x.Name).ToList();
                            }

                            break;                       
                    }
                    return new Response<List<CountryModel>>((int)StatusResponses.Success, countData, MessageResConst.Success, listCountryModel);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<CountryModel>>((int)StatusResponses.ErrorSystem, 0, ex.Message, null);
            }
        }

        public Response<CountryModel> Delete(int idCountry)
        {
            try
            {
                using (var unitOfWorkStore = new UnitOfWorkStore(dbFactory))
                {
                    var rpCountry = unitOfWorkStore.GetRepository<Country>();
                    Country CountryEntity = rpCountry.GetById(idCountry);
                    rpCountry.Delete(CountryEntity);
                    if (unitOfWorkStore.Save() >= 1)
                    {
                        return new Response<CountryModel>((int)StatusResponses.Success, MessageResConst.Success, null);
                    }
                    else
                    {
                        return new Response<CountryModel>((int)StatusResponses.ErrorSystem, MessageResConst.ErrorCommonRequestParam, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<CountryModel>((int)StatusResponses.ErrorSystem, ex.Message, null);
            }
        }
    }
}
