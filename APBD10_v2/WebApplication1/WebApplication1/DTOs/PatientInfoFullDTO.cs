namespace WebApplication1.DTOs;

public class PatientInfoFullDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PatientPrescriptionDTO> Prescriptions { get; set; }
}