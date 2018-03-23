using System.Linq;
using System.Web.Http;
using AutoMapper;

using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class ClinicTimingController : BaseController
    {
        #region C R U D
        public Response<IEnumerable<ClinicTimingDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var clinicTimings = entities.ClinicTimings.Include("Clinic").ToList();
                    IEnumerable<ClinicTimingDTO> ClinicTimingDTOs = Mapper.Map<IEnumerable<ClinicTimingDTO>>(clinicTimings);
                    return new Response<IEnumerable<ClinicTimingDTO>>(true, null, ClinicTimingDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ClinicTimingDTO>>(false, GetMessageFromExceptionObject(e), null);
            }

        }
        public Response<ClinicTimingDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbClinicTiming = entities.ClinicTimings.Where(c => c.ID == Id).FirstOrDefault();
                    ClinicTimingDTO ClinicTimingDTO = Mapper.Map<ClinicTimingDTO>(dbClinicTiming);
                    return new Response<ClinicTimingDTO>(true, null, ClinicTimingDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<ClinicTimingDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<ClinicTimingDTO> Post([FromBody] ClinicTimingDTO ClinicTimingDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    ClinicTiming clinicTimingDb = Mapper.Map<ClinicTiming>(ClinicTimingDTO);
                    entities.ClinicTimings.Add(clinicTimingDb);
                    entities.SaveChanges();
                    ClinicTimingDTO.ID = clinicTimingDb.ID;
                    return new Response<ClinicTimingDTO>(true, null, ClinicTimingDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<ClinicTimingDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<ClinicTimingDTO> Put(int Id, ClinicTimingDTO ClinicTimingDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbClinicTiming = entities.ClinicTimings.Where(c => c.ID == Id).FirstOrDefault();
                    dbClinicTiming.StartTime = ClinicTimingDTO.StartTime;
                    dbClinicTiming.EndTime = ClinicTimingDTO.EndTime;
                    dbClinicTiming.Session = ClinicTimingDTO.Session;
                    dbClinicTiming.Day = ClinicTimingDTO.Day;
                    dbClinicTiming.IsOpen = ClinicTimingDTO.IsOpen;
                    dbClinicTiming.ClinicID = ClinicTimingDTO.ClinicID;

                    entities.SaveChanges();
                    return new Response<ClinicTimingDTO>(true, null, ClinicTimingDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<ClinicTimingDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbClinicTiming = entities.ClinicTimings.Where(c => c.ID == Id).FirstOrDefault();
                    entities.ClinicTimings.Remove(dbClinicTiming);
                    entities.SaveChanges();
                    return new Response<string>(true, null, "record deleted");
                }
            }
            catch (Exception ex)
            {
                    return new Response<string>(false, GetMessageFromExceptionObject(ex), null);
            }
        }

        #endregion
        
    }
}
