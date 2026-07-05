using AutoMapper;
using DoctorService.DTOs.Doctor;
using DoctorService.Models;
using DoctorService.Repositories;

namespace DoctorService.Services;

public class DoctorService(IDoctorRepository doctorRepository, IMapper mapper) : IDoctorService
{
    public async Task<IReadOnlyList<DoctorResponseDto>> GetAllAsync()
    {
        var doctors = await doctorRepository.GetAllAsync();
        return mapper.Map<IReadOnlyList<DoctorResponseDto>>(doctors);
    }

    public async Task<DoctorResponseDto?> GetByIdAsync(int id)
    {
        var doctor = await doctorRepository.GetByIdAsync(id);
        return doctor is null ? null : mapper.Map<DoctorResponseDto>(doctor);
    }

    public async Task<DoctorResponseDto> AddAsync(CreateDoctorRequestDto request)
    {
        var entity = mapper.Map<Doctor>(request);
        var saved = await doctorRepository.AddAsync(entity);
        return mapper.Map<DoctorResponseDto>(saved);
    }

    public async Task<DoctorResponseDto?> UpdateAsync(int id, UpdateDoctorRequestDto request)
    {
        var entity = mapper.Map<Doctor>(request);
        entity.Id = id;
        var updated = await doctorRepository.UpdateAsync(entity);
        return updated is null ? null : mapper.Map<DoctorResponseDto>(updated);
    }

    public async Task<bool> DeleteAsync(int id) => await doctorRepository.DeleteAsync(id);
}
