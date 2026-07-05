using AppointmentService.DTOs.Appointment;

namespace AppointmentService.Services;

public interface IAppointmentService
{
    Task<IReadOnlyList<AppointmentResponseDto>> GetAllAsync();
    Task<AppointmentResponseDto?> GetByIdAsync(int id);
    Task<IReadOnlyList<AppointmentResponseDto>> GetHistoryByPatientIdAsync(int patientId);
    Task<AppointmentResponseDto> AddAsync(CreateAppointmentRequestDto request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id);
}
