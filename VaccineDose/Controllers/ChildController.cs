using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    [RoutePrefix("api/child")]
    public class ChildController : ApiController
    {
        #region C R U D
         public Response<IEnumerable<ChildDTO>> Get()
        {
            using(VDConnectionString entities = new VDConnectionString())
            {
                var dbchildren = entities.Children.ToList();
                IEnumerable<ChildDTO> childDTOs = Mapper.Map<IEnumerable<ChildDTO>>(dbchildren);
                return new Response<IEnumerable<ChildDTO>>(true, null, childDTOs);
            }
        }
       
        public Response<ChildDTO> Get(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbChild = entities.Children.Where(c => c.Id == Id).FirstOrDefault();
                ChildDTO ClinicDTO = Mapper.Map<ChildDTO>(dbChild);
                return new Response<ChildDTO>(true, null, ClinicDTO);
            }
        }
        
        public Response<ChildDTO> Post(ChildDTO childDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                Child clinicDb = Mapper.Map<Child>(childDTO);
                entities.Children.Add(clinicDb);
                entities.SaveChanges();
                childDTO.Id = clinicDb.Id;
                return new Response<ChildDTO>(true, null, childDTO);
            }
        }
        
        public Response<ChildDTO> Put([FromBody] ChildDTO childDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbChild = entities.Children.Where(c => c.Id == childDTO.Id).FirstOrDefault();
                dbChild = Mapper.Map<ChildDTO, Child>(childDTO, dbChild);
                entities.SaveChanges();
                return new Response<ChildDTO>(true, null, childDTO);
            }
        }
            
        public Response<string> Delete(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbChild = entities.Children.Where(c => c.Id == Id).FirstOrDefault();
                entities.Children.Remove(dbChild);
                entities.SaveChanges();
                return new Response<string>(true, null, "record deleted");
            }
        }

        #endregion
    }
}
