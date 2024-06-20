using AutoMapper;
using VenusSoftTask.DTOs;
using VenusSoftTask.Models;

namespace VenusSoftTask.Helper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientCreationDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.PatientEmails.FirstOrDefault()!.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PatientPhoneNumbers.FirstOrDefault()!.Phone));
        
        CreateMap<PatientCreationDTO, Patient>()
            .ForMember(dest => dest.PatientEmails, opt => opt.Ignore())
            .ForMember(dest => dest.PatientPhoneNumbers, opt => opt.Ignore());
        
        CreateMap<Patient, PatientDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.PatientEmails.FirstOrDefault()!.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PatientPhoneNumbers.FirstOrDefault()!.Phone));

        CreateMap<Patient, PatientUpdationDTO>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.PatientEmails.FirstOrDefault()!.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PatientPhoneNumbers.FirstOrDefault()!.Phone));
        
        CreateMap<PatientUpdationDTO, Patient>()
            .ForMember(dest => dest.PatientEmails, opt => opt.Ignore())
            .ForMember(dest => dest.PatientPhoneNumbers, opt => opt.Ignore());

    }
}