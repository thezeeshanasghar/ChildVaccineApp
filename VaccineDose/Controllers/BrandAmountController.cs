using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VaccineDose.Model;
namespace VaccineDose.Controllers
{
    public class BrandAmountController : BaseController
    {
        #region C R U D

        public Response<List<BrandAmountDTO>> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    List<BrandAmount> brandAmountDBs = entities.BrandAmounts.Include("Brand").Include("Doctor").Where(x => x.DoctorID == Id).ToList();
                    if (brandAmountDBs == null || brandAmountDBs.Count() == 0)
                        return new Response<List<BrandAmountDTO>>(false, "Brand not found", null);
                    List<BrandAmountDTO> brandAmountDTOs = Mapper.Map<List<BrandAmountDTO>>(brandAmountDBs);
                    foreach (BrandAmountDTO baDTO in brandAmountDTOs)
                        baDTO.VaccineName = entities.Brands.Where(x => x.ID == baDTO.BrandID).First().Vaccine.Name;
                    return new Response<List<BrandAmountDTO>>(true, null, brandAmountDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<List<BrandAmountDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<IEnumerable<BrandAmountDTO>> Post(IEnumerable<BrandAmountDTO> brandAmountDTOs)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    foreach (var brandAmountDTO in brandAmountDTOs)
                    {
                        BrandAmount brandAmountDB = Mapper.Map<BrandAmount>(brandAmountDTO);
                        entities.BrandAmounts.Add(brandAmountDB);
                        entities.SaveChanges();
                        brandAmountDTO.ID = brandAmountDB.ID;
                    }
                    return new Response<IEnumerable<BrandAmountDTO>>(true, null, brandAmountDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<BrandAmountDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<List<BrandAmountDTO>> Put([FromBody] List<BrandAmountDTO> brandAmountDTOs)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    foreach (var brandAmountDTO in brandAmountDTOs)
                    {
                        var brandAmoundDB = entities.BrandAmounts.Where(b => b.ID == brandAmountDTO.ID).FirstOrDefault();
                        brandAmoundDB.Amount = brandAmountDTO.Amount;
                        entities.SaveChanges();
                    }
                    return new Response<List<BrandAmountDTO>>(true, null, brandAmountDTOs);
                }

            }
            catch (Exception e)
            {
                return new Response<List<BrandAmountDTO>>(false, GetMessageFromExceptionObject(e), null);
            }
        }
        public Response<string> Delete(int Id)
        {
            try
            {
                using(VDConnectionString entities = new VDConnectionString())
                {
                    var dbBrandAmount = entities.BrandAmounts.Where(b => b.ID == Id).FirstOrDefault();
                    entities.BrandAmounts.Remove(dbBrandAmount);
                    entities.SaveChanges();
                    return new Response<string>(true, null, "record deleted");
                }

            }
            catch(Exception ex)
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
