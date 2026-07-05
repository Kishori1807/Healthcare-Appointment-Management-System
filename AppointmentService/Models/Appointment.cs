using System.ComponentModel.DataAnnotations;

namespace AppointmentService.Models;

public class Appointment
{
    public int Id { get; set; }

    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }

    [Range(1, int.MaxValue)]
    public int DoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }

    [Required]
    [MaxLength(40)]
    public string Status { get; set; } = "Booked";

    [MaxLength(300)]
    public string? Notes { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
