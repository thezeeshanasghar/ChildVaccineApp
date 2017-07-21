using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VaccineDose.App_Code;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    //[RoutePrefix("api/child")]
    public class ChildController : ApiController
    {
        #region C R U D
        public Response<IEnumerable<ChildDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbchildren = entities.Children.ToList();
                    IEnumerable<ChildDTO> childDTOs = Mapper.Map<IEnumerable<ChildDTO>>(dbchildren);
                    return new Response<IEnumerable<ChildDTO>>(true, null, childDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ChildDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
            
        }

        public Response<ChildDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbChild = entities.Children.Where(c => c.ID == Id).FirstOrDefault();
                    ChildDTO ClinicDTO = Mapper.Map<ChildDTO>(dbChild);
                    return new Response<ChildDTO>(true, null, ClinicDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<ChildDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        public Response<ChildDTO> Post(ChildDTO childDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    Child childDB = Mapper.Map<Child>(childDTO);
                    entities.Children.Add(childDB);
                    User userDB = new User();
                    userDB.MobileNumber = childDTO.MobileNumber;
                    userDB.Password = childDTO.Password;
                    userDB.UserType = "PARENT";
                    entities.Users.Add(userDB);
                    entities.SaveChanges();
                    childDTO.ID = childDB.ID;
                    //send email to parent
                    UserEmail.ParentEmail(childDTO);
                    // TODO: Generate Schedule here
                    List<Vaccine> vaccines = entities.Vaccines.OrderBy(x => x.MinAge).ToList();
                    foreach (Vaccine v in vaccines)
                    {
                        List<Dose> doses = v.Doses.OrderBy(i => i.DoseOrder).ToList();

                        int gap = Convert.ToInt32(v.MinAge);
                        foreach (Dose d in doses)
                        {
                            gap = gap + Convert.ToInt32(d.GapInDays);
                            DateTime currentDate = DateTime.Now.AddDays(gap);
                            //DateTime currentDate = (childDB.DOB == null ? DateTime.Now.AddDays(gap) : Convert.ToDateTime(childDB.DOB).AddDays(gap);
                            Schedule cvd = new Schedule();
                            cvd.ChildId = childDTO.ID;
                            cvd.DoseId = d.ID;
                            cvd.IsDone = false;


                            //List< DoseRule> doseToRules = d.DoseRules.ToList();
                            //cvd.Date = DateTime.Now.AddDays( doseToRules[0].Days );
                            cvd.Date = currentDate;

                            entities.Schedules.Add(cvd);
                            entities.SaveChanges();
                        }
                    }
                    List<Dose> doses1 = entities.Doses.ToList();




                    return new Response<ChildDTO>(true, null, childDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<ChildDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        public Response<ChildDTO> Put([FromBody] ChildDTO childDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbChild = entities.Children.Where(c => c.ID == childDTO.ID).FirstOrDefault();
                    dbChild = Mapper.Map<ChildDTO, Child>(childDTO, dbChild);
                    entities.SaveChanges();
                    return new Response<ChildDTO>(true, null, childDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<ChildDTO>(false, GetMessageFromExceptionObject(e), null);
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
                if (ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                    return new Response<string>(false, "Cannot delete child because it schedule exits. Delete the child schedule first.", null);
                else
                    return new Response<string>(false, GetMessageFromExceptionObject(ex), null);
            }
        }

        private static string GetMessageFromExceptionObject(Exception ex)
        {
            String message = ex.Message;
            message += (ex.InnerException != null) ? ("<br />" + ex.InnerException.Message) : "";
            message += (ex.InnerException.InnerException != null) ? ("<br />" + ex.InnerException.InnerException.Message) : "";
            return message;
        }

        #endregion

        [Route("api/child/{id}/schedule")]
        public Response<IEnumerable<ScheduleDTO>> GetChildSchedule(int id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var child = entities.Children.FirstOrDefault(c => c.ID == id);
                    if (child == null)
                        return new Response<IEnumerable<ScheduleDTO>>(false, "Child not found", null);
                    else
                    {
                        var dbSchedules = child.Schedules.OrderBy(x => x.Date).ToList();
                        var schedulesDTO = Mapper.Map<List<ScheduleDTO>>(dbSchedules);
                        return new Response<IEnumerable<ScheduleDTO>>(true, null, schedulesDTO);
                    }
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }
    }
}
