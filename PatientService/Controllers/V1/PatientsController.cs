using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PatientService.DTOs.Patient;
using PatientService.Services;

namespace PatientService.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/patients")]
public class PatientsController(IPatientService patientService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PatientResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var patients = await patientService.GetAllAsync();
        return Ok(patients);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PatientResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await patientService.GetByIdAsync(id);
        return patient is null ? NotFound(new { message = "Patient not found." }) : Ok(patient);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PatientResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePatientRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var created = await patientService.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1" }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PatientResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var updated = await patientService.UpdateAsync(id, request);
        return updated is null ? NotFound(new { message = "Patient not found." }) : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await patientService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound(new { message = "Patient not found." });
    }
}
