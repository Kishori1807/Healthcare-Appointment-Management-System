using Microsoft.EntityFrameworkCore;
using PatientService.Data;
using PatientService.Models;

namespace PatientService.Repositories;

public class PatientRepository(PatientDbContext context) : IPatientRepository
{
    public async Task<IReadOnlyList<Patient>> GetAllAsync() =>
        await context.Patients.AsNoTracking().OrderBy(p => p.Id).ToListAsync();

    public async Task<Patient?> GetByIdAsync(int id) =>
        await context.Patients.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Patient> AddAsync(Patient patient)
    {
        await context.Patients.AddAsync(patient);
        await context.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient?> UpdateAsync(Patient patient)
    {
        var existing = await context.Patients.FirstOrDefaultAsync(p => p.Id == patient.Id);
        if (existing is null)
        {
            return null;
        }

        existing.FullName = patient.FullName;
        existing.Age = patient.Age;
        existing.Gender = patient.Gender;
        existing.Phone = patient.Phone;
        existing.Email = patient.Email;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        if (existing is null)
        {
            return false;
        }

        context.Patients.Remove(existing);
        await context.SaveChangesAsync();
        return true;
    }
}
