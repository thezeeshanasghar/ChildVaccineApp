using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Collections;
using AutoMapper;
using System.Threading;
using System;

namespace VaccineDose.Controllers
{
    public class DoctorScheduleController : BaseController
    {
        #region C R U D
        public Response<List<DoctorScheduleDTO>> Get(int Id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {

                    List<DoctorSchedule> doctorSchduleDBs = entities.DoctorSchedules.Include("Dose").Include("Doctor").Where(x => x.DoctorID == Id)
                        .OrderBy(x => x.Dose.MinAge).ThenBy(x => x.Dose.Name).ToList();
                    if (doctorSchduleDBs == null || doctorSchduleDBs.Count() == 0)
                        return new Response<List<DoctorScheduleDTO>>(false, "DoctorSchedule not found", null);

                    List<DoctorScheduleDTO> DoctorScheduleDTOs = Mapper.Map<List<DoctorScheduleDTO>>(doctorSchduleDBs);
                    return new Response<List<DoctorScheduleDTO>>(true, null, DoctorScheduleDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<List<DoctorScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<List<DoctorScheduleDTO>> Post(List<DoctorScheduleDTO> dsDTOS)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    foreach (var DoctorSchedueDTO in dsDTOS)
                    {
                        DoctorSchedule doctorSchduleDB = Mapper.Map<DoctorSchedule>(DoctorSchedueDTO);
                        entities.DoctorSchedules.Add(doctorSchduleDB);
                        entities.SaveChanges();
                        DoctorSchedueDTO.ID = doctorSchduleDB.ID;
                    }
                    return new Response<List<DoctorScheduleDTO>>(true, null, dsDTOS);
                }
            }
            catch (Exception e)
            {
                return new Response<List<DoctorScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<List<DoctorScheduleDTO>> Put(List<DoctorScheduleDTO> dsDTOS)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    foreach (var DoctorSchedueDTO in dsDTOS)
                    {
                        var doctorSchduleDB = entities.DoctorSchedules.Where(c => c.ID == DoctorSchedueDTO.ID).FirstOrDefault();
                        doctorSchduleDB.GapInDays = DoctorSchedueDTO.GapInDays;
                        entities.SaveChanges();
                    }
                    return new Response<List<DoctorScheduleDTO>>(true, null, dsDTOS);
                }
            }
            catch (Exception e)
            {
                return new Response<List<DoctorScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }


        #endregion

        [HttpGet]
        [Route("api/doctorschedule/update-schedule")]
        public Response<DoctorScheduleDTO> UpdateDoctorSchedule()
        {
            try
            {
                using (VDEntities db = new VDEntities())
                {
                    // 1- get all vaccines
                    var dbDoses = db.Doses.ToList();
                    var dbDoctors = db.Doctors.ToList<Doctor>();
                    foreach (var dbDoctor in dbDoctors)
                    {
                        var listOfIds = dbDoctor.DoctorSchedules.Select(x => x.DoseID);
                        var newDoses = db.Doses.Where(x => !listOfIds.Contains(x.ID)).ToList();
                        foreach (Dose newDose in newDoses)
                        {
                            dbDoctor.DoctorSchedules.Add(new DoctorSchedule()
                            {
                                DoctorID = dbDoctor.ID,
                                DoseID = newDose.ID,
                                GapInDays = newDose.MinAge
                            });

                            //////////////////////////////////////////////////
                            //// Add data in BrandInventory when
                            //// new Vaccines/Doses/Brands are added by admin
                            //// and then admin press UpdateDoctorSchedule
                            var brands = newDose.Vaccine.Brands;
                            foreach (var brand in brands)
                            {
                                BrandInventory existingBrandInventory = db.BrandInventories.Where(x => x.BrandID == brand.ID && x.DoctorID == dbDoctor.ID).FirstOrDefault<BrandInventory>();
                                if (existingBrandInventory == null)
                                {
                                    db.BrandInventories.Add(new BrandInventory()
                                    {
                                        Count = 0,
                                        DoctorID = dbDoctor.ID,
                                        BrandID = brand.ID
                                    });
                                    db.SaveChanges();
                                }

                                BrandAmount existingBrandAmount = db.BrandAmounts.Where(x => x.BrandID == brand.ID && x.DoctorID == dbDoctor.ID).FirstOrDefault<BrandAmount>();
                                if (existingBrandAmount == null)
                                {
                                    db.BrandAmounts.Add(new BrandAmount()
                                    {
                                        Amount = 0,
                                        DoctorID = dbDoctor.ID,
                                        BrandID = brand.ID
                                    });
                                    db.SaveChanges();
                                }
                            }
                            //////////////////////////////////////////////////



                            db.SaveChanges();
                        }
                    }
                    return new Response<DoctorScheduleDTO>(true, null, null);
                }
            }
            catch (Exception e)
            {
                return new Response<DoctorScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

    }
}
