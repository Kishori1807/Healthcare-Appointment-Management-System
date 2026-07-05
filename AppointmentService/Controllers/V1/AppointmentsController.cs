using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using AppointmentService.DTOs.Appointment;
using AppointmentService.Services;

namespace AppointmentService.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/appointments")]
public class AppointmentsController(IAppointmentService appointmentService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AppointmentResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await appointmentService.GetAllAsync();
        return Ok(appointments);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AppointmentResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await appointmentService.GetByIdAsync(id);
        return appointment is null ? NotFound(new { message = "Appointment not found." }) : Ok(appointment);
    }

    [HttpGet("history/{patientId:int}")]
    [ProducesResponseType(typeof(IEnumerable<AppointmentResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHistory(int patientId)
    {
        var history = await appointmentService.GetHistoryByPatientIdAsync(patientId);
        return Ok(history);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AppointmentResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentRequestDto request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var created = await appointmentService.AddAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1" }, created);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cancel(int id)
    {
        var deleted = await appointmentService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound(new { message = "Appointment not found." });
    }
}
