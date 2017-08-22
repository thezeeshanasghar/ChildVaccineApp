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

        public Response<VaccineInventoryDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineInventory = entities.VaccineInventories.Where(c => c.ID == Id).FirstOrDefault();
                    VaccineInventoryDTO VaccineInventoryDTO = Mapper.Map<VaccineInventoryDTO>(dbVaccineInventory);
                    return new Response<VaccineInventoryDTO>(true, null, VaccineInventoryDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<VaccineInventoryDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        public Response<VaccineInventoryDTO> Post(VaccineInventoryDTO vaccineInventoryDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    VaccineInventory dbVaccineInventory = Mapper.Map<VaccineInventory>(vaccineInventoryDTO);
                    entities.VaccineInventories.Add(dbVaccineInventory);
                    entities.SaveChanges();
                    return new Response<VaccineInventoryDTO>(true, null, vaccineInventoryDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<VaccineInventoryDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        public Response<VaccineInventoryDTO> Put([FromBody] VaccineInventoryDTO vaccineInventoryDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbVaccineInventory= entities.VaccineInventories.Where(c => c.ID == vaccineInventoryDTO.ID).FirstOrDefault();
                    dbVaccineInventory = Mapper.Map<VaccineInventoryDTO, VaccineInventory>(vaccineInventoryDTO, dbVaccineInventory);
                    entities.SaveChanges();

                    return new Response<VaccineInventoryDTO>(true, null, vaccineInventoryDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<VaccineInventoryDTO>(false, GetMessageFromExceptionObject(e), null);
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
