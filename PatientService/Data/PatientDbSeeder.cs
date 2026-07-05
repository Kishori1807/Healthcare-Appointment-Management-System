using PatientService.Models;

namespace PatientService.Data;

public static class PatientDbSeeder
{
    public static async Task SeedAsync(PatientDbContext context)
    {
        if (context.Patients.Any())
        {
            return;
        }

        var patients = new List<Patient>
        {
            new() { FullName = "Aarav Sharma", Age = 32, Gender = "Male", Phone = "+1-555-1001", Email = "aarav.sharma@example.com", CreatedDate = DateTime.UtcNow },
            new() { FullName = "Priya Nair", Age = 28, Gender = "Female", Phone = "+1-555-1002", Email = "priya.nair@example.com", CreatedDate = DateTime.UtcNow }
        };

        await context.Patients.AddRangeAsync(patients);
        await context.SaveChangesAsync();
    }
}
