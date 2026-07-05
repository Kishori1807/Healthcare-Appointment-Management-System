using Microsoft.EntityFrameworkCore;
using DoctorService.Data;
using DoctorService.Models;

namespace DoctorService.Repositories;

public class DoctorRepository(DoctorDbContext context) : IDoctorRepository
{
    public async Task<IReadOnlyList<Doctor>> GetAllAsync() =>
        await context.Doctors.AsNoTracking().OrderBy(d => d.Id).ToListAsync();

    public async Task<Doctor?> GetByIdAsync(int id) =>
        await context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);

    public async Task<Doctor> AddAsync(Doctor doctor)
    {
        await context.Doctors.AddAsync(doctor);
        await context.SaveChangesAsync();
        return doctor;
    }

    public async Task<Doctor?> UpdateAsync(Doctor doctor)
    {
        var existing = await context.Doctors.FirstOrDefaultAsync(d => d.Id == doctor.Id);
        if (existing is null)
        {
            return null;
        }

        existing.FullName = doctor.FullName;
        existing.Specialization = doctor.Specialization;
        existing.Experience = doctor.Experience;
        existing.ConsultationFee = doctor.ConsultationFee;
        existing.Available = doctor.Available;

        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        if (existing is null)
        {
            return false;
        }

        context.Doctors.Remove(existing);
        await context.SaveChangesAsync();
        return true;
    }
}
