using System.Linq;
using System.Web.Http;
using AutoMapper;
using System;

namespace VaccineDose.Controllers
{
    public class ClinicController : ApiController
    {
        #region C R U D
        public Response<ClinicDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbClinic = entities.Clinics.Where(c => c.ID == Id).FirstOrDefault();
                    ClinicDTO ClinicDTO = Mapper.Map<ClinicDTO>(dbClinic);
                    return new Response<ClinicDTO>(true, null, ClinicDTO);
                }
            }
            catch(Exception e)
            {
                return new Response<ClinicDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<ClinicDTO> Post([FromBody] ClinicDTO clinicDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    Clinic clinicDb = Mapper.Map<Clinic>(clinicDTO);
                    entities.Clinics.Add(clinicDb);
                    entities.SaveChanges();
                    clinicDTO.ID = clinicDb.ID;
                    return new Response<ClinicDTO>(true, null, clinicDTO);

                }
            }
            catch(Exception e)
            {
                return new Response<ClinicDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }
        public Response<ClinicDTO> Put(int Id, ClinicDTO clinicDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbClinic = entities.Clinics.Where(c => c.ID == Id).FirstOrDefault();
                    if (clinicDTO.IsOnline)
                    {
                        dbClinic.IsOnline = true;
                    }
                    else
                    {
                        clinicDTO.IsOnline = false;
                        dbClinic = Mapper.Map<ClinicDTO, Clinic>(clinicDTO, dbClinic);
                    }
                    entities.SaveChanges();
                    return new Response<ClinicDTO>(true, null, clinicDTO);
                }
            }
            catch(Exception e)
            {
                return new Response<ClinicDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbClinic = entities.Clinics.Where(c => c.ID == Id).FirstOrDefault();
                    entities.Clinics.Remove(dbClinic);
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

        #endregion

        private static string GetMessageFromExceptionObject(Exception ex)
        {
            String message = ex.Message;
            message += (ex.InnerException != null) ? ("<br />" + ex.InnerException.Message) : "";
            message += (ex.InnerException.InnerException != null) ? ("<br />" + ex.InnerException.InnerException.Message) : "";
            return message;
        }
    }
}
