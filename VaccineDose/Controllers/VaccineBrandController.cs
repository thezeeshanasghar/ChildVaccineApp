using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Collections;
using AutoMapper;
using System.Threading;
using System;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class VaccineBrandController : BaseController
    {
        #region C R U D

        public Response<IEnumerable<VaccineBrandDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineBrands = entities.VaccineBrands.ToList();
                    IEnumerable<VaccineBrandDTO> vaccineBrandDTOs = Mapper.Map<IEnumerable<VaccineBrandDTO>>(dbVaccineBrands);
                    return new Response<IEnumerable<VaccineBrandDTO>>(true, null, vaccineBrandDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<VaccineBrandDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<VaccineBrandDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineBrand = entities.VaccineBrands.Where(c => c.ID == Id).FirstOrDefault();
                    VaccineBrandDTO vaccineBrandDTO = Mapper.Map<VaccineBrandDTO>(dbVaccineBrand);
                    return new Response<VaccineBrandDTO>(true, null, vaccineBrandDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<VaccineBrandDTO>(false, GetMessageFromExceptionObject(e), null);

            }

        }

        public Response<VaccineBrandDTO> Post(VaccineBrandDTO vaccineBrandDTO)
        {
            try
            {

                using (VDConnectionString entities = new VDConnectionString())
                {
                    VaccineBrand dbVaccineBrand = Mapper.Map<VaccineBrand>(vaccineBrandDTO);
                    entities.VaccineBrands.Add(dbVaccineBrand);
                    entities.SaveChanges();
                    vaccineBrandDTO.ID = dbVaccineBrand.ID;
                    return new Response<VaccineBrandDTO>(true, null, vaccineBrandDTO);

                }
            }
            catch (Exception e)
            {
                return new Response<VaccineBrandDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }
        public Response<VaccineBrandDTO> Put(int Id, VaccineBrandDTO vaccineBrandDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineBrand = entities.VaccineBrands.Where(c => c.ID == Id).FirstOrDefault();
                    dbVaccineBrand = Mapper.Map<VaccineBrandDTO, VaccineBrand>(vaccineBrandDTO, dbVaccineBrand);
                    entities.SaveChanges();
                    return new Response<VaccineBrandDTO>(true, null, vaccineBrandDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<VaccineBrandDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineBrand = entities.VaccineBrands.Where(c => c.ID == Id).FirstOrDefault();
                    entities.VaccineBrands.Remove(dbVaccineBrand);
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
