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
    public class Vaccine2Controller : BaseController
    {

        #region C R U D

        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable<VaccineDTO> vaccineDTOs = null;
                using (VDEntities entities = new VDEntities())
                {
                    var dbVaccines = entities.Vaccines.OrderBy(x => x.MinAge).ToList();
                    vaccineDTOs = Mapper.Map<IEnumerable<VaccineDTO>>(dbVaccines);
                    foreach (var vaccineDTO in vaccineDTOs)
                    {
                        vaccineDTO.NumOfBrands = entities.Vaccines.Where(x => x.ID == vaccineDTO.ID).First().Brands.Count();
                        vaccineDTO.NumOfDoses = entities.Vaccines.Where(x => x.ID == vaccineDTO.ID).First().Doses.Count();
                    }
                    return Ok(vaccineDTOs);
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }
        }

        public IHttpActionResult Get(int Id)
        {
            VaccineDTO vaccineDTO = null;
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbVaccine = entities.Vaccines.Where(c => c.ID == Id).FirstOrDefault();
                    vaccineDTO = Mapper.Map<VaccineDTO>(dbVaccine);
                    if (vaccineDTO != null)
                        return Ok(vaccineDTO);
                    else
                        return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }

        }

        public IHttpActionResult Post(VaccineDTO vaccineDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    Vaccine vaccinedb = Mapper.Map<Vaccine>(vaccineDTO);
                    entities.Vaccines.Add(vaccinedb);
                    entities.SaveChanges();
                    vaccineDTO.ID = vaccinedb.ID;
                    //add vaccine in brand
                    Brand dbBrand = new Brand();
                    dbBrand.VaccineID = vaccinedb.ID;
                    dbBrand.Name = "Local";
                    entities.Brands.Add(dbBrand);
                    entities.SaveChanges();
                    return Ok(vaccineDTO);
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }
        }

        public IHttpActionResult Put(int Id, VaccineDTO vaccineDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbVaccine = entities.Vaccines.Where(c => c.ID == Id).FirstOrDefault();
                    if (dbVaccine != null)
                    {
                        dbVaccine.Name = vaccineDTO.Name == null ? dbVaccine.Name : vaccineDTO.Name;
                        dbVaccine.MaxAge = vaccineDTO.MaxAge == null ? dbVaccine.MaxAge : vaccineDTO.MaxAge;
                        dbVaccine.MinAge = vaccineDTO.MinAge??dbVaccine.MinAge;
                        entities.SaveChanges();
                        vaccineDTO = Mapper.Map<VaccineDTO>(dbVaccine);
                        return Ok(vaccineDTO);
                    }
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }
        }

        public IHttpActionResult Delete(int Id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbVaccine = entities.Vaccines.Where(c => c.ID == Id).FirstOrDefault();
                    if (dbVaccine.Brands.Count > 0)
                        return BadRequest(CANNOT_DELETE_VACCINE_BRAND_ALREADY_EXISTS);
                    else if (dbVaccine == null)
                        NotFound();
                    entities.Vaccines.Remove(dbVaccine);
                    entities.SaveChanges();
                    return BadRequest(RECORD_DELETED);
                }
            }
            catch (Exception e)
            {
                if (e.InnerException.InnerException.Message.Contains(DELETE_CONFLICTED_WITH_REFERENCE_CONSTRAINT))
                    return BadRequest(CANNOT_DELETE_VACCINE_DOSES_ALREADY_EXISTS);
                else
                    return BadRequest(GetMessageFromExceptionObject(e));
            }
        }

        #endregion

        public static string sendRequest(string url)
        {
            System.Net.Http.HttpClient c = new System.Net.Http.HttpClient();
            var content = c.GetStringAsync(url).Result;
            return content.ToString();
        }

        [Route("api/vaccine2/{id}/dosses")]
        public IHttpActionResult GetDossesByVaccineId(int id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var vaccine = entities.Vaccines.FirstOrDefault(c => c.ID == id);
                    if (vaccine != null)
                    {
                        var dbDosses = vaccine.Doses.ToList();
                        var dossesDTOs = Mapper.Map<List<DoseDTO>>(dbDosses);
                        return Ok(dossesDTOs);
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetMessageFromExceptionObject(ex));
            }
        }

        [Route("api/vaccine2/{id}/brands")]
        public IHttpActionResult GetBrands(int id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var vaccine = entities.Vaccines.FirstOrDefault(c => c.ID == id);
                    if (vaccine != null)
                    {
                        var dbBrands = vaccine.Brands.ToList();
                        var vaccineBrandDTOs = Mapper.Map<List<BrandDTO>>(dbBrands);
                        return Ok(vaccineBrandDTOs);
                    }
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }
        }

        [HttpGet]
        [Route("api/vaccine2/vaccine-with-brands")]

        public IHttpActionResult GetAllVaccineWithBrands()
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbVaccines = entities.Vaccines.Include("Brands").ToList();
                    IEnumerable<VaccineDTO> vaccineDTOs = Mapper.Map<IEnumerable<VaccineDTO>>(dbVaccines);
                    return Ok(vaccineDTOs);
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }
        }
    }
}
