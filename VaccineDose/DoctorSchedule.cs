
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
    
public partial class DoctorSchedule
{

    public int ID { get; set; }

    public int DoseID { get; set; }

    public int DoctorID { get; set; }

    public int GapInDays { get; set; }



    public virtual Doctor Doctor { get; set; }

    public virtual Dose Dose { get; set; }

}

}
