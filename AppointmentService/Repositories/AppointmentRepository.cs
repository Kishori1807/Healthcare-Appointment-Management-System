using Microsoft.EntityFrameworkCore;
using AppointmentService.Data;
using AppointmentService.Models;

namespace AppointmentService.Repositories;

public class AppointmentRepository(AppointmentDbContext context) : IAppointmentRepository
{
    public async Task<IReadOnlyList<Appointment>> GetAllAsync() =>
        await context.Appointments.AsNoTracking().OrderBy(a => a.Id).ToListAsync();

    public async Task<Appointment?> GetByIdAsync(int id) =>
        await context.Appointments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

    public async Task<IReadOnlyList<Appointment>> GetHistoryByPatientIdAsync(int patientId) =>
        await context.Appointments.AsNoTracking().Where(a => a.PatientId == patientId).OrderByDescending(a => a.AppointmentDate).ToListAsync();

    public async Task<Appointment> AddAsync(Appointment appointment)
    {
        await context.Appointments.AddAsync(appointment);
        await context.SaveChangesAsync();
        return appointment;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        if (existing is null)
        {
            return false;
        }

        existing.Status = "Cancelled";
        await context.SaveChangesAsync();
        return true;
    }
}
