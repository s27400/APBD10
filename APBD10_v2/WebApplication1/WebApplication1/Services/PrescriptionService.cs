using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.DTOs;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    
    public PrescriptionService(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<string> AddPatientWithPrescription(AddingPrescriptionDTO dto, CancellationToken cancellationToken)
    {
        bool patientExists = await _prescriptionRepository.PatientExists(dto.Patient.IdPatient, cancellationToken);
        int PatientId = dto.Patient.IdPatient;

        if (!patientExists)
        {
            await _prescriptionRepository.AddPatient(dto.Patient, cancellationToken);
            int newId = await _prescriptionRepository.GetAddedPatientId(dto.Patient, cancellationToken);
            PatientId = newId;
        }

        dto.Patient.IdPatient = PatientId;

        bool medsExist = await _prescriptionRepository.MedicamentsVerification(dto.medicaments, cancellationToken);

        if (!medsExist)
        {
            return "Error: Inserted medicaments not exist";
        }

        bool amount = _prescriptionRepository.AmountMeds(dto.medicaments);

        if (!amount)
        {
            return "Error: There is more than 10 meds in list";
        }

        bool dateVal = _prescriptionRepository.DateValidation(dto.DueDate, dto.Date);

        if (!dateVal)
        {
            return "Error: DueDate < Date";
        }

        await _prescriptionRepository.AddPrescription(dto, cancellationToken);
        int idPrescription = await _prescriptionRepository.GetAddedPrescriptionId(dto, cancellationToken);
        await _prescriptionRepository.AddMedsToPrescription(dto.medicaments, cancellationToken, idPrescription);
        return $"Prescription with id {idPrescription} added";
    }
}