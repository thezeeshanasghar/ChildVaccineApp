using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class ScheduleController : BaseController
    {
        #region C R U D

        public Response<ScheduleDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbSchedule = entities.Schedules.Where(c => c.ID == Id).FirstOrDefault();
                    ScheduleDTO scheduleDTOs = Mapper.Map<ScheduleDTO>(dbSchedule);
                    int vaccineId = dbSchedule.Dose.VaccineID;
                    var dbBrands = entities.Brands.Where(b => b.VaccineID == vaccineId).ToList();
                    List<BrandDTO> brandDTOs = Mapper.Map<List<BrandDTO>>(dbBrands);
                    scheduleDTOs.Brands = brandDTOs;
                    return new Response<ScheduleDTO>(true, null, scheduleDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        #endregion
        [HttpPut]
        [Route("api/schedule/child-schedule")]
        public Response<ScheduleDTO> Update(ScheduleDTO scheduleDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbBrandInventory = entities.BrandInventories.Where(b => b.BrandID == scheduleDTO.BrandId
                    && b.DoctorID == scheduleDTO.DoctorID).FirstOrDefault();
                    var dbSchedule = entities.Schedules.Where(c => c.ID == scheduleDTO.ID).FirstOrDefault();
                    if (dbBrandInventory.Count > 0)
                    {
                        dbBrandInventory.Count--;
                        dbSchedule.Weight = scheduleDTO.Weight;
                        dbSchedule.Height = scheduleDTO.Height;
                        dbSchedule.Circle = scheduleDTO.Circle;
                        dbSchedule.IsDone = scheduleDTO.IsDone;
                        dbSchedule.BrandId = scheduleDTO.BrandId;
                        entities.SaveChanges();
                    }

                    ScheduleDTO scheduleDTOs = Mapper.Map<ScheduleDTO>(dbSchedule);
                    return new Response<ScheduleDTO>(true, null, scheduleDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        [HttpGet]
        [Route("api/schedule/alert/{GapDays}/{OnlineClinicID}")]
        public Response<IEnumerable<ScheduleDTO>> GetAlert(int GapDays, int OnlineClinicID)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    IEnumerable<Schedule> schedules = new List<Schedule>();
                    DateTime AddedDateTime = DateTime.Now.AddDays(GapDays);
                    if (GapDays == 0)
                        schedules = entities.Schedules.Include("Child").Include("Dose")
                            .Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => c.Date == DateTime.Now)
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.Date).ToList<Schedule>();
                    else if (GapDays > 0)
                        schedules = entities.Schedules.Include("Child").Include("Dose")
                            .Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => c.Date >= DateTime.Now && c.Date <= AddedDateTime)
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.Date)
                            .ToList<Schedule>();
                    else if (GapDays < 0)
                        schedules = entities.Schedules.Include("Child").Include("Dose")
                            .Where(c => c.Child.ClinicID == OnlineClinicID)
                            .Where(c => c.Date <= DateTime.Now && c.Date >= AddedDateTime)
                            .OrderBy(x => x.Child.ID).ThenBy(x => x.Date)
                            .ToList<Schedule>();
                    IEnumerable<ScheduleDTO> scheduleDTO = Mapper.Map<IEnumerable<ScheduleDTO>>(schedules);
                    return new Response<IEnumerable<ScheduleDTO>>(true, null, scheduleDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [HttpPut]
        [Route("api/schedule/update-bulk-schedule")]
        public Response<ScheduleDTO> UpdateBulkSchedule(ScheduleDTO scheduleDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbSchedule = entities.Schedules.Where(x => x.ID == scheduleDTO.ID).FirstOrDefault();
                    var daysDifference = (scheduleDTO.Date.Date - dbSchedule.Date.Date).TotalDays;
                    daysDifference = Convert.ToInt32(daysDifference);
                    ICollection<Schedule> childSchedules = dbSchedule.Child.Schedules;
                    if (daysDifference > 0)
                    {
                        foreach (Schedule schedule in childSchedules)
                        {
                            Dose dose = new Dose();
                            if (schedule.Date.Date == dbSchedule.Date.Date)
                            {
                                dose = schedule.Dose;

                                IEnumerable<Dose> nextDoses = entities.Doses.Where(o => o.VaccineID == dose.VaccineID).ToList();
                                foreach (Dose nextDose in nextDoses)
                                {
                                    var nextSchedule = childSchedules.Where(x => x.DoseId == nextDose.ID).FirstOrDefault();
                                    if (nextSchedule.Date.Date >= dbSchedule.Date.Date && nextSchedule.ID != dbSchedule.ID)
                                    {
                                        nextSchedule.Date = nextSchedule.Date.AddDays(daysDifference);
                                        entities.Schedules.Attach(nextSchedule);
                                        entities.Entry(nextSchedule).State = EntityState.Modified;
                                        entities.SaveChanges();
                                    }
                                }
                            }
                        }
                        dbSchedule.Date = scheduleDTO.Date.Date;
                        entities.SaveChanges();
                    }
                    return new Response<ScheduleDTO>(true, "schedule updated successfully.", null);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }


        [HttpPut]
        [Route("api/schedule/update-bulk-injection")]
        public Response<ScheduleDTO> UpdateBulkInjection(ScheduleDTO scheduleDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbSchedule = entities.Schedules.Where(x => x.ID == scheduleDTO.ID).FirstOrDefault();
                    ICollection<Schedule> childSchedules = dbSchedule.Child.Schedules;

                    foreach (var schedule in childSchedules)
                    {
                        if (schedule.Date.Date == dbSchedule.Date.Date)
                        {
                            schedule.Weight = scheduleDTO.Weight;
                            schedule.Height = scheduleDTO.Height;
                            schedule.Circle = scheduleDTO.Circle;
                            schedule.IsDone = scheduleDTO.IsDone;
                            if (scheduleDTO.ScheduleBrands.Count > 0)
                            {
                                var scheduleBrand = scheduleDTO.ScheduleBrands.Find(x => x.ScheduleId == schedule.ID);
                                if (scheduleBrand != null)
                                {
                                    schedule.BrandId = scheduleBrand.BrandId;
                                    var brandInventory = entities.BrandInventories.Where(b => b.BrandID == scheduleBrand.BrandId && b.DoctorID == scheduleDTO.DoctorID).FirstOrDefault();
                                    brandInventory.Count--;
                                }
                            }
                            //entities.Schedules.Attach(schedule);
                            //entities.Entry(schedule).State = EntityState.Modified;
                            entities.SaveChanges();

                        }
                    }
                    return new Response<ScheduleDTO>(true, "schedule updated successfully.", null);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [HttpPut]
        [Route("api/schedule/update-schedule")]
        public Response<ScheduleDTO> UpdateSchedule(ScheduleDTO scheduleDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbSchedule = entities.Schedules.Where(x => x.ID == scheduleDTO.ID).FirstOrDefault();
                    var daysDifference = (scheduleDTO.Date.Date - dbSchedule.Date.Date).TotalDays;
                    daysDifference = Convert.ToInt32(daysDifference);
                    ICollection<Schedule> childSchedules = dbSchedule.Child.Schedules;
                    if (daysDifference > 0)
                    {
                        foreach (Schedule schedule in childSchedules)
                        {
                            Dose dose = new Dose();
                            if (schedule.Date.Date == dbSchedule.Date.Date && schedule.ID == dbSchedule.ID)
                            {
                                dose = schedule.Dose;

                                IEnumerable<Dose> nextDoses = entities.Doses.Where(o => o.VaccineID == dose.VaccineID).ToList();
                                foreach (Dose nextDose in nextDoses)
                                {
                                    var nextSchedule = childSchedules.Where(x => x.DoseId == nextDose.ID).FirstOrDefault();
                                    if (nextSchedule.Date.Date >= dbSchedule.Date.Date && nextSchedule.ID != dbSchedule.ID)
                                    {
                                        nextSchedule.Date = nextSchedule.Date.AddDays(daysDifference);
                                        entities.Schedules.Attach(nextSchedule);
                                        entities.Entry(nextSchedule).State = EntityState.Modified;
                                        entities.SaveChanges();
                                    }
                                }
                            }
                        }
                        dbSchedule.Date = scheduleDTO.Date.Date;
                        entities.SaveChanges();
                    }
                    return new Response<ScheduleDTO>(true, "schedule updated successfully.", null);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [HttpPost]
        [Route("api/schedule/brandinventory-stock")]
        public Response<BrandInventoryDTO> checkBrandInventoryStock(BrandInventoryDTO brandInventoryDTo)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbBrandInventory = entities.BrandInventories.Where(b => b.BrandID == brandInventoryDTo.BrandID
                    && b.DoctorID==brandInventoryDTo.DoctorID).FirstOrDefault();

                    BrandInventoryDTO brandInventoryDTO = Mapper.Map<BrandInventoryDTO>(dbBrandInventory);
                    if (brandInventoryDTO.Count > 0)
                        return new Response<BrandInventoryDTO>(true, null, brandInventoryDTO);

                    return new Response<BrandInventoryDTO>(false, "Sorry this brand is out of stock", null);

                }
            }
            catch (Exception e)
            {
                return new Response<BrandInventoryDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [HttpPost]
        [Route("api/schedule/bulk-brand")]

        public Response<List<ScheduleDTO>> GetVaccineBrands(ScheduleDTO scheduleDto)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbSchedule = entities.Schedules.Where(x => x.Date == scheduleDto.Date && x.ChildId == scheduleDto.ChildId).ToList();

                    List<ScheduleDTO> scheduleDTOs = new List<ScheduleDTO>();
                    foreach (var schedule in dbSchedule)
                    {
                        ScheduleDTO scheduleDTO = new ScheduleDTO();
                        var dbBrands = schedule.Dose.Vaccine.Brands.ToList();
                        List<BrandDTO> brandDTOs = Mapper.Map<List<BrandDTO>>(dbBrands);
                        scheduleDTO.Dose = Mapper.Map<DoseDTO>(schedule.Dose);
                        scheduleDTO.ID = schedule.ID;
                        scheduleDTO.Brands = brandDTOs;
                        scheduleDTOs.Add(scheduleDTO);
                    }

                    return new Response<List<ScheduleDTO>>(true, null, scheduleDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<List<ScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }


    }

}
