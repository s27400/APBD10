namespace WebApplication1.Entities;

public class PrescriptionMedicament
{
    public int IdMedication { get; set; }
    public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
    public virtual Medicament MedicamentNavigation { get; set; }
    public virtual Prescription PrescriptionNavigation { get; set; }
}