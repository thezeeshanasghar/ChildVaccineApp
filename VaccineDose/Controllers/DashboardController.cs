using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class DashboardController : BaseController
    {
        public IHttpActionResult getCountOfDifferentCases()
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    DashboardDTO dto = new DashboardDTO();

                    dto.VaccineCount = entities.Vaccines.Count();
                    dto.DoseCount = entities.Doses.Count();
                    dto.BrandCount = entities.Brands.Count();
                    dto.ChildCount = entities.Children.Count();
                    dto.ApprovedDoctorCount = entities.Doctors.Where(e => e.IsApproved).Count();
                    dto.UnApprovedDoctorCount = entities.Doctors.Count() - dto.ApprovedDoctorCount;

                    return Ok(dto);
                }
            }
            catch (Exception e)
            {                 
                return BadRequest(GetMessageFromExceptionObject(e));
            }
        }
    }
}

