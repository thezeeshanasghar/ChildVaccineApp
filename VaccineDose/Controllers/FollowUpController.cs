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
                using (VDEntities entities = new VDEntities())
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
                using (VDEntities entities = new VDEntities())
                {
                    var doctor = entities.Clinics.Where(x => x.ID == OnlineClinicID).First<Clinic>().Doctor;
                    int[] ClinicIDs = doctor.Clinics.Select(x => x.ID).ToArray<int>();

                    IEnumerable<FollowUp> followups = new List<FollowUp>();
                    DateTime AddedDateTime = DateTime.UtcNow.AddHours(5).AddDays(GapDays);
                    DateTime pakistanTime = DateTime.UtcNow.AddHours(5);
                    if (GapDays == 0)
                        followups = entities.FollowUps.Include("Child")
                            //.Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => ClinicIDs.Contains(c.Child.ClinicID))
                            .Where(c => System.Data.Entity.DbFunctions.TruncateTime(c.NextVisitDate) == System.Data.Entity.DbFunctions.TruncateTime(pakistanTime))
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.NextVisitDate).ToList<FollowUp>();
                    else if (GapDays > 0)
                    {
                        AddedDateTime = AddedDateTime.AddDays(1);
                        followups = entities.FollowUps.Include("Child")
                            //.Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => ClinicIDs.Contains(c.Child.ClinicID))
                            .Where(c => c.NextVisitDate > pakistanTime && c.NextVisitDate <= AddedDateTime)
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.NextVisitDate)
                            .ToList<FollowUp>();

                    }
                    else if (GapDays < 0)
                    {
                        followups = entities.FollowUps.Include("Child")
                            //.Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => ClinicIDs.Contains(c.Child.ClinicID))
                            .Where(c => c.NextVisitDate < pakistanTime && c.NextVisitDate >= AddedDateTime)
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
                using (VDEntities entities = new VDEntities())
                {
                    var dbChildFollowup = entities.FollowUps.Where(x => x.ChildID == childId).OrderByDescending(x => x.ID).FirstOrDefault();
                    UserSMS.ParentFollowUpSMSAlert(dbChildFollowup);
                    FollowUpDTO followupDTO = Mapper.Map<FollowUpDTO>(dbChildFollowup);
                    return new Response<FollowUpDTO>(true, null, followupDTO);
                }

            }
            catch (Exception ex)
            {
                return new Response<FollowUpDTO>(false, GetMessageFromExceptionObject(ex), null);
            }

        }

        [HttpGet]
        [Route("api/followup/bulk-sms-alert/{GapDays}/{doctorId}")]
        public Response<List<FollowUpDTO>> SendSMSAlertToAllChildren(int GapDays, int doctorId)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    DateTime AddedDateTime = DateTime.UtcNow.AddHours(5).AddDays(GapDays);
                    List<FollowUp> dbFollowUps = new List<FollowUp>();
                    if (GapDays == 0)
                    {
                        dbFollowUps = entities.FollowUps.Where(x => x.DoctorID == doctorId &&
                             x.NextVisitDate == DateTime.UtcNow.AddHours(5).Date)
                            .OrderByDescending(x => x.ID).ToList();

                    }
                    if (GapDays > 0)
                    {
                        dbFollowUps = entities.FollowUps.Where(x => x.DoctorID == doctorId &&
                             x.NextVisitDate >= DateTime.UtcNow.AddHours(5).Date && x.NextVisitDate <= AddedDateTime)
                            .OrderByDescending(x => x.ID).ToList();

                    }
                    if (GapDays < 0)
                    {
                        dbFollowUps = entities.FollowUps.Where(x => x.DoctorID == doctorId &&
                             x.NextVisitDate <= DateTime.UtcNow.AddHours(5).Date && x.NextVisitDate >= AddedDateTime)
                            .OrderByDescending(x => x.ID).ToList();

                    }

                    foreach (FollowUp followup in dbFollowUps)
                    {
                        UserSMS.ParentFollowUpSMSAlert(followup);
                    }
                    List<FollowUpDTO> dbFollowDTOs = Mapper.Map<List<FollowUpDTO>>(dbFollowUps);
                    return new Response<List<FollowUpDTO>>(true, null, dbFollowDTOs);
                }

            }
            catch (Exception ex)
            {
                return new Response<List<FollowUpDTO>>(false, GetMessageFromExceptionObject(ex), null);
            }

        }


    }
}
