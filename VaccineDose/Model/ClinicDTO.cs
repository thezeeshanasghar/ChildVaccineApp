﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaccineDose.Model;

namespace VaccineDose
{
    public class ClinicDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public int ConsultationFee { get; set; }
        
        public double Lat { get; set; }
        public double Long { get; set; }
        public int DoctorID { get; set; }
        public bool IsOnline { get; set; }
        public DoctorDTO Doctor { get; set; }
        
        //to show child count 
        public int childrenCount { get; set; }
        public List<ClinicTimingDTO> ClinicTimings { get; set; }

    }
}