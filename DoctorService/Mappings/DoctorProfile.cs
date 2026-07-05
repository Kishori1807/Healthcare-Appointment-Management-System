using AutoMapper;
using DoctorService.DTOs.Doctor;
using DoctorService.Models;

namespace DoctorService.Mappings;

public class DoctorProfile : Profile
{
    public DoctorProfile()
    {
        CreateMap<CreateDoctorRequestDto, Doctor>();
        CreateMap<UpdateDoctorRequestDto, Doctor>();
        CreateMap<Doctor, DoctorResponseDto>();
    }
}
