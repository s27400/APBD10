using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public interface IPrescriptionRepository
{
    public Task<bool> PatientExists(int id, CancellationToken cancellationToken);
    public Task<string> AddPatient(PatientDTO patientDto, CancellationToken cancellationToken);
    public Task<int> GetAddedPatientId(PatientDTO patientDto, CancellationToken cancellationToken);
    public Task<bool> MedicamentsVerification(List<PrescriptionMedsDTO> meds, CancellationToken cancellationToken);
    public bool AmountMeds(List<PrescriptionMedsDTO> meds);
    public bool DateValidation(DateTime dueDate, DateTime date);
    public Task<string> AddPrescription(AddingPrescriptionDTO dto, CancellationToken cancellationToken);
    public Task<int> GetAddedPrescriptionId(AddingPrescriptionDTO dto, CancellationToken cancellationToken);
    public Task<int> AddMedsToPrescription(List<PrescriptionMedsDTO> meds, CancellationToken cancellationToken,  int IdPresc);
    
    public Task<string> AddPatientWithPrescription(AddingPrescriptionDTO dto, CancellationToken cancellationToken);
}