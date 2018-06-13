﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using VaccineDose.Model;
using System.Linq;
using System.Xml;
using System.Web;

namespace VaccineDose.Controllers
{
    public class MessageController : BaseController
    {
        public Response<List<MessageDTO>> Get([FromUri] string mobileNumber="", [FromUri] string fromDate = "", [FromUri] string toDate = "")
        {
            try
            {
                using (VDConnectionString entities = new VDConnectionString())
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
                using (VDConnectionString entities = new VDConnectionString())
                {
                    var dbMessages = entities.Messages.Where(x => x.UserID == id).OrderByDescending(x => x.Created).ToList();
                    var messageDTOs = Mapper.Map<List<MessageDTO>>(dbMessages);
                    foreach(var msg in messageDTOs)
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(msg.ApiResponse);

                        string xpath = "Response";
                        var parentNode = xmlDoc.SelectNodes(xpath);

                        foreach (XmlNode childrenNode in parentNode)
                        {
                            //HttpContext.Current.Response.Write(childrenNode.SelectSingleNode("//Message").Value);
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
    }
}