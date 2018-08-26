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
    
    public partial class Doctor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Doctor()
        {
            this.BrandAmounts = new HashSet<BrandAmount>();
            this.BrandInventories = new HashSet<BrandInventory>();
            this.Clinics = new HashSet<Clinic>();
            this.DoctorSchedules = new HashSet<DoctorSchedule>();
            this.FollowUps = new HashSet<FollowUp>();
        }
    
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PMDC { get; set; }
        public bool IsApproved { get; set; }
        public bool ShowPhone { get; set; }
        public bool ShowMobile { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<System.DateTime> ValidUpto { get; set; }
        public int UserID { get; set; }
        public Nullable<int> InvoiceNumber { get; set; }
        public string ProfileImage { get; set; }
        public string SignatureImage { get; set; }
        public string DisplayName { get; set; }
        public bool AllowInvoice { get; set; }
        public bool AllowChart { get; set; }
        public bool AllowFollowUp { get; set; }
        public bool AllowInventory { get; set; }
        public int SMSLimit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BrandAmount> BrandAmounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BrandInventory> BrandInventories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clinic> Clinics { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FollowUp> FollowUps { get; set; }
    }
}
