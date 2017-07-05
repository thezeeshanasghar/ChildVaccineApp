﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose.Model
{
    public class ChildVaccineDTO
    {
        public int ID { get; set; }
        public int ChildId { get; set; }
        public int DoseId { get; set; }
        public System.DateTime Date { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Circle { get; set; }
        public string Brand { get; set; }
        public bool IsDone { get; set; }
        
    }
}