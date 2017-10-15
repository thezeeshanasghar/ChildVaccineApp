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
                    config.CreateMap<Doctor, DoctorDTO>().ReverseMap();
                    config.CreateMap<Clinic, ClinicDTO>().ReverseMap();
                    config.CreateMap<Child, ChildDTO>().ReverseMap();
                    config.CreateMap<User, UserDTO>().ReverseMap(); 
                    config.CreateMap<Schedule, ScheduleDTO>().ReverseMap();
                    config.CreateMap<DoctorSchedule, DoctorScheduleDTO>().ReverseMap();
                    config.CreateMap<BrandInventory, BrandInventoryDTO>().ReverseMap();
                    //config.CreateMap<BrandAmount, BrandAmountDTO>().ReverseMap();
                    config.CreateMap<Brand, BrandDTO>().ReverseMap();
                    config.CreateMap<Message, MessageDTO>().ReverseMap();
                    config.CreateMap<BrandAmount, BrandAmountDTO>().ReverseMap();
                    config.CreateMap<FollowUp, FollowUpDTO>().ReverseMap();
                    //Mapper.CreateMap<Source, Destination>().ForMember(dest => dest.SomePropToIgnore, opt => opt.Ignore())
                });
            }
    }
}