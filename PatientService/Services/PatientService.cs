using AutoMapper;
using PatientService.DTOs.Patient;
using PatientService.Models;
using PatientService.Repositories;

namespace PatientService.Services;

public class PatientService(IPatientRepository patientRepository, IMapper mapper) : IPatientService
{
    public async Task<IReadOnlyList<PatientResponseDto>> GetAllAsync()
    {
        var patients = await patientRepository.GetAllAsync();
        return mapper.Map<IReadOnlyList<PatientResponseDto>>(patients);
    }

    public async Task<PatientResponseDto?> GetByIdAsync(int id)
    {
        var patient = await patientRepository.GetByIdAsync(id);
        return patient is null ? null : mapper.Map<PatientResponseDto>(patient);
    }

    public async Task<PatientResponseDto> AddAsync(CreatePatientRequestDto request)
    {
        var entity = mapper.Map<Patient>(request);
        entity.CreatedDate = DateTime.UtcNow;
        var saved = await patientRepository.AddAsync(entity);
        return mapper.Map<PatientResponseDto>(saved);
    }

    public async Task<PatientResponseDto?> UpdateAsync(int id, UpdatePatientRequestDto request)
    {
        var entity = mapper.Map<Patient>(request);
        entity.Id = id;
        var updated = await patientRepository.UpdateAsync(entity);
        return updated is null ? null : mapper.Map<PatientResponseDto>(updated);
    }

    public async Task<bool> DeleteAsync(int id) => await patientRepository.DeleteAsync(id);
}
