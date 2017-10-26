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
    public class FollowUpController : BaseController
    {
        #region C R U D
        [HttpPost]
        [Route("api/followup")]
        public Response<FollowUpDTO> Post(FollowUpDTO FollowUpDto)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    FollowUp dbFollowUp = Mapper.Map<FollowUp>(FollowUpDto);
                    //call when followup add from parent side
                    if (dbFollowUp.DoctorID < 1)
                    {
                        dbFollowUp.DoctorID = DoctorID();
                    }
                    //
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
       public int DoctorID()
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbChild = entities.Children.Include("Clinic").FirstOrDefault();
                var dbDoctorID = dbChild.Clinic.DoctorID;
                return dbDoctorID;
            }
        }
 
    }
}
