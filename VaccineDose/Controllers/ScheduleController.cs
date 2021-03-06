﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VaccineDose.App_Code;
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
                using (VDEntities entities = new VDEntities())
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

        #region Rescheduling

        [HttpPut]
        [Route("api/schedule/BulkReschedule")]
        public Response<ScheduleDTO> BulkReschedule(ScheduleDTO scheduleDTO, [FromUri] bool ignoreMaxAgeRule = false, [FromUri] bool ignoreMinAgeFromDOB = false, [FromUri] bool ignoreMinGapFromPreviousDose = false)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbSchedule = entities.Schedules.Where(x => x.ID == scheduleDTO.ID).FirstOrDefault();

                    var dbSchedules = entities.Schedules.Where(x => x.Date == dbSchedule.Date 
                                                                && x.ChildId == dbSchedule.ChildId
                                                                && x.IsDone==false
                                                                ).ToList();

                    foreach (var schedule in dbSchedules)
                        ChangeDueDatesOfSchedule(scheduleDTO, entities, schedule, "bulk", ignoreMaxAgeRule, ignoreMinAgeFromDOB, ignoreMinGapFromPreviousDose);

                    return new Response<ScheduleDTO>(true, "schedule updated successfully.", null);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        private void ChangeDueDatesOfSchedule(ScheduleDTO scheduleDTO, VDEntities entities, Schedule dbSchedule, string mode, bool ignoreMaxAgeRule, bool ignoreMinAgeFromDOB, bool ignoreMinGapFromPreviousDose)
        {
            var daysDifference = Convert.ToInt32((scheduleDTO.Date.Date - dbSchedule.Date.Date).TotalDays);

            var AllDoses = dbSchedule.Dose.Vaccine.Doses;
            // FOR BCG Only or those vaccines who have only 1 dose 
            if (AllDoses.Count == 1)
            {
                // check for reschedule backward from DateOfBirth
                if (scheduleDTO.Date < dbSchedule.Child.DOB)
                    throw new Exception("Cannot reschedule to your selected date: " +
                                Convert.ToDateTime(scheduleDTO.Date.Date).ToString("dd-MM-yyyy") + " because it is less than date of birth of child.");
                Dose d = AllDoses.ElementAt<Dose>(0);
                var TargetSchedule = entities.Schedules.Where(x => x.ChildId == dbSchedule.ChildId && x.DoseId == d.ID).FirstOrDefault();
                if (TargetSchedule != null)
                {
                    if (daysDifference > d.MaxAge && !ignoreMaxAgeRule)
                        if (mode.Equals("bulk"))
                            throw new Exception("Cannot reschedule to your selected date: " +
                               Convert.ToDateTime(scheduleDTO.Date.Date).ToString("dd-MM-yyyy") + " because is is greater than the Max Age of dose. " +
                               "<button onclick=BulkReschedule({ID:" + scheduleDTO.ID + ",Date:'" + scheduleDTO.Date.ToString("dd-MM-yyyy") + "'},true,false,false)> Ignore Rule</a>");
                        else
                            throw new Exception("Cannot reschedule to your selected date: " +
                           Convert.ToDateTime(scheduleDTO.Date.Date).ToString("dd-MM-yyyy") + " because is is greater than the Max Age of dose. " +
                           "<button onclick=Reschedule({ID:" + scheduleDTO.ID + ",Date:'" + scheduleDTO.Date.ToString("dd-MM-yyyy") + "'},true,false,false)> Ignore Rule</a>");

                    TargetSchedule.Date = calculateDate(TargetSchedule.Date, daysDifference);// TargetSchedule.Date.AddDays(daysDifference);

                }
                }
            else
            {
                // forward rescheduling
                if (daysDifference > 0)
                {
                    AllDoses = AllDoses.Where(x => x.DoseOrder >= dbSchedule.Dose.DoseOrder).ToList();
                    DateTime previousDate = DateTime.UtcNow.AddHours(5);
                    //foreach (var d in AllDoses)
                    for (int i = 0; i < AllDoses.Count; i++)
                    {
                        var d = AllDoses.ElementAt(i);
                        int? MinGap = d.MinGap;
                        var TargetSchedule = entities.Schedules.Where(x => x.ChildId == dbSchedule.ChildId && x.DoseId == d.ID).FirstOrDefault();
                        // if MinGap is this dose < MinAge of Previouse Dose; then dont reschedule
                        // stop updating date of a dose if minimum gap is valid
                        if (TargetSchedule != null)
                        {
                            if (i != 0)
                            {
                                var doseDaysDifference = Convert.ToInt32((TargetSchedule.Date.Date - previousDate.Date).TotalDays);
                                if (doseDaysDifference <= MinGap && TargetSchedule != null)
                                    TargetSchedule.Date = calculateDate(TargetSchedule.Date, daysDifference); // TargetSchedule.Date.AddDays(daysDifference);
                            }
                            else
                            {
                                // check for MaxAge of any Dose
                                if (daysDifference > d.MaxAge && !ignoreMaxAgeRule)
                                    if (mode.Equals("bulk"))
                                        throw new Exception("Cannot reschedule to your selected date: " +
                                       Convert.ToDateTime(scheduleDTO.Date.Date).ToString("dd-MM-yyyy") + " because is is greater than the Max Age of dose. " +
                                       "<button onclick=BulkReschedule({ID:" + scheduleDTO.ID + ",Date:'" + scheduleDTO.Date.ToString("dd-MM-yyyy") + "'},true,false,false)> Ignore Rule</a>");
                                    else
                                        throw new Exception("Cannot reschedule to your selected date: " +
                                       Convert.ToDateTime(scheduleDTO.Date.Date).ToString("dd-MM-yyyy") + " because is is greater than the Max Age of dose. " +
                                       "<button onclick=Reschedule({ID:" + scheduleDTO.ID + ",Date:'" + scheduleDTO.Date.ToString("dd-MM-yyyy") + "'},true,false,false)> Ignore Rule</a>");

                                TargetSchedule.Date = calculateDate(TargetSchedule.Date, daysDifference); //TargetSchedule.Date.AddDays(daysDifference);
                                previousDate = TargetSchedule.Date;

                            }
                        }
                    }
                }
                // backward rescheduling
                else
                {
                    // find that dose and its previous dose
                    AllDoses = AllDoses.Where(x => x.DoseOrder <= dbSchedule.Dose.DoseOrder).OrderBy(x => x.DoseOrder).ToList();
                    // if we rescdule the first dose of any vaccine
                    if (AllDoses.Count == 1)
                    {
                        Dose d = AllDoses.ElementAt<Dose>(0);
                        var FirstDoseSchedule = entities.Schedules.Where(x => x.ChildId == dbSchedule.ChildId && x.DoseId == d.ID).FirstOrDefault();
                        if (FirstDoseSchedule != null)
                        { 
                        int diff = Convert.ToInt32((scheduleDTO.Date.Date - FirstDoseSchedule.Child.DOB).TotalDays);
                        if (diff < 0)
                            throw new Exception("Cannot reschedule to your selected date: " +
                                Convert.ToDateTime(scheduleDTO.Date.Date).ToString("dd-MM-yyyy") + " because it is less than date of birth of child.");
                        else if (diff < d.MinAge && !ignoreMinAgeFromDOB)
                            if (mode.Equals("bulk"))
                                throw new Exception("Cannot reschedule to your selected date: " +
                                Convert.ToDateTime(scheduleDTO.Date.Date).ToString("dd-MM-yyyy") + " because Minimum Age of this vaccine from date of birth should be " + d.MinAge + " days." +
                                "<button onclick=BulkReschedule({ID:" + scheduleDTO.ID + ",Date:'" + scheduleDTO.Date.ToString("dd-MM-yyyy") + "'},false,true,false)> Ignore Rule</a>");
                            else
                                throw new Exception("Cannot reschedule to your selected date: " +
                               Convert.ToDateTime(scheduleDTO.Date.Date).ToString("dd-MM-yyyy") + " because Minimum Age of this vaccine from date of birth should be " + d.MinAge + " days." +
                               "<button onclick=Reschedule({ID:" + scheduleDTO.ID + ",Date:'" + scheduleDTO.Date.ToString("dd-MM-yyyy") + "'},false,true,false)> Ignore Rule</a>");
                        else
                            FirstDoseSchedule.Date = calculateDate(FirstDoseSchedule.Date, daysDifference);
                        }
                       
                    }
                    // if we rescdule other than first dose of any vaccine
                    else
                    {
                        var lastDose = AllDoses.Last<Dose>();
                        var secondLastDose = AllDoses.ElementAt(AllDoses.Count - 2);

                        var TargetSchedule = entities.Schedules.Where(x => x.ChildId == dbSchedule.ChildId && x.DoseId == lastDose.ID).FirstOrDefault();
                        var TargetSchedulePrevious = entities.Schedules.Where(x => x.ChildId == dbSchedule.ChildId && x.DoseId == secondLastDose.ID).FirstOrDefault();

                        if (TargetSchedule != null && TargetSchedulePrevious !=null )
                        {
                               long doseDaysDifference = 0;

                        if (TargetSchedulePrevious.IsDone && TargetSchedulePrevious.GivenDate.HasValue)
                            doseDaysDifference = Convert.ToInt32((scheduleDTO.Date.Date - TargetSchedulePrevious.GivenDate.Value).TotalDays);
                        else
                            doseDaysDifference = Convert.ToInt32((scheduleDTO.Date.Date - TargetSchedulePrevious.Date).TotalDays);

                        if (doseDaysDifference < lastDose.MinGap && !ignoreMinGapFromPreviousDose )
                            if (mode.Equals("bulk"))
                                throw new Exception("Cannot reschedule to your selected date: " +
                                Convert.ToDateTime(scheduleDTO.Date.Date).ToString("dd-MM-yyyy") + " because Minimum Gap from previous dose of this vaccine should be " + lastDose.MinGap + " days." +
                                "<button onclick=BulkReschedule({ID:" + scheduleDTO.ID + ",Date:'" + scheduleDTO.Date.ToString("dd-MM-yyyy") + "'},false,false,true)> Ignore Rule</a>");
                            else
                                throw new Exception("Cannot reschedule to your selected date: " +
                                Convert.ToDateTime(scheduleDTO.Date.Date).ToString("dd-MM-yyyy") + " because Minimum Gap from previous dose of this vaccine should be " + lastDose.MinGap + " days." +
                                "<button onclick=Reschedule({ID:" + scheduleDTO.ID + ",Date:'" + scheduleDTO.Date.ToString("dd-MM-yyyy") + "'},false,false,true)> Ignore Rule</a>");
                        TargetSchedule.Date = calculateDate(TargetSchedule.Date, daysDifference);
                        }
                         
                    }
                }
            }
            entities.SaveChanges();
        }

        [HttpPut]
        [Route("api/schedule/Reschedule")]
        public Response<ScheduleDTO> Reschedule(ScheduleDTO scheduleDTO, [FromUri] bool ignoreMaxAgeRule = false, [FromUri] bool ignoreMinAgeFromDOB = false, [FromUri] bool ignoreMinGapFromPreviousDose = false)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbSchedule = entities.Schedules.Where(x => x.ID == scheduleDTO.ID).FirstOrDefault();
                    ChangeDueDatesOfSchedule(scheduleDTO, entities, dbSchedule, "single", ignoreMaxAgeRule, ignoreMinAgeFromDOB, ignoreMinGapFromPreviousDose);

                    return new Response<ScheduleDTO>(true, "schedule updated successfully.", null);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        #endregion

        #region Injection

        [HttpPut]
        [Route("api/schedule/update-bulk-injection")]
        public Response<ScheduleDTO> UpdateBulkInjection(ScheduleDTO scheduleDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbSchedule = entities.Schedules.Where(x => x.ID == scheduleDTO.ID).FirstOrDefault();
                    var dbChildSchedules = dbSchedule.Child.Schedules.Where(x => x.Date == dbSchedule.Date).ToList();

                    foreach (var schedule in dbChildSchedules)
                    {
                        //if (!schedule.IsDone)
                        //{
                        schedule.Weight = (scheduleDTO.Weight > 0) ? scheduleDTO.Weight : schedule.Weight;
                        schedule.Height = (scheduleDTO.Height > 0) ? scheduleDTO.Height : schedule.Height;
                        schedule.Circle = (scheduleDTO.Circle > 0) ? scheduleDTO.Circle : schedule.Circle;
                        schedule.IsDone = scheduleDTO.IsDone;
                        schedule.GivenDate = scheduleDTO.GivenDate;

                        if (scheduleDTO.ScheduleBrands.Count > 0)
                        {
                            var scheduleBrand = scheduleDTO.ScheduleBrands.Find(x => x.ScheduleId == schedule.ID);
                            if (scheduleBrand != null)
                            {
                                schedule.BrandId = scheduleBrand.BrandId;
                                if (scheduleDTO.GivenDate.Date == DateTime.UtcNow.AddHours(5).Date)
                                {
                                    var brandInventory = entities.BrandInventories.Where(b => b.BrandID == scheduleBrand.BrandId && b.DoctorID == scheduleDTO.DoctorID).FirstOrDefault();
                                    brandInventory.Count--;
                                }
                            }
                        }
                        ChangeDueDatesOfInjectedSchedule(scheduleDTO, entities, schedule);
                    }
                    entities.SaveChanges();
                    return new Response<ScheduleDTO>(true, "schedule updated successfully.", null);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [HttpPut]
        [Route("api/schedule/child-schedule")]
        public Response<ScheduleDTO> Update(ScheduleDTO scheduleDTO)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbSchedule = entities.Schedules.Where(c => c.ID == scheduleDTO.ID).FirstOrDefault();
                    var dbBrandInventory = entities.BrandInventories.Where(b => b.BrandID == scheduleDTO.BrandId
                                            && b.DoctorID == scheduleDTO.DoctorID).FirstOrDefault();
                    if (dbBrandInventory != null && dbBrandInventory.Count > 0)
                        if (scheduleDTO.GivenDate.Date == DateTime.UtcNow.AddHours(5).Date)
                            dbBrandInventory.Count--;
                    dbSchedule.BrandId = scheduleDTO.BrandId;
                    dbSchedule.Weight = scheduleDTO.Weight;
                    dbSchedule.Height = scheduleDTO.Height;
                    dbSchedule.Circle = scheduleDTO.Circle;
                    dbSchedule.IsDone = scheduleDTO.IsDone;
                    dbSchedule.GivenDate = scheduleDTO.GivenDate;

                    ChangeDueDatesOfInjectedSchedule(scheduleDTO, entities, dbSchedule);
                    entities.SaveChanges();
                    return new Response<ScheduleDTO>(true, "schedule updated successfully.", null);
                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        private void ChangeDueDatesOfInjectedSchedule(ScheduleDTO scheduleDTO, VDEntities entities, Schedule dbSchedule)
        {
            var daysDifference = Convert.ToInt32((scheduleDTO.GivenDate.Date - dbSchedule.Date.Date).TotalDays);

            var AllDoses = dbSchedule.Dose.Vaccine.Doses;
            AllDoses = AllDoses.Where(x => x.DoseOrder > dbSchedule.Dose.DoseOrder).ToList();
            foreach (var d in AllDoses)
            {
                var TargetSchedule = entities.Schedules.Where(x => x.ChildId == dbSchedule.ChildId && x.DoseId == d.ID).FirstOrDefault();
                if (TargetSchedule != null)
                {
                    TargetSchedule.Date = calculateDate(TargetSchedule.Date, daysDifference); //TargetSchedule.Date.AddDays(daysDifference);
                }
            }

        }


        #endregion

        #region ALERT PAGE CALL

        [HttpGet]
        [Route("api/schedule/alert/{GapDays}/{OnlineClinicID}")]
        public Response<IEnumerable<ScheduleDTO>> GetAlert(int GapDays, int OnlineClinicID)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    List<Schedule> schedules = GetAlertData(GapDays, OnlineClinicID, entities);
                    IEnumerable<ScheduleDTO> scheduleDTO = Mapper.Map<IEnumerable<ScheduleDTO>>(schedules);
                    return new Response<IEnumerable<ScheduleDTO>>(true, null, scheduleDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<ScheduleDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        private static List<Schedule> GetAlertData(int GapDays, int OnlineClinicID, VDEntities entities)
        {
            List<Schedule> schedules = new List<Schedule>();
            var doctor = entities.Clinics.Where(x => x.ID == OnlineClinicID).First<Clinic>().Doctor;
            int[] ClinicIDs = doctor.Clinics.Select(x => x.ID).ToArray<int>();
            DateTime CurrentPakDateTime = DateTime.UtcNow.AddHours(5);
            DateTime AddedDateTime = CurrentPakDateTime.AddDays(GapDays);
            if (GapDays == 0)
            {
                schedules = entities.Schedules.Include("Child")
                    .Where(c => ClinicIDs.Contains(c.Child.ClinicID))
                    .Where(c => c.Date == CurrentPakDateTime.Date)
                    .Where(c => c.IsDone == false)
                    .OrderBy(x => x.Child.ID).ThenBy(x => x.Date).ToList<Schedule>();
                var sc = entities.Schedules.Include("Child")
                    .Where(c => ClinicIDs.Contains(c.Child.ClinicID))
                    .Where(c => c.Child.PreferredDayOfReminder != 0)
                    .Where(c => c.Date == DbFunctions.AddDays(CurrentPakDateTime.Date, c.Child.PreferredDayOfReminder))
                    .Where(c => c.IsDone == false)
                    .OrderBy(x => x.Child.ID).ThenBy(x => x.Date).ToList<Schedule>();
                schedules.AddRange(sc);
            }
            else if (GapDays > 0)
            {
                AddedDateTime = AddedDateTime.AddDays(1);
                schedules = entities.Schedules.Include("Child")
                    .Where(c => ClinicIDs.Contains(c.Child.ClinicID))
                    .Where(c => c.Date > CurrentPakDateTime.Date && c.Date <= AddedDateTime)
                    .Where(c => c.IsDone == false)
                    .OrderBy(x => x.Child.ID).ThenBy(x => x.Date)
                    .ToList<Schedule>();
                
            }
            else if (GapDays < 0)
            {
                schedules = entities.Schedules.Include("Child")
                    .Where(c => ClinicIDs.Contains(c.Child.ClinicID))
                    .Where(c => c.Date < CurrentPakDateTime.Date && c.Date >= AddedDateTime)
                    .Where(c => c.IsDone == false)
                    .OrderBy(x => x.Child.ID).ThenBy(x => x.Date)
                    .ToList<Schedule>();
            }
            schedules = removeDuplicateRecords(schedules);
            return schedules;
        }

        private static List<Schedule> removeDuplicateRecords(List<Schedule> schedules)
        {
            List<Schedule> uniqueSchedule = new List<Schedule>();
            long childId = 0;
            foreach(Schedule s in schedules)
            {
                if(childId != s.ChildId)
                    uniqueSchedule.Add(s); 
                childId = s.ChildId;
            }
            return uniqueSchedule;
        }

        [HttpGet]
        [Route("api/schedule/sms-alert/{GapDays}/{OnlineClinicId}")]
        public Response<IEnumerable<ScheduleDTO>> SendSMSAlertToParent(int GapDays, int OnlineClinicId)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    List<Schedule> Schedules = GetAlertData(GapDays, OnlineClinicId, entities);
                    var dbChildren = Schedules.Select(x => x.Child).Distinct().ToList();
                    foreach (var child in dbChildren)
                    {
                        var dbSchedules = Schedules.Where(x => x.ChildId == child.ID).ToList();
                        var doseName = "";
                        DateTime scheduleDate = new DateTime();
                        foreach (var schedule in dbSchedules)
                        {
                            doseName += schedule.Dose.Name + ", ";
                            scheduleDate = schedule.Date;
                        }
                        UserSMS.ParentSMSAlert(doseName, scheduleDate, child);
                    }

                    List<ScheduleDTO> scheduleDtos = Mapper.Map<List<ScheduleDTO>>(Schedules);
                    return new Response<IEnumerable<ScheduleDTO>>(true, null, scheduleDtos);
                }

            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ScheduleDTO>>(false, GetMessageFromExceptionObject(ex), null);
            }

        }


        [HttpGet]
        [Route("api/schedule/individual-sms-alert/{GapDays}/{childId}")]
        public Response<IEnumerable<ScheduleDTO>> SendSMSAlertToOneChild(int GapDays, int childId)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    IEnumerable<Schedule> Schedules = new List<Schedule>();
                    DateTime AddedDateTime = DateTime.UtcNow.AddHours(5).AddDays(GapDays);
                    DateTime pakistanDate = DateTime.UtcNow.AddHours(5).Date;
                    if (GapDays == 0)
                    {
                        Schedules = entities.Schedules.Include("Child").Include("Dose")
                            .Where(sc => sc.ChildId == childId)
                            .Where(sc => sc.Date == pakistanDate)
                            .Where(sc => sc.IsDone == false)
                            .OrderBy(x => x.Child.ID).ThenBy(y => y.Date).ToList<Schedule>();
                    }
                    if (GapDays > 0)
                    {
                        Schedules = entities.Schedules.Include("Child").Include("Dose")
                            .Where(sc => sc.ChildId == childId)
                            .Where(sc => sc.IsDone == false)
                            .Where(sc => sc.Date >= pakistanDate && sc.Date <= AddedDateTime)
                            .OrderBy(x => x.Child.ID).ThenBy(y => y.Date).ToList<Schedule>();
                    }
                    if (GapDays < 0)
                    {
                        Schedules = entities.Schedules.Include("Child").Include("Dose")
                           .Where(sc => sc.ChildId == childId)
                           .Where(sc => sc.IsDone == false)
                           .Where(sc => sc.Date <= pakistanDate && sc.Date >= AddedDateTime)
                           .OrderBy(x => x.Child.ID).ThenBy(y => y.Date).ToList<Schedule>();
                    }

                    var doseName = "";
                    DateTime scheduleDate = new DateTime();
                    var dbChild = entities.Children.Where(x => x.ID == childId).FirstOrDefault();
                    foreach (var schedule in Schedules)
                    {
                        doseName += schedule.Dose.Name.Trim() + ", ";
                        scheduleDate = schedule.Date;
                    }
                    UserSMS.ParentSMSAlert(doseName, scheduleDate, dbChild);

                    List<ScheduleDTO> scheduleDtos = Mapper.Map<List<ScheduleDTO>>(Schedules);
                    return new Response<IEnumerable<ScheduleDTO>>(true, null, scheduleDtos);
                }

            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ScheduleDTO>>(false, GetMessageFromExceptionObject(ex), null);
            }

        }

        #endregion

        #region Vacation
        [HttpPost]
        [Route("api/schedule/add-vacation")]
        public Response<ScheduleDTO> AddVacations(ScheduleDTO obj)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    foreach (var clinic in obj.Clinics)
                    {
                        var dbSchedules = entities.Schedules.Where(x => x.Child.ClinicID == clinic.ID
                        && x.Date >= obj.FromDate && x.Date <= obj.ToDate).ToList();

                        foreach (Schedule schedule in dbSchedules)
                        {
                            schedule.Date = obj.ToDate.AddDays(1);
                            entities.SaveChanges();
                        }
                    }

                    return new Response<ScheduleDTO>(true, "Vacations are considered and appointments are moved to " +
                        obj.ToDate.AddDays(1).ToString("dd-MM-yyy") + " date.", null);

                }
            }
            catch (Exception e)
            {
                return new Response<ScheduleDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        #endregion

        [HttpPost]
        [Route("api/schedule/brandinventory-stock")]
        public Response<BrandInventoryDTO> checkBrandInventoryStock(BrandInventoryDTO brandInventoryDTo)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbBrandInventory = entities.BrandInventories.Where(b => b.BrandID == brandInventoryDTo.BrandID
                    && b.DoctorID == brandInventoryDTo.DoctorID).FirstOrDefault();

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
                using (VDEntities entities = new VDEntities())
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
                        scheduleDTO.Date = schedule.Date;
                        scheduleDTO.IsDone = schedule.IsDone;
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
