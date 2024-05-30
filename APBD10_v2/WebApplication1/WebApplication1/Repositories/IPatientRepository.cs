using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories;

public interface IPatientRepository
{
    public Task<bool> PatientExists(int PatientId, CancellationToken cancellationToken);
    public Task<PatientInfoFullDTO> GetCompleteInfoPatient(int PatientId, CancellationToken cancellationToken);
}