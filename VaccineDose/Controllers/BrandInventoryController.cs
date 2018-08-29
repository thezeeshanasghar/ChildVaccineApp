using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    public class BrandInventoryController : BaseController
    {
        #region C R U D

        public Response<List<BrandInventoryDTO>> Get(int Id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {

                    List<BrandInventory> vaccineInventoryDBs = entities.BrandInventories.Include("Brand").Include("Doctor").Where(x => x.DoctorID == Id).ToList();
                    if (vaccineInventoryDBs == null || vaccineInventoryDBs.Count() == 0)
                        return new Response<List<BrandInventoryDTO>>(false, "brand inventory not found", null);

                    List<BrandInventoryDTO> VaccineInventoryDTOs = Mapper.Map<List<BrandInventoryDTO>>(vaccineInventoryDBs);
                    foreach (BrandInventoryDTO brandInventoryDTO in VaccineInventoryDTOs)
                        brandInventoryDTO.VaccineName = entities.Brands.Where(x => x.ID == brandInventoryDTO.BrandID).FirstOrDefault().Vaccine.Name;
                    return new Response<List<BrandInventoryDTO>>(true, null, VaccineInventoryDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<List<BrandInventoryDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<IEnumerable<BrandInventoryDTO>> Post(IEnumerable<BrandInventoryDTO> vaccineInventoryDTOs)
        {
            try
            {

                using (VDEntities entities = new VDEntities())
                {
                    foreach (var vaccineInventoryDTO in vaccineInventoryDTOs)
                    {
                        BrandInventory vaccineInventoryDB = Mapper.Map<BrandInventory>(vaccineInventoryDTO);
                        entities.BrandInventories.Add(vaccineInventoryDB);
                        entities.SaveChanges();
                        vaccineInventoryDTO.ID = vaccineInventoryDB.ID;
                    }

                    return new Response<IEnumerable<BrandInventoryDTO>>(true, null, vaccineInventoryDTOs);

                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<BrandInventoryDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }
        public Response<List<BrandInventoryDTO>> Put([FromBody] List<BrandInventoryDTO> vaccineInventoryDTOs)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    foreach (var vaccineInventoryDTO in vaccineInventoryDTOs)
                    {
                        var vaccineInventoryDB = entities.BrandInventories.Where(c => c.ID == vaccineInventoryDTO.ID).FirstOrDefault();
                        vaccineInventoryDB.Count = vaccineInventoryDTO.Count;
                        entities.SaveChanges();
                    }


                    return new Response<List<BrandInventoryDTO>>(true, null, vaccineInventoryDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<List<BrandInventoryDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbVaccineInventory = entities.BrandInventories.Where(c => c.ID == Id).FirstOrDefault();
                    entities.BrandInventories.Remove(dbVaccineInventory);
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
