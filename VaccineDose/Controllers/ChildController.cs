using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    //[RoutePrefix("api/child")]
    public class ChildController : ApiController
    {
        #region C R U D
        public Response<IEnumerable<ChildDTO>> Get()
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbchildren = entities.Children.ToList();
                IEnumerable<ChildDTO> childDTOs = Mapper.Map<IEnumerable<ChildDTO>>(dbchildren);
                return new Response<IEnumerable<ChildDTO>>(true, null, childDTOs);
            }
        }

        public Response<ChildDTO> Get(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbChild = entities.Children.Where(c => c.ID == Id).FirstOrDefault();
                ChildDTO ClinicDTO = Mapper.Map<ChildDTO>(dbChild);
                return new Response<ChildDTO>(true, null, ClinicDTO);
            }
        }

        public Response<ChildDTO> Post(ChildDTO childDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                Child clinicDb = Mapper.Map<Child>(childDTO);
                entities.Children.Add(clinicDb);
                User userDB = new User();
                userDB.MobileNumber = childDTO.MobileNumber;
                userDB.Password = childDTO.Password;
                userDB.UserType = "PARENT";
                entities.Users.Add(userDB);
                entities.SaveChanges();
                childDTO.ID = clinicDb.ID;
                // TODO: Generate Schedule here
                List<Vaccine> vaccines = entities.Vaccines.OrderBy(x => x.MinAge).ToList();
                foreach (Vaccine v in vaccines)
                {
                    List<Dose> doses = v.Doses.OrderBy(x => x.ID).ToList();
                    
                    foreach (Dose d in doses)
                    {
                        Schedule cvd = new Schedule();
                        cvd.ChildId = childDTO.ID;
                        cvd.DoseId = d.ID;
                        cvd.IsDone = false;


                        //List< DoseRule> doseToRules = d.DoseRules.ToList();
                        //cvd.Date = DateTime.Now.AddDays( doseToRules[0].Days );
                        cvd.Date = DateTime.Now.AddDays(v.MinAge ?? 0);

                        entities.Schedules.Add(cvd);
                        entities.SaveChanges();
                    }
                }
                List<Dose> doses1 = entities.Doses.ToList();




                return new Response<ChildDTO>(true, null, childDTO);
            }
        }

        public Response<ChildDTO> Put([FromBody] ChildDTO childDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbChild = entities.Children.Where(c => c.ID == childDTO.ID).FirstOrDefault();
                dbChild = Mapper.Map<ChildDTO, Child>(childDTO, dbChild);
                entities.SaveChanges();
                return new Response<ChildDTO>(true, null, childDTO);
            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbChild = entities.Children.Where(c => c.ID == Id).FirstOrDefault();
                    entities.Children.Remove(dbChild);
                    entities.SaveChanges();
                    return new Response<string>(true, null, "record deleted");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Equals("Foreign Key"))
                {
                    return new Response<string>(false, "Foreign key issue message", null);
                }
                else {
                    String message = ex.Message;
                    message += (ex.InnerException != null) ? ("<br />" + ex.InnerException.Message) : "";
                    message += (ex.InnerException.InnerException != null) ? ("<br />" + ex.InnerException.InnerException.Message) : "";
                    return new Response<string>(false, message, null);
                }
            }
        }

        #endregion

        [Route("api/child/{id}/schedule")]
        public Response<IEnumerable<ScheduleDTO>> GetChildSchedule(int id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var child = entities.Children.FirstOrDefault(c => c.ID == id);
                if (child == null)
                    return new Response<IEnumerable<ScheduleDTO>>(false, "Child not found", null);
                else
                {
                    var dbSchedules = child.Schedules.ToList();
                    var schedulesDTO = Mapper.Map<List<ScheduleDTO>>(dbSchedules);
                    return new Response<IEnumerable<ScheduleDTO>>(true, null, schedulesDTO);
                }
            }
        }
    }
}
