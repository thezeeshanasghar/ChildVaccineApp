using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Http;
using VaccineDose.Model;
using System.Linq;
using System.Xml;
using Newtonsoft.Json.Linq;
using VaccineDose.App_Code;

namespace VaccineDose.Controllers
{
    public class MessageController : BaseController
    {
        public Response<List<MessageDTO>> Get([FromUri] string mobileNumber = "", [FromUri] string fromDate = "", [FromUri] string toDate = "")
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    List<Message> dbMessages = new List<Message>();

                    if (!string.IsNullOrEmpty(mobileNumber) || !string.IsNullOrEmpty(fromDate) || !string.IsNullOrEmpty(toDate))
                    {
                        var dbUser = entities.Users.Where(x => x.MobileNumber == mobileNumber && x.UserType == "DOCTOR").FirstOrDefault();
                        if (dbUser == null)
                            return new Response<List<MessageDTO>>(false, "No records found", null);
                        if (fromDate != null && toDate == null)
                        {
                            DateTime FromDate = DateTime.ParseExact(fromDate, "dd-MM-yyyy", null);
                            dbMessages = entities.Messages.Where(m => m.UserID == dbUser.ID && m.Created >= FromDate).ToList();
                        }
                        if (toDate != null && fromDate == null)
                        {
                            DateTime ToDate = DateTime.ParseExact(toDate, "dd-MM-yyyy", null);
                            dbMessages = entities.Messages.Where(m => m.UserID == dbUser.ID &&
                                            m.Created <= ToDate).ToList();
                        }
                        if (toDate != null && fromDate != null)
                        {
                            DateTime FromDate = DateTime.ParseExact(fromDate, "dd-MM-yyyy", null);
                            DateTime ToDate = DateTime.ParseExact(toDate, "dd-MM-yyyy", null);

                            dbMessages = entities.Messages.Where(m => m.UserID == dbUser.ID && m.Created >= FromDate &&
                                            m.Created <= ToDate).ToList();

                        }
                        if (toDate == null && fromDate == null)
                        {
                            dbMessages = entities.Messages.Where(m => m.UserID == dbUser.ID).ToList();
                        }

                    }
                    else
                    {
                        dbMessages = entities.Messages.ToList();
                    }


                    var messageDTOs = Mapper.Map<List<MessageDTO>>(dbMessages.OrderByDescending(x => x.Created));
                    return new Response<List<MessageDTO>>(true, null, messageDTOs);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<MessageDTO>>(false, GetMessageFromExceptionObject(ex), null);
            }
        }

        [Route("~/api/message/{id}/doctor")]
        public Response<List<MessageDTO>> Get(int id)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    var dbMessages = entities.Messages.Where(x => x.UserID == id).OrderByDescending(x => x.Created).ToList();
                    var messageDTOs = Mapper.Map<List<MessageDTO>>(dbMessages);
                    foreach (var msg in messageDTOs)
                    {
                        if (IsJson(msg.ApiResponse))
                        {
                            JObject json = JObject.Parse(msg.ApiResponse);
                            msg.ApiResponse = (string)json["returnString"];
                        }
                        else
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(msg.ApiResponse);

                            string xpath = "Response";
                            var parentNode = xmlDoc.SelectNodes(xpath);

                            foreach (XmlNode childrenNode in parentNode)
                                msg.ApiResponse = childrenNode.FirstChild.InnerText;
                        }
                    }

                    return new Response<List<MessageDTO>>(true, null, messageDTOs);
                }
            }
            catch (Exception ex)
            {
                return new Response<List<MessageDTO>>(false, GetMessageFromExceptionObject(ex), null);

            }


        }

        public Response<MessageDTO> Post([FromBody] MessageDTO msg)
        {
            try
            {
                using (VDEntities entities = new VDEntities())
                {
                    if (!string.IsNullOrEmpty(msg.SMS) && !string.IsNullOrEmpty(msg.MobileNumber))
                    {
                        var dbReceiver = entities.Users.Where(x => x.MobileNumber == msg.MobileNumber).FirstOrDefault();
                        if (dbReceiver != null)
                        {
                            var response = UserSMS.SendSMS(dbReceiver.CountryCode, dbReceiver.MobileNumber, "", msg.SMS);
                            UserSMS.addMessageToDB(dbReceiver.MobileNumber, response, msg.SMS, dbReceiver.ID);
                            return new Response<MessageDTO>(true, null, null);
                        }
                        else
                        {
                            return new Response<MessageDTO>(false, "The number " + msg.MobileNumber + " does not exist in our records", null);
                        }
                    }
                    else
                    {
                        return new Response<MessageDTO>(false, "Please fill sms and mobile number text fields", null);
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response<MessageDTO>(false, GetMessageFromExceptionObject(ex), null);
            }

        }

    }
}