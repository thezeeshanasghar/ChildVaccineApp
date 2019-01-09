namespace VaccineDose.Model
{
    public class DashboardDTO
    {
        public int VaccineCount { get; set; }
        public int BrandCount { get; set; }
        public int DoseCount { get; set; }
        public int ChildCount { get; set; }
        public int ApprovedDoctorCount { get; set; }
        public int UnApprovedDoctorCount { get; set; }
    }
}