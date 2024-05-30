using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly HospitalDbContext _context;

    public PrescriptionRepository(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<bool> PatientExists(int id, CancellationToken cancellationToken)
    {
        Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == id, cancellationToken);
        if (patient == null)
        {
            return false;
        }

        return true;
    }

    public async Task<string> AddPatient(PatientDTO patientDto, CancellationToken cancellationToken)
    {
        Patient patient = new Patient()
        {
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            Birthdate = patientDto.Birthdate
        };
        await _context.Patients.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return "dodalem";
    }

    public async Task<int> GetAddedPatientId(PatientDTO patientDto, CancellationToken cancellationToken)
    {
        Patient patient = await _context.Patients.FirstOrDefaultAsync(p =>
            p.FirstName == patientDto.FirstName && p.LastName == patientDto.LastName && p.Birthdate ==
                patientDto.Birthdate, cancellationToken);

        return patient.IdPatient;
    }

    public async Task<bool> MedicamentsVerification(List<PrescriptionMedsDTO> meds, CancellationToken cancellationToken)
    {
        foreach (PrescriptionMedsDTO dto in meds)
        {
            Medicament med = await _context.Medicaments.FirstOrDefaultAsync(m => m.IdMedicament == dto.IdMedicament);
            if (med == null)
            {
                return false;
            }
        }

        return true;
    }

    public bool AmountMeds(List<PrescriptionMedsDTO> meds)
    {
        if (meds.Count > 10)
        {
            return false;
        }

        return true;
    }
    
    public bool DateValidation(DateTime dueDate, DateTime date)
    {
        return dueDate >= date;
    }

    public async Task<string> AddPrescription(AddingPrescriptionDTO dto, CancellationToken cancellationToken)
    {
        Prescription prescription = new Prescription()
        {
            Date = dto.Date,
            DueDate = dto.DueDate,
            IdDoctor = dto.IdDoctor,
            IdPatient = dto.Patient.IdPatient
        };

        await _context.AddAsync(prescription,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return "dodalem prescription";
    }

    public async Task<int> GetAddedPrescriptionId(AddingPrescriptionDTO dto, CancellationToken cancellationToken)
    {
        Prescription pres = await _context.Prescriptions.FirstOrDefaultAsync(
            p => p.Date == dto.Date && p.DueDate == dto.DueDate && p.IdDoctor == dto.IdDoctor &&
                 p.IdPatient == dto.Patient.IdPatient, cancellationToken);

        return pres.IdPrescription;
    }

    public async Task<int> AddMedsToPrescription(List<PrescriptionMedsDTO> meds, CancellationToken cancellationToken, int IdPresc)
    {
        foreach (PrescriptionMedsDTO temp in meds)
        {
            PrescriptionMedicament toAdd = new PrescriptionMedicament()
            {
                IdMedication = temp.IdMedicament,
                Details = temp.Details,
                Dose = temp.Dose,
                IdPrescription = IdPresc
            };
            await _context.PrescriptionMedicaments.AddAsync(toAdd, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return 1;
    }
    
    public async Task<string> AddPatientWithPrescription(AddingPrescriptionDTO dto, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            bool patientExists = await PatientExists(dto.Patient.IdPatient, cancellationToken);
            int PatientId = dto.Patient.IdPatient;

            if (!patientExists)
            {
                await AddPatient(dto.Patient, cancellationToken);
                int newId = await GetAddedPatientId(dto.Patient, cancellationToken);
                PatientId = newId;
            }

            dto.Patient.IdPatient = PatientId;

            bool medsExist = await MedicamentsVerification(dto.medicaments, cancellationToken);

            if (!medsExist)
            {
                return "Error: Inserted medicaments not exist";
            }

            bool amount = AmountMeds(dto.medicaments);

            if (!amount)
            {
                return "Error: There is more than 10 meds in list";
            }

            bool dateVal = DateValidation(dto.DueDate, dto.Date);

            if (!dateVal)
            {
                return "Error: DueDate < Date";
            }

            await AddPrescription(dto, cancellationToken);
            int idPrescription = await GetAddedPrescriptionId(dto, cancellationToken);
            await AddMedsToPrescription(dto.medicaments, cancellationToken, idPrescription);
            await transaction.CommitAsync(cancellationToken);
            return $"Prescription with id {idPrescription} added";
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return "Error: Transaction Error.";
        }
    }
    
}