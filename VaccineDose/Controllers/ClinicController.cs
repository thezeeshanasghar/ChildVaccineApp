using System.Linq;
using System.Web.Http;
using AutoMapper;

using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace VaccineDose.Controllers
{
    public class ClinicController : BaseController
    {
        #region C R U D
        public Response<IEnumerable<ClinicDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var clinics = entities.Clinics.Include("Doctor").ToList();
                    IEnumerable<ClinicDTO> clinicDTOs = Mapper.Map<IEnumerable<ClinicDTO>>(clinics);
                    return new Response<IEnumerable<ClinicDTO>>(true, null, clinicDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ClinicDTO>>(false, GetMessageFromExceptionObject(e), null);
            }

        }
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
            catch (Exception e)
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
            catch (Exception e)
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
                    clinicDTO.IsOnline = false;
                    dbClinic.Name = clinicDTO.Name;
                    dbClinic.ConsultationFee = clinicDTO.ConsultationFee;
                    dbClinic.StartTime = clinicDTO.StartTime;
                    dbClinic.EndTime = clinicDTO.EndTime;
                    dbClinic.PhoneNumber = clinicDTO.PhoneNumber;
                    dbClinic.OffDays = clinicDTO.OffDays;
                    dbClinic.Lat = clinicDTO.Lat;
                    dbClinic.Long = clinicDTO.Long;


                    entities.SaveChanges();
                    return new Response<ClinicDTO>(true, null, clinicDTO);
                }
            }
            catch (Exception e)
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

        [Route("api/clinic/{id}/childs")]
        public Response<IEnumerable<ChildDTO>> GetAllChildsOfaClinic(int id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var clinic = entities.Clinics.FirstOrDefault(c => c.ID == id);
                    if (clinic == null)
                        return new Response<IEnumerable<ChildDTO>>(false, "Clinic not found", null);
                    else
                    {
                        var doctorClinics = entities.Clinics.Where(x => x.DoctorID == clinic.DoctorID).ToList();
                        List<ChildDTO> childDTOs = new List<ChildDTO>();
                        List<Child> dbChild = new List<Child>();
                        foreach (var dc in doctorClinics)
                        {
                            var dbChildren = dc.Children.OrderByDescending(x => x.ID).ToList();
                            dbChild.AddRange(dbChildren);
                            childDTOs.AddRange(Mapper.Map<List<ChildDTO>>(dbChildren));
                        }

                        foreach (var item in childDTOs)
                        {
                            var dbMobileNumber = dbChild.Where(x => x.ID == item.ID).FirstOrDefault().User.MobileNumber;
                            if (dbMobileNumber != null)
                                item.MobileNumber = dbMobileNumber;
                        }
                        return new Response<IEnumerable<ChildDTO>>(true, null, childDTOs);
                    }
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ChildDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }


        [HttpPut]
        [Route("api/clinic/editClinic")]
        public Response<ClinicDTO> EditClinic(ClinicDTO clinicDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbClinic = entities.Clinics.Where(c => c.ID == clinicDTO.ID).FirstOrDefault();
                    if (clinicDTO.IsOnline)
                    {
                        dbClinic.IsOnline = true;

                    }

                    var clinicList = entities.Clinics.Where(x => x.DoctorID == clinicDTO.DoctorID).Where(x => x.ID != clinicDTO.ID).ToList();
                    if (clinicList.Count != 0)
                        foreach (var clinic in clinicList)
                        {
                            clinic.IsOnline = false;
                            entities.Clinics.Attach(clinic);
                            entities.Entry(clinic).State = EntityState.Modified;
                        }
                    entities.SaveChanges();
                    return new Response<ClinicDTO>(true, null, clinicDTO);
                }
            }
            catch (Exception ex)
            {
                return new Response<ClinicDTO>(false, ex.Message, null);
            }

        }
    }
}
