using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class BrandController : BaseController
    {
        #region C R U D

        public Response<IEnumerable<BrandDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineBrands = entities.Brands.ToList();
                    IEnumerable<BrandDTO> vaccineBrandDTOs = Mapper.Map<IEnumerable<BrandDTO>>(dbVaccineBrands);
                    return new Response<IEnumerable<BrandDTO>>(true, null, vaccineBrandDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<BrandDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        public Response<BrandDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineBrand = entities.Brands.Where(c => c.ID == Id).FirstOrDefault();
                    BrandDTO vaccineBrandDTO = Mapper.Map<BrandDTO>(dbVaccineBrand);
                    return new Response<BrandDTO>(true, null, vaccineBrandDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<BrandDTO>(false, GetMessageFromExceptionObject(e), null);

            }

        }

        public Response<BrandDTO> Post(BrandDTO vaccineBrandDTO)
        {
            try
            {

                using (VDConnectionString entities = new VDConnectionString())
                {
                    Brand dbVaccineBrand = Mapper.Map<Brand>(vaccineBrandDTO);
                    entities.Brands.Add(dbVaccineBrand);
                    entities.SaveChanges();
                    vaccineBrandDTO.ID = dbVaccineBrand.ID;
                    return new Response<BrandDTO>(true, null, vaccineBrandDTO);

                }
            }
            catch (Exception e)
            {
                return new Response<BrandDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }
        public Response<BrandDTO> Put(int Id, BrandDTO vaccineBrandDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineBrand = entities.Brands.Where(c => c.ID == Id).FirstOrDefault();
                    dbVaccineBrand = Mapper.Map<BrandDTO, Brand>(vaccineBrandDTO, dbVaccineBrand);
                    entities.SaveChanges();
                    return new Response<BrandDTO>(true, null, vaccineBrandDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<BrandDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineBrand = entities.Brands.Where(c => c.ID == Id).FirstOrDefault();
                    entities.Brands.Remove(dbVaccineBrand);
                    entities.SaveChanges();
                    return new Response<string>(true, null, "record deleted");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                    return new Response<string>(false, "The DELETE statement conflicted with the REFERENCE constraint", null);
                else
                    return new Response<string>(false, GetMessageFromExceptionObject(ex), null);
            }
        }

        #endregion
    }
}
