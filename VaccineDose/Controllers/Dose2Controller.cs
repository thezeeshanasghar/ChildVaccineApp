using System.Linq;
using System.Web.Http;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace VaccineDose.Controllers
{
    public class Dose2Controller : BaseController
    {
        #region C R U D

        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable<DoseDTO> doseDTOs = null;
                using (VDEntities entities = new VDEntities())
                {
                    var dbDoses = entities.Doses.OrderBy(x => x.MinAge).ThenBy(x => x.Name).ToList();
                    doseDTOs = Mapper.Map<IEnumerable<DoseDTO>>(dbDoses);
                    return Ok(doseDTOs);
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
                DoseDTO doseDTO = null;
                using (VDEntities entities = new VDEntities())
                {
                    var dbDose = entities.Doses.Where(c => c.ID == Id).FirstOrDefault();
                    doseDTO = Mapper.Map<DoseDTO>(dbDose);
                    if (doseDTO != null)
                        return Ok(doseDTO);
                    else
                        return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }
        }

        public IHttpActionResult Post(DoseDTO doseDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    Dose doseDb = Mapper.Map<Dose>(doseDTO);
                    entities.Doses.Add(doseDb);
                    entities.SaveChanges();
                    doseDTO.ID = doseDb.ID;
                    return Ok(doseDTO);
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetMessageFromExceptionObject(e));
            }
        }

        public IHttpActionResult Put(int Id, DoseDTO doseDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbDose = entities.Doses.Where(c => c.ID == Id).FirstOrDefault();
                    if (dbDose != null)
                    {
                        dbDose.Name = doseDTO.Name == null ? dbDose.Name : doseDTO.Name;
                        dbDose.MinAge = doseDTO.MinAge == 0 ? dbDose.MinAge : doseDTO.MinAge;
                        dbDose.MaxAge = doseDTO.MaxAge == null ? dbDose.MaxAge : doseDTO.MaxAge;
                        dbDose.MinGap = doseDTO.MinGap == null ? dbDose.MinGap : doseDTO.MinGap;
                        dbDose.DoseOrder = doseDTO.DoseOrder == 0 ? dbDose.DoseOrder : doseDTO.DoseOrder;
                        entities.SaveChanges();
                        doseDTO = Mapper.Map<DoseDTO>(dbDose);
                        return Ok(doseDTO);
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
                    var dbDose = entities.Doses.Where(c => c.ID == Id).FirstOrDefault();
                    if (dbDose != null)
                    {
                        entities.Doses.Remove(dbDose);
                        entities.SaveChanges();
                        return Ok(RECORD_DELETED);
                    }
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains(DELETE_CONFLICTED_WITH_REFERENCE_CONSTRAINT))
                    return BadRequest("Cannot delete child because it schedule exits. Delete the child schedule first.");
                else
                    return BadRequest(GetMessageFromExceptionObject(ex));
            }
        }

        #endregion

    }
}
