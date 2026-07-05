using PatientService.DTOs.Patient;

namespace PatientService.Services;

public interface IPatientService
{
    Task<IReadOnlyList<PatientResponseDto>> GetAllAsync();
    Task<PatientResponseDto?> GetByIdAsync(int id);
    Task<PatientResponseDto> AddAsync(CreatePatientRequestDto request);
    Task<PatientResponseDto?> UpdateAsync(int id, UpdatePatientRequestDto request);
    Task<bool> DeleteAsync(int id);
}
