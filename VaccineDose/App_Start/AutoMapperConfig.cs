﻿using AutoMapper;

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

                    //Mapper.CreateMap<Source, Destination>().ForMember(dest => dest.SomePropToIgnore, opt => opt.Ignore())
                });
            }
    }
}