using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class PrescriptionMedsDTO
{
    [Required]
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
}