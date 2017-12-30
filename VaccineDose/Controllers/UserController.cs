using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using VaccineDose.App_Code;

namespace VaccineDose.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        #region C R U D
        public Response<IEnumerable<UserDTO>> Get()
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbUsers = entities.Users.ToList();
                    IEnumerable<UserDTO> userDTOs = Mapper.Map<IEnumerable<UserDTO>>(dbUsers);
                    return new Response<IEnumerable<UserDTO>>(true, null, userDTOs);
                }
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<UserDTO>>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<UserDTO> Get(int Id)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbUser = entities.Users.Where(c => c.ID == Id).FirstOrDefault();
                    UserDTO UserDTO = Mapper.Map<UserDTO>(dbUser);
                    return new Response<UserDTO>(true, null, UserDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<UserDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<UserDTO> Post(UserDTO UserDTO)
        {
            try
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
            catch (Exception e)
            {
                return new Response<UserDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        public Response<UserDTO> Put([FromBody] UserDTO UserDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbUser = entities.Users.Where(c => c.ID == UserDTO.ID).FirstOrDefault();
                    dbUser = Mapper.Map<UserDTO, User>(UserDTO, dbUser);
                    entities.SaveChanges();
                    return new Response<UserDTO>(true, null, UserDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<UserDTO>(false, GetMessageFromExceptionObject(e), null);

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
        public Response<UserDTO> login(UserDTO userDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbUser = entities.Users.FirstOrDefault(x => 
                                                                x.MobileNumber == userDTO.MobileNumber && 
                                                                x.Password == userDTO.Password && 
                                                                x.CountryCode==userDTO.CountryCode &&
                                                                x.UserType == userDTO.UserType);
                    if (dbUser == null)
                        return new Response<UserDTO>(false, "Invalid Mobile Number and Password.", null);

                    userDTO.ID = dbUser.ID;

                    if (userDTO.UserType.Equals("SUPERADMIN"))
                        return new Response<UserDTO>(true, null, userDTO);
                    else if (userDTO.UserType.Equals("DOCTOR"))
                    {

                        var doctorDb = entities.Doctors.Where(x => x.UserID == dbUser.ID).FirstOrDefault();
                        if (doctorDb == null)
                            return new Response<UserDTO>(false, "Doctor not found.", null);
                        if (!doctorDb.IsApproved)
                            return new Response<UserDTO>(false, "You are not approved. Contact admin for approval at 923335196658", null);

                        userDTO.DoctorID = doctorDb.ID;
                    }
                    else if (userDTO.UserType.Equals("PARENT"))
                    {
                        var childDB = entities.Children.Where(x => x.UserID == dbUser.ID).FirstOrDefault();
                        if (childDB == null)
                            return new Response<UserDTO>(false, "Child not found.", null);
                        else
                            userDTO.ChildID = childDB.ID;
                    }

                    return new Response<UserDTO>(true, null, userDTO);
                }
            }
            catch (Exception e)
            {
                return new Response<UserDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

        [HttpGet]
        [Route("checkUniqueMobile")]
        public HttpResponseMessage CheckUniqueMobile(string MobileNumber)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    User userDB = entities.Users.Where(x => x.MobileNumber == MobileNumber).FirstOrDefault();
                    if (userDB == null)
                        return Request.CreateResponse((HttpStatusCode)200);
                    else
                    {
                        //return BadRequest("Mobile number already exists");
                        //return Content((HttpStatusCode)400, "Mobile number already exists");
                        //return new System.Web.Http.Results.ResponseMessageResult(
                        //    Request.CreateErrorResponse((HttpStatusCode)422, new HttpError("Mobile number already exists")));
                        int HTTPResponse = 400;
                        var response = Request.CreateResponse((HttpStatusCode)HTTPResponse);
                        response.ReasonPhrase = "Mobile Number already exists";
                        return response;
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPost]
        [Route("change-password")]
        public Response<UserDTO> ChangePassword(ChangePasswordRequestDTO user)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    User userDB = entities.Users.Where(x => x.ID == user.UserID).FirstOrDefault();
                    if (userDB == null)
                        return new Response<UserDTO>(false, "User not found.", null);
                    if(!userDB.Password.Equals(user.OldPassword))
                        return new Response<UserDTO>(false, "Old password doesn't match.", null);
                    else
                    {
                        userDB.Password = user.NewPassword;
                        entities.SaveChanges();
                        return new Response<UserDTO>(true, "Password change successfully.", null);
                    }
                }
            }
            catch (Exception e)
            {
                return new Response<UserDTO>(false, GetMessageFromExceptionObject(e), null);
            }
        }

        [HttpPost]
        [Route("forgot-password")]
        public Response<UserDTO> ForgotPassword(UserDTO userDTO)
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbUser = entities.Users.Where(x => x.MobileNumber == userDTO.MobileNumber)
                        .Where(x => x.CountryCode == userDTO.CountryCode)
                        .Where(ut =>ut.UserType==userDTO.UserType).FirstOrDefault();

                    if (dbUser == null)
                        return new Response<UserDTO>(false, "Invalid Mobile Number", null);

                    if (dbUser.UserType.Equals("DOCTOR"))
                    {
                        var doctorDb = entities.Doctors.Where(x => x.UserID == dbUser.ID).FirstOrDefault();
                        if (doctorDb == null)
                        {
                            return new Response<UserDTO>(false, "Invalid Mobile Number", null);

                        }
                        else
                        {
                            UserEmail.DoctorForgotPassword(doctorDb);
                            return new Response<UserDTO>(true, "your password has been sent to your email address", null);

                        }
                    }
                    else if (dbUser.UserType.Equals("PARENT"))
                    {
                        var childDB = entities.Children.Where(x => x.UserID == dbUser.ID).FirstOrDefault();
                        if (childDB == null)
                        {
                            return new Response<UserDTO>(false, "Invalid Mobile Number", null);
                        }
                        else
                        {
                            UserEmail.ParentForgotPassword(childDB);
                            return new Response<UserDTO>(true, "your password has been sent to your email address", null);
                        }
                    }
                    else
                    {
                        return new Response<UserDTO>(false, "Please contact with admin", null);
                    }

                }
            }
            catch (Exception e)
            {
                return new Response<UserDTO>(false, GetMessageFromExceptionObject(e), null);

            }
        }

    }

}