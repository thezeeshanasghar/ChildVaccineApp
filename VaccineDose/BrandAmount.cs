//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VaccineDose
{
    using System;
    using System.Collections.Generic;
    
    public partial class BrandAmount
    {
        public int ID { get; set; }
        public Nullable<int> Amount { get; set; }
        public int BrandID { get; set; }
        public int DoctorID { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}