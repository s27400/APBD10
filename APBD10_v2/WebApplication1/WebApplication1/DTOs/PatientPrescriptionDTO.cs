using System.Runtime.InteropServices.JavaScript;

namespace WebApplication1.DTOs;

public class PatientPrescriptionDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDTO> Medicaments { get; set; }
    public DoctorDTO Doctor { get; set; }
    
}