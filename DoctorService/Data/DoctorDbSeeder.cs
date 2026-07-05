using DoctorService.Models;

namespace DoctorService.Data;

public static class DoctorDbSeeder
{
    public static async Task SeedAsync(DoctorDbContext context)
    {
        if (context.Doctors.Any())
        {
            return;
        }

        var doctors = new List<Doctor>
        {
            new() { FullName = "Dr. Meera Kapoor", Specialization = "Cardiology", Experience = 10, ConsultationFee = 1200, Available = true },
            new() { FullName = "Dr. Rahul Menon", Specialization = "Dermatology", Experience = 8, ConsultationFee = 900, Available = true }
        };

        await context.Doctors.AddRangeAsync(doctors);
        await context.SaveChangesAsync();
    }
}
