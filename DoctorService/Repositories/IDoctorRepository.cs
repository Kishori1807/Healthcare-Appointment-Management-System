using DoctorService.Models;

namespace DoctorService.Repositories;

public interface IDoctorRepository
{
    Task<IReadOnlyList<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(int id);
    Task<Doctor> AddAsync(Doctor doctor);
    Task<Doctor?> UpdateAsync(Doctor doctor);
    Task<bool> DeleteAsync(int id);
}
