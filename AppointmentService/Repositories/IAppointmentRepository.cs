using AppointmentService.Models;

namespace AppointmentService.Repositories;

public interface IAppointmentRepository
{
    Task<IReadOnlyList<Appointment>> GetAllAsync();
    Task<Appointment?> GetByIdAsync(int id);
    Task<IReadOnlyList<Appointment>> GetHistoryByPatientIdAsync(int patientId);
    Task<Appointment> AddAsync(Appointment appointment);
    Task<bool> DeleteAsync(int id);
}
