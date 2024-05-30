using System.IO.Pipelines;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public class PatientRepository : IPatientRepository
{
    
    private readonly HospitalDbContext _context;

    public PatientRepository(HospitalDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> PatientExists(int PatientId, CancellationToken cancellationToken)
    {
        Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == PatientId, cancellationToken);

        if (patient == null)
        {
            return false;
        }

        
        return true;
    }

    public async Task<PatientInfoFullDTO> GetCompleteInfoPatient(int PatientId, CancellationToken cancellationToken)
    {
        PatientInfoFullDTO patientDto = new PatientInfoFullDTO();
        
        Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == PatientId, cancellationToken);
        
        patientDto.IdPatient = patient.IdPatient;
        patientDto.FirstName = patient.FirstName;
        patientDto.LastName = patient.LastName;
        patientDto.Birthdate = patient.Birthdate;

        var presc = _context.Prescriptions
            .Include(p => p.IdDoctor)
            .Include(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.IdMedication)
            .Where(x => x.IdPatient == PatientId);

        var toPatient = await presc
            .Select(prescription => new PatientPrescriptionDTO()
            {
                IdPrescription = prescription.IdPrescription,
                Date = prescription.Date,
                DueDate = prescription.DueDate,
                Medicaments = prescription.PrescriptionMedicaments
                    .Select(med => new MedicamentDTO()
                    {
                        IdMedicament = med.MedicamentNavigation.IdMedicament,
                        Name = med.MedicamentNavigation.Name,
                        Dose = med.Dose,
                        Description = med.MedicamentNavigation.Description
                    }).ToList(),
                Doctor = 
            }).ToListAsync(cancellationToken);

        patientDto.Prescriptions = toPatient;
            
            

        return patientDto;
    }
}