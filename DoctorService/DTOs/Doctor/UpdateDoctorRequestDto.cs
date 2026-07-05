using System.ComponentModel.DataAnnotations;

namespace DoctorService.DTOs.Doctor;

public class UpdateDoctorRequestDto
{
    [Required]
    [StringLength(120, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(80, MinimumLength = 2)]
    public string Specialization { get; set; } = string.Empty;

    [Range(0, 70)]
    public int Experience { get; set; }

    [Range(0, 50000)]
    public decimal ConsultationFee { get; set; }

    public bool Available { get; set; } = true;
}
