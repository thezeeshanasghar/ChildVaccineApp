using System.Linq;
using System.Web.Http;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace VaccineDose.Controllers
{
    public class DoseController : BaseController
    {
        #region C R U D

        public Response<IEnumerable<DoseDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbDoses = entities.Doses.OrderBy(x=> x.Name).ToList();
                    IEnumerable<DoseDTO> doseDTOs = Mapper.Map<IEnumerable<DoseDTO>>(dbDoses);
                    return new Response<IEnumerable<DoseDTO>>(true, null, doseDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<DoseDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }


        public Response<DoseDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbDose = entities.Doses.Where(c => c.ID == Id).FirstOrDefault();
                    DoseDTO doseDTO = Mapper.Map<DoseDTO>(dbDose);
                    return new Response<DoseDTO>(true, null, doseDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<DoseDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }


        public Response<DoseDTO> Post(DoseDTO doseDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    Dose doseDb = Mapper.Map<Dose>(doseDTO);
                    entities.Doses.Add(doseDb);
                    entities.SaveChanges();
                    doseDTO.ID = doseDb.ID;
                    return new Response<DoseDTO>(true, null, doseDTO);

                }
            }
            catch (Exception e)
            {
                return new Response<DoseDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<DoseDTO> Put(int Id, DoseDTO doseDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbDose = entities.Doses.Where(c => c.ID == Id).FirstOrDefault();
                    dbDose.Name = doseDTO.Name;
                    dbDose.MinAge = doseDTO.MinAge;
                    dbDose.MaxAge = doseDTO.MaxAge;
                    dbDose.MinGap = doseDTO.MinGap;
                    dbDose.DoseOrder = doseDTO.DoseOrder;
                    entities.SaveChanges();
                    return new Response<DoseDTO>(true, null, doseDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<DoseDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbDose = entities.Doses.Where(c => c.ID == Id).FirstOrDefault();
                    entities.Doses.Remove(dbDose);
                    entities.SaveChanges();
                    return new Response<string>(true, null, "record deleted");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                    return new Response<string>(false, "Cannot delete child because it schedule exits. Delete the child schedule first.", null);
                else
                    return new Response<string>(false, GetMessageFromExceptionObject(ex), null);
            }
        }

        #endregion



    }
}
