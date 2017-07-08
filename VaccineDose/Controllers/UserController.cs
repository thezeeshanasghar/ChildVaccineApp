using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VaccineDose.Model;

namespace VaccineDose.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController
    {
        #region C R U D
        public Response<IEnumerable<UserDTO>> Get()
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbUsers = entities.Users.ToList();
                IEnumerable<UserDTO> userDTOs = Mapper.Map<IEnumerable<UserDTO>>(dbUsers);
                return new Response<IEnumerable<UserDTO>>(true, null, userDTOs);
            }
        }

        public Response<UserDTO> Get(int Id)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbUser = entities.Users.Where(c => c.ID == Id).FirstOrDefault();
                UserDTO UserDTO = Mapper.Map<UserDTO>(dbUser);
                return new Response<UserDTO>(true, null, UserDTO);
            }
        }

        public Response<UserDTO> Post(UserDTO UserDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                User dbUser = Mapper.Map<User>(UserDTO);
                entities.Users.Add(dbUser);
                entities.SaveChanges();
                UserDTO.ID = dbUser.ID;
                return new Response<UserDTO>(true, null, UserDTO);
            }
        }

        public Response<UserDTO> Put([FromBody] UserDTO UserDTO)
        {
            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbUser = entities.Users.Where(c => c.ID == UserDTO.ID).FirstOrDefault();
                dbUser = Mapper.Map<UserDTO, User>(UserDTO, dbUser);
                entities.SaveChanges();
                return new Response<UserDTO>(true, null, UserDTO);
            }
        }

        public Response<string> Delete(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbUser = entities.Users.Where(c => c.ID == Id).FirstOrDefault();
                    entities.Users.Remove(dbUser);
                    entities.SaveChanges();
                    return new Response<string>(true, null, "record deleted");
                }
            }
            catch (Exception ex)
            {
                return new Response<string>(false, ex.Message, null);
            }
        }

        #endregion
        [HttpPost]
        [Route("login")]
        public Response<UserDTO> login(UserDTO user)
        {

            using (VDConnectionString entities = new VDConnectionString())
            {
                var dbUser = entities.Users.Where(x => x.MobileNumber == user.MobileNumber).Where(x => x.Password == user.Password).FirstOrDefault();

                UserDTO UserDTO = Mapper.Map<UserDTO>(dbUser);
                return new Response<UserDTO>(true, null, UserDTO);
            }

        }

    }
}