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
    
    public partial class Clinic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clinic()
        {
            this.Children = new HashSet<Child>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string OffDays { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public int DoctorID { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<bool> IsOnline { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Child> Children { get; set; }
    }
}
