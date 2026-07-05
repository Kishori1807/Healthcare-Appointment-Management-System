using System.ComponentModel.DataAnnotations;

namespace AppointmentService.DTOs.Appointment;

public class CreateAppointmentRequestDto
{
    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }

    [Range(1, int.MaxValue)]
    public int DoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }

    [StringLength(300)]
    public string? Notes { get; set; }
}
