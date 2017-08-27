using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaccineDose
{
    public class ChangePasswordRequestDTO
    {
        public int UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set;  } 
    }
}