using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
                    IEnumerable<FollowUp> followups = new List<FollowUp>();
                    DateTime AddedDateTime = DateTime.Now.AddDays(GapDays);
                    if (GapDays == 0)
                        followups = entities.FollowUps.Include("Child")
                            .Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => System.Data.Entity.DbFunctions.TruncateTime(c.NextVisitDate) == System.Data.Entity.DbFunctions.TruncateTime(DateTime.Now))
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.NextVisitDate).ToList<FollowUp>();
                    else if (GapDays > 0)
                        followups = entities.FollowUps.Include("Child")
                            .Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => c.NextVisitDate >= DateTime.Now && c.NextVisitDate <= AddedDateTime)
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.NextVisitDate)
                            .ToList<FollowUp>();
                    else if (GapDays < 0)
                        followups = entities.FollowUps.Include("Child")
                            .Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => c.NextVisitDate <= DateTime.Now && c.NextVisitDate >= AddedDateTime)
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.NextVisitDate)
                            .ToList<FollowUp>();
                    IEnumerable<FollowUpDTO> followUpDTO = Mapper.Map<IEnumerable<FollowUpDTO>>(followups);
                    return new Response<IEnumerable<FollowUpDTO>>(true, null, followUpDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<FollowUpDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }

    }
}
