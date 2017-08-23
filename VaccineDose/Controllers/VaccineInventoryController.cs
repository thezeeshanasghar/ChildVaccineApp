using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class VaccineInventoryController : BaseController
    {
        #region C R U D

        public Response<List<VaccineInventoryDTO>> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {

                    List<VaccineInventory> vaccineInventoryDBs = entities.VaccineInventories.Include("Vaccine").Include("Doctor").Where(x => x.DoctorID == Id).ToList();
                    if (vaccineInventoryDBs == null || vaccineInventoryDBs.Count() == 0)
                        return new Response<List<VaccineInventoryDTO>>(false, "inventory not found", null);

                    List<VaccineInventoryDTO> VaccineInventoryDTOs = Mapper.Map<List<VaccineInventoryDTO>>(vaccineInventoryDBs);
                    return new Response<List<VaccineInventoryDTO>>(true, null, VaccineInventoryDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<List<VaccineInventoryDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<IEnumerable<VaccineInventoryDTO>> Post(IEnumerable<VaccineInventoryDTO> vaccineInventoryDTOs)
        {
            try
            {

                using (VDConnectionString entities = new VDConnectionString())
                {
                    foreach (var vaccineInventoryDTO in vaccineInventoryDTOs)
                    {
                        VaccineInventory vaccineInventoryDB = Mapper.Map<VaccineInventory>(vaccineInventoryDTO);
                        entities.VaccineInventories.Add(vaccineInventoryDB);
                        entities.SaveChanges();
                        vaccineInventoryDTO.ID = vaccineInventoryDB.ID;
                    }

                    return new Response<IEnumerable<VaccineInventoryDTO>>(true, null, vaccineInventoryDTOs);

                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<VaccineInventoryDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }
        public Response<List<VaccineInventoryDTO>> Put([FromBody] List<VaccineInventoryDTO> vaccineInventoryDTOs)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    foreach(var vaccineInventoryDTO in vaccineInventoryDTOs)
                    {
                        var vaccineInventoryDB = entities.VaccineInventories.Where(c => c.ID == vaccineInventoryDTO.ID).FirstOrDefault();
                        vaccineInventoryDB.Count = vaccineInventoryDTO.Count;
                        entities.SaveChanges();
                    }
                    

                    return new Response<List<VaccineInventoryDTO>>(true, null, vaccineInventoryDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<List<VaccineInventoryDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineInventory = entities.VaccineInventories.Where(c => c.ID == Id).FirstOrDefault();
                    entities.VaccineInventories.Remove(dbVaccineInventory);
                    entities.SaveChanges();
                    return new Response<string>(true, null, "record deleted");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                    return new Response<string>(false, "The DELETE statement conflicted with the REFERENCE constraint", null);
                else
                    return new Response<string>(false, GetMessageFromExceptionObject(ex), null);
            }
        }

        #endregion

    }
}
