using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using DoctorService.DTOs.Doctor;
using DoctorService.Services;

namespace DoctorService.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/doctors")]
public class DoctorsController(IDoctorService doctorService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DoctorResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await doctorService.GetAllAsync();
        return Ok(doctors);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DoctorResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var doctor = await doctorService.GetByIdAsync(id);
        return doctor is null ? NotFound(new { message = "Doctor not found." }) : Ok(doctor);
    }

    [HttpPost]
    [ProducesResponseType(typeof(DoctorResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDoctorRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var created = await doctorService.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1" }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(DoctorResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctorRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var updated = await doctorService.UpdateAsync(id, request);
        return updated is null ? NotFound(new { message = "Doctor not found." }) : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await doctorService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound(new { message = "Doctor not found." });
    }
}
