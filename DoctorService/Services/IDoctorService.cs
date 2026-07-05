using DoctorService.DTOs.Doctor;

namespace DoctorService.Services;

public interface IDoctorService
{
    Task<IReadOnlyList<DoctorResponseDto>> GetAllAsync();
    Task<DoctorResponseDto?> GetByIdAsync(int id);
    Task<DoctorResponseDto> AddAsync(CreateDoctorRequestDto request);
    Task<DoctorResponseDto?> UpdateAsync(int id, UpdateDoctorRequestDto request);
    Task<bool> DeleteAsync(int id);
}
