using AutoMapper;
using VaccineDose.Model;

namespace VaccineDose
{
    public class AutoMapperConfig
    {
            public static void Initialize()
            {
                Mapper.Initialize((config) =>
                {
                    config.CreateMap<Dose, DoseDTO>().ReverseMap();
                    config.CreateMap<Vaccine, VaccineDTO>().ReverseMap();
                    config.CreateMap<DoseRule, DoseRuleDTO>().ReverseMap();
                    config.CreateMap<Doctor, DoctorDTO>().ReverseMap();
                    config.CreateMap<Clinic, ClinicDTO>().ReverseMap();
                    config.CreateMap<Child, ChildDTO>().ReverseMap();
                    config.CreateMap<User, UserDTO>().ReverseMap();


                    //Mapper.CreateMap<Source, Destination>().ForMember(dest => dest.SomePropToIgnore, opt => opt.Ignore())
                });
            }
    }
}