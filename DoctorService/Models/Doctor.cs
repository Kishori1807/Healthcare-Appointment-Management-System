using System.ComponentModel.DataAnnotations;

namespace DoctorService.Models;

public class Doctor
{
    public int Id { get; set; }

    [Required]
    [MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [MaxLength(80)]
    public string Specialization { get; set; } = string.Empty;

    [Range(0, 70)]
    public int Experience { get; set; }

    [Range(0, 50000)]
    public decimal ConsultationFee { get; set; }

    public bool Available { get; set; } = true;
}
