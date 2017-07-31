using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VaccineDose.Controllers
{
     public class ScheduleController : BaseController
    {
        #region C R U D
      
        public Response<ScheduleDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbSchedule = entities.Schedules.Where(c => c.ID == Id).FirstOrDefault();
                    ScheduleDTO scheduleDTOs = Mapper.Map<ScheduleDTO>(dbSchedule);
                    return new Response<ScheduleDTO>(true, null, scheduleDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }
     
        #endregion
       [HttpPut]
       [Route("api/schedule/child-schedule")]
       public Response<ScheduleDTO> Update(ScheduleDTO scheduleDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbSchedule = entities.Schedules.Where(c => c.ID == scheduleDTO.ID).FirstOrDefault();
                    dbSchedule.Weight = scheduleDTO.Weight;
                    dbSchedule.Height = scheduleDTO.Height;
                    dbSchedule.Circle = scheduleDTO.Circle;
                    dbSchedule.Brand = scheduleDTO.Brand;
                    dbSchedule.IsDone = scheduleDTO.IsDone;
                    entities.SaveChanges();
                    ScheduleDTO scheduleDTOs = Mapper.Map<ScheduleDTO>(dbSchedule);
                    return new Response<ScheduleDTO>(true, null, scheduleDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        [HttpGet]
        [Route("api/schedule/alert/{Id}")]
        public Response<IEnumerable<ScheduleDTO>> GetAlert(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    IEnumerable<Schedule> schedules = new List<Schedule>();
                    DateTime AddedDateTime = DateTime.Now.AddDays(Id);
                    if (Id == 0)
                        schedules = entities.Schedules.Include("Child").Include("Dose")
                            .Where(c => c.Date == DateTime.Now)
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.Date).ToList<Schedule>();
                    else if (Id > 0)
                        schedules = entities.Schedules.Include("Child").Include("Dose")
                            .Where(c => c.Date >= DateTime.Now && c.Date <= AddedDateTime)
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.Date)
                            .ToList<Schedule>();
                    else if (Id <0)
                        schedules = entities.Schedules.Include("Child").Include("Dose")
                            .Where(c => c.Date <= DateTime.Now && c.Date >= AddedDateTime)
                            .OrderBy(x=>x.Child.ID).ThenBy(x=>x.Date)
                            .ToList<Schedule>();
                    IEnumerable<ScheduleDTO> scheduleDTO = Mapper.Map<IEnumerable<ScheduleDTO>>(schedules);
                    return new Response<IEnumerable<ScheduleDTO>>(true, null, scheduleDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }

    }
}
