using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using WebApplication1.Entities;

namespace WebApplication1.DTOs;

public class AddingPrescriptionDTO
{
    [Required]
    public PatientDTO Patient { get; set; }
    
    [Required]
    public int IdDoctor { get; set; }
    
    [Required]
    public List<PrescriptionMedsDTO> medicaments { get; set; }

    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
}