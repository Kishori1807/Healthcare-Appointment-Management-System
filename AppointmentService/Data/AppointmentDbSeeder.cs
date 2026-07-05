using AppointmentService.Models;

namespace AppointmentService.Data;

public static class AppointmentDbSeeder
{
    public static async Task SeedAsync(AppointmentDbContext context)
    {
        if (context.Appointments.Any())
        {
            return;
        }

        var appointments = new List<Appointment>
        {
            new() { PatientId = 1, DoctorId = 1, AppointmentDate = DateTime.UtcNow.AddDays(2), Status = "Booked", Notes = "Initial consultation", CreatedDate = DateTime.UtcNow },
            new() { PatientId = 2, DoctorId = 2, AppointmentDate = DateTime.UtcNow.AddDays(5), Status = "Booked", Notes = "Follow-up", CreatedDate = DateTime.UtcNow }
        };

        await context.Appointments.AddRangeAsync(appointments);
        await context.SaveChangesAsync();
    }
}
