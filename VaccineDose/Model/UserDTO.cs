﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public int DoctorID { get; set; }
        public int ChildID { get; set; }
        public string CountryCode { get; set; }
        public string Email { get; set; }

    }
}