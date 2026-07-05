using AutoMapper;
using AppointmentService.DTOs.Appointment;
using AppointmentService.Models;

namespace AppointmentService.Mappings;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<CreateAppointmentRequestDto, Appointment>();
        CreateMap<Appointment, AppointmentResponseDto>();
    }
}
