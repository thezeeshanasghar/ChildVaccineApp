using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VaccineDose.App_Code;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class FollowUpController : BaseController
    {
        #region C R U D

        public Response<FollowUpDTO> Post(FollowUpDTO FollowUpDto)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    FollowUp dbFollowUp = Mapper.Map<FollowUp>(FollowUpDto);
                    entities.FollowUps.Add(dbFollowUp);
                    entities.SaveChanges();
                    return new Response<FollowUpDTO>(true, null, FollowUpDto);
                }
            }
            catch (Exception e)
            {
                return new Response<FollowUpDTO>(false, GetMessageFromExceptionObject(e), null);

            }

        }
        #endregion
        [HttpGet]
        [Route("api/followup/alert/{GapDays}/{OnlineClinicID}")]
        public Response<IEnumerable<FollowUpDTO>> GetAlert(int GapDays, int OnlineClinicID)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var doctor = entities.Clinics.Where(x => x.ID == OnlineClinicID).First<Clinic>().Doctor;
                    int[] ClinicIDs = doctor.Clinics.Select(x => x.ID).ToArray<int>();

                    IEnumerable<FollowUp> followups = new List<FollowUp>();
                    DateTime AddedDateTime = DateTime.Now.AddDays(GapDays);
                    if (GapDays == 0)
                        followups = entities.FollowUps.Include("Child")
                            //.Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => ClinicIDs.Contains(c.Child.ClinicID))
                            .Where(c => System.Data.Entity.DbFunctions.TruncateTime(c.NextVisitDate) == System.Data.Entity.DbFunctions.TruncateTime(DateTime.Now))
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.NextVisitDate).ToList<FollowUp>();
                    else if (GapDays > 0)
                    {
                        AddedDateTime = AddedDateTime.AddDays(1);
                        followups = entities.FollowUps.Include("Child")
                            //.Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => ClinicIDs.Contains(c.Child.ClinicID))
                            .Where(c => c.NextVisitDate > DateTime.Now && c.NextVisitDate <= AddedDateTime)
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.NextVisitDate)
                            .ToList<FollowUp>();

                    }
                    else if (GapDays < 0)
                    {
                        followups = entities.FollowUps.Include("Child")
                            //.Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => ClinicIDs.Contains(c.Child.ClinicID))
                            .Where(c => c.NextVisitDate < DateTime.Now && c.NextVisitDate >= AddedDateTime)
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.NextVisitDate)
                            .ToList<FollowUp>();
                    }
                        
                    IEnumerable<FollowUpDTO> followUpDTO = Mapper.Map<IEnumerable<FollowUpDTO>>(followups);
                    return new Response<IEnumerable<FollowUpDTO>>(true, null, followUpDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<FollowUpDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [HttpGet]
        [Route("api/followup/sms-alert/{childId}")]
        public Response<FollowUpDTO> SendSMSAlertToOneChild(int childId)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbChilFollowUp = entities.FollowUps.Where(x => x.ChildID == childId).FirstOrDefault();
                    UserSMS.ParentFollowUpSMSAlert(dbChilFollowUp);
                    FollowUpDTO scheduleDtos = Mapper.Map<FollowUpDTO>(dbChilFollowUp);
                    return new Response<FollowUpDTO>(true, null, scheduleDtos);
                }

            }
            catch (Exception ex)
            {
                return new Response<FollowUpDTO>(false, GetMessageFromExceptionObject(ex), null);
            }

        }


    }
}
