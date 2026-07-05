using AutoMapper;
using AppointmentService.Clients;
using AppointmentService.Common;
using AppointmentService.DTOs.Appointment;
using AppointmentService.Models;
using AppointmentService.Repositories;

namespace AppointmentService.Services;

public class AppointmentService(IAppointmentRepository appointmentRepository, IExternalValidationClient externalValidationClient, IMapper mapper) : IAppointmentService
{
    public async Task<IReadOnlyList<AppointmentResponseDto>> GetAllAsync()
    {
        var appointments = await appointmentRepository.GetAllAsync();
        return mapper.Map<IReadOnlyList<AppointmentResponseDto>>(appointments);
    }

    public async Task<AppointmentResponseDto?> GetByIdAsync(int id)
    {
        var appointment = await appointmentRepository.GetByIdAsync(id);
        return appointment is null ? null : mapper.Map<AppointmentResponseDto>(appointment);
    }

    public async Task<IReadOnlyList<AppointmentResponseDto>> GetHistoryByPatientIdAsync(int patientId)
    {
        var appointments = await appointmentRepository.GetHistoryByPatientIdAsync(patientId);
        return mapper.Map<IReadOnlyList<AppointmentResponseDto>>(appointments);
    }

    public async Task<AppointmentResponseDto> AddAsync(CreateAppointmentRequestDto request, CancellationToken cancellationToken = default)
    {
        var patientExists = await externalValidationClient.PatientExistsAsync(request.PatientId, cancellationToken);
        if (!patientExists)
        {
            throw new ResourceNotFoundException("Patient not found.");
        }

        var doctorExists = await externalValidationClient.DoctorExistsAsync(request.DoctorId, cancellationToken);
        if (!doctorExists)
        {
            throw new ResourceNotFoundException("Doctor not found.");
        }

        var entity = mapper.Map<Appointment>(request);
        entity.Status = "Booked";
        entity.CreatedDate = DateTime.UtcNow;

        var created = await appointmentRepository.AddAsync(entity);
        return mapper.Map<AppointmentResponseDto>(created);
    }

    public async Task<bool> DeleteAsync(int id) => await appointmentRepository.DeleteAsync(id);
}
