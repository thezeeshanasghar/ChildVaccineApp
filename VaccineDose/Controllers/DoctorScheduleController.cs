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
        
        public Response<List<DoctorScheduleDTO>> Post(List<DoctorScheduleDTO> dsDTOS)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    foreach(var DoctorSchedueDTO in dsDTOS)
                    {
                        DoctorSchedule doctorSchduleDB = Mapper.Map<DoctorSchedule>(DoctorSchedueDTO);
                        entities.DoctorSchedules.Add(doctorSchduleDB);
                        entities.SaveChanges();
                        DoctorSchedueDTO.ID = doctorSchduleDB.ID;
                    }
                    return new Response<List<DoctorScheduleDTO>>(true, null, dsDTOS);
                }
            } catch (Exception e) {
                return new Response<List<DoctorScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }


        #endregion
    }
}
