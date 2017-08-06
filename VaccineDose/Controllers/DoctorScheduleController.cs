using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Collections;
using AutoMapper;
using System.Threading;
using System;

namespace VaccineDose.Controllers
{
    public class DoctorScheduleController : BaseController
    {
        #region C R U D
        public Response<List<DoctorScheduleDTO>> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {

                    List<DoctorSchedule> doctorSchduleDBs = entities.DoctorSchedules.Include("Dose").Include("Doctor").Where(x => x.DoctorID == Id).ToList();
                    if(doctorSchduleDBs==null || doctorSchduleDBs.Count()==0)
                        return new Response<List<DoctorScheduleDTO>>(false, "DoctorSchedule not found", null);

                    List<DoctorScheduleDTO> DoctorScheduleDTOs = Mapper.Map<List<DoctorScheduleDTO>>(doctorSchduleDBs);
                    return new Response<List<DoctorScheduleDTO>>(true, null, DoctorScheduleDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<List<DoctorScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<List<DoctorScheduleDTO>> Post(List<DoctorScheduleDTO> dsDTOS)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    foreach (var DoctorSchedueDTO in dsDTOS)
                    {
                        DoctorSchedule doctorSchduleDB = Mapper.Map<DoctorSchedule>(DoctorSchedueDTO);
                        entities.DoctorSchedules.Add(doctorSchduleDB);
                        entities.SaveChanges();
                        DoctorSchedueDTO.ID = doctorSchduleDB.ID;
                    }
                    return new Response<List<DoctorScheduleDTO>>(true, null, dsDTOS);
                }
            }
            catch (Exception e)
            {
                return new Response<List<DoctorScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<List<DoctorScheduleDTO>> Put(List<DoctorScheduleDTO> dsDTOS)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    foreach (var DoctorSchedueDTO in dsDTOS)
                    {
                        var doctorSchduleDB = entities.DoctorSchedules.Where(c => c.ID == DoctorSchedueDTO.ID).FirstOrDefault();
                        doctorSchduleDB.GapInDays = DoctorSchedueDTO.GapInDays;
                        entities.SaveChanges();
                    }
                    return new Response<List<DoctorScheduleDTO>>(true, null, dsDTOS);
                }
            }
            catch (Exception e)
            {
                return new Response<List<DoctorScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }


        #endregion


    }
}
