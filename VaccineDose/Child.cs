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
    
    public partial class Child
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Child()
        {
            this.ChildVaccines = new HashSet<ChildVaccine>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string MobileNumber { get; set; }
        public string StreetAddress { get; set; }
        public string Gender { get; set; }
        public Nullable<int> PreferredDayOfReminder { get; set; }
        public string City { get; set; }
        public string PreferredShedule { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChildVaccine> ChildVaccines { get; set; }
    }
}
