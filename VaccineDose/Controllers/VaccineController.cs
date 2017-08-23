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
    public class VaccineController : BaseController
    {
        #region C R U D

        public Response<IEnumerable<VaccineDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccines = entities.Vaccines.ToList();
                    IEnumerable<VaccineDTO> vaccineDTOs = Mapper.Map<IEnumerable<VaccineDTO>>(dbVaccines);
                    return new Response<IEnumerable<VaccineDTO>>(true, null, vaccineDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<VaccineDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<VaccineDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccine = entities.Vaccines.Where(c => c.ID == Id).FirstOrDefault();
                    VaccineDTO vaccineDTO = Mapper.Map<VaccineDTO>(dbVaccine);
                    return new Response<VaccineDTO>(true, null, vaccineDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<VaccineDTO>(false, GetMessageFromExceptionObject(e), null);

            }

        }

        public Response<VaccineDTO> Post(VaccineDTO vaccineDTO)
        {
            try
            {

                using (VDConnectionString entities = new VDConnectionString())
                {
                    Vaccine vaccinedb = Mapper.Map<Vaccine>(vaccineDTO);
                    entities.Vaccines.Add(vaccinedb);
                    entities.SaveChanges();
                    vaccineDTO.ID = vaccinedb.ID;
                    return new Response<VaccineDTO>(true, null, vaccineDTO);

                }
            }
            catch (Exception e)
            {
                return new Response<VaccineDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }
        public Response<VaccineDTO> Put(int Id, VaccineDTO vaccineDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccine = entities.Vaccines.Where(c => c.ID == Id).FirstOrDefault();
                    dbVaccine = Mapper.Map<VaccineDTO, Vaccine>(vaccineDTO, dbVaccine);
                    entities.SaveChanges();
                    return new Response<VaccineDTO>(true, null, vaccineDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<VaccineDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccine = entities.Vaccines.Where(c => c.ID == Id).FirstOrDefault();
                    entities.Vaccines.Remove(dbVaccine);
                    entities.SaveChanges();
                    return new Response<string>(true, null, "record deleted");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                    return new Response<string>(false, "Cannot delete vaccine because it's doses exits. Delete the doses first.", null);
                else
                    return new Response<string>(false, GetMessageFromExceptionObject(ex), null);
            }
        }

        #endregion
       

        [Route("api/vaccine/{id}/dosses")]
        public Response<IEnumerable<DoseDTO>> GetDosses(int id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var vaccine = entities.Vaccines.FirstOrDefault(c => c.ID == id);
                    if (vaccine == null)
                        return new Response<IEnumerable<DoseDTO>>(false, "Vaccine not found", null);
                    else
                    {
                        var dbDosses = vaccine.Doses.ToList();
                        var dossesDTOs = Mapper.Map<List<DoseDTO>>(dbDosses);
                        return new Response<IEnumerable<DoseDTO>>(true, null, dossesDTOs);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<DoseDTO>>(false, GetMessageFromExceptionObject(ex), null);

            }
        }

        [Route("api/vaccine/{id}/brands")]
        public Response<IEnumerable<VaccineBrandDTO>> GetBrands(int id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var vaccine = entities.Vaccines.FirstOrDefault(c => c.ID == id);
                    if (vaccine == null)
                        return new Response<IEnumerable<VaccineBrandDTO>>(false, "Vaccine not found", null);
                    else
                    {
                        var dbBrands = vaccine.VaccineBrands.ToList();
                        var vaccineBrandDTOs = Mapper.Map<List<VaccineBrandDTO>>(dbBrands);
                        return new Response<IEnumerable<VaccineBrandDTO>>(true, null, vaccineBrandDTOs);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<VaccineBrandDTO>>(false, GetMessageFromExceptionObject(ex), null);

            }
        }
    }
}
