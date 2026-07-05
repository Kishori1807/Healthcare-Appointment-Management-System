using AutoMapper;
using PatientService.DTOs.Patient;
using PatientService.Models;

namespace PatientService.Mappings;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<CreatePatientRequestDto, Patient>();
        CreateMap<UpdatePatientRequestDto, Patient>();
        CreateMap<Patient, PatientResponseDto>();
    }
}
