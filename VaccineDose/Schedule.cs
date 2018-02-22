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
    
    public partial class Schedule
    {
        public int ID { get; set; }
        public int ChildId { get; set; }
        public int DoseId { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> Circle { get; set; }
        public Nullable<int> BrandId { get; set; }
        public bool IsDone { get; set; }
        public bool Due2EPI { get; set; }
        public Nullable<System.DateTime> GivenDate { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Child Child { get; set; }
        public virtual Dose Dose { get; set; }
    }
}
