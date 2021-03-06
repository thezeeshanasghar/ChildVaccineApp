﻿using System.Linq;
using System.Web.Http;
using AutoMapper;

using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace VaccineDose.Controllers
{
    public class ClinicController : BaseController
    {
        #region C R U D
        public Response<IEnumerable<ClinicDTO>> Get()
        {
            try
            {
                using (VDEntities entities = new VDEntities())
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
                using (VDEntities entities = new VDEntities())
                {
                    var dbClinic = entities.Clinics.Include("ClinicTimings").Where(c => c.ID == Id).FirstOrDefault();
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
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                clinicDTO.Name = textInfo.ToTitleCase(clinicDTO.Name);
                using (VDEntities entities = new VDEntities())
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
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                clinicDTO.Name = textInfo.ToTitleCase(clinicDTO.Name);
                using (VDEntities entities = new VDEntities())
                {
                    var dbClinic = entities.Clinics.Where(c => c.ID == Id).FirstOrDefault();
                    clinicDTO.IsOnline = false;
                    dbClinic.Name = clinicDTO.Name;
                    dbClinic.ConsultationFee = clinicDTO.ConsultationFee;
                    dbClinic.PhoneNumber = clinicDTO.PhoneNumber;
                    dbClinic.Lat = clinicDTO.Lat;
                    dbClinic.Long = clinicDTO.Long;
                    dbClinic.Address = clinicDTO.Address;
                    entities.SaveChanges();
                    foreach (var clinicTiming in clinicDTO.ClinicTimings)
                    {
                        ClinicTiming dbClinicTiming = entities.ClinicTimings.Where(x => x.ID == clinicTiming.ID).FirstOrDefault();
                        if (dbClinicTiming != null)
                        {
                            dbClinicTiming.ClinicID = Id;
                            dbClinicTiming.Day = clinicTiming.Day;
                            dbClinicTiming.StartTime = clinicTiming.StartTime;
                            dbClinicTiming.EndTime = clinicTiming.EndTime;
                            dbClinicTiming.Session = clinicTiming.Session;
                            dbClinicTiming.IsOpen = clinicTiming.IsOpen;
                        }
                        else if (dbClinicTiming == null && clinicTiming.IsOpen)
                        {
                            ClinicTiming newClinicTiming = new ClinicTiming();
                            newClinicTiming.ClinicID = Id;
                            newClinicTiming.Day = clinicTiming.Day;
                            newClinicTiming.StartTime = clinicTiming.StartTime;
                            newClinicTiming.EndTime = clinicTiming.EndTime;
                            newClinicTiming.Session = clinicTiming.Session;
                            newClinicTiming.IsOpen = clinicTiming.IsOpen;
                            entities.ClinicTimings.Add(newClinicTiming);
                        }
                        entities.SaveChanges();
                    }
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
                using (VDEntities entities = new VDEntities())
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


        [HttpPut]
        [Route("api/clinic/editClinic")]
        public Response<ClinicDTO> EditClinic(ClinicDTO clinicDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
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
                    clinicDTO.Name = dbClinic.Name;
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
