using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System;
using VaccineDose.Model;
using System.Web.Http;

namespace VaccineDose.Controllers
{
    public class Brand2Controller : BaseController
    {
        #region C R U D

        public IHttpActionResult Get()
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbVaccineBrands = entities.Brands.ToList();
                    IEnumerable<BrandDTO> vaccineBrandDTOs = Mapper.Map<IEnumerable<BrandDTO>>(dbVaccineBrands);
                    return Ok(vaccineBrandDTOs);
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }
        }

        public IHttpActionResult Get(int Id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbVaccineBrand = entities.Brands.Where(c => c.ID == Id).FirstOrDefault();
                    if (dbVaccineBrand != null)
                    {
                        BrandDTO vaccineBrandDTO = Mapper.Map<BrandDTO>(dbVaccineBrand);
                        return Ok(vaccineBrandDTO);
                    }
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }

        }

        public IHttpActionResult Post(BrandDTO vaccineBrandDTO)
        {
            try
            {

                using (VDEntities entities = new VDEntities())
                {
                    Brand dbVaccineBrand = Mapper.Map<Brand>(vaccineBrandDTO);
                    entities.Brands.Add(dbVaccineBrand);
                    entities.SaveChanges();
                    vaccineBrandDTO.ID = dbVaccineBrand.ID;
                    return Ok(vaccineBrandDTO);
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }
        }

        public IHttpActionResult Put(int Id, BrandDTO vaccineBrandDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbVaccineBrand = entities.Brands.Where(c => c.ID == Id).FirstOrDefault();
                    if (dbVaccineBrand != null)
                    {
                        dbVaccineBrand.Name = vaccineBrandDTO.Name;
                        entities.SaveChanges();
                        return Ok(vaccineBrandDTO);
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
                    var dbVaccineBrand = entities.Brands.Where(c => c.ID == Id).FirstOrDefault();
                    if (dbVaccineBrand != null)
                    {
                        entities.Brands.Remove(dbVaccineBrand);
                        entities.SaveChanges();
                        return Ok(RECORD_DELETED);
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains(DELETE_CONFLICTED_WITH_REFERENCE_CONSTRAINT))
                    return Ok("Cannot delete brand because it is used in Child Schedule, Inventory or Invoice.");
                else
                    return Ok(GetMessageFromExceptionObject(ex));
            }
        }

        #endregion
    }
}
