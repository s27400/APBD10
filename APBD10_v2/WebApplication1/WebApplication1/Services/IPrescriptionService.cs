using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IPrescriptionService
{
    public Task<string> AddPatientWithPrescription(AddingPrescriptionDTO dto, CancellationToken cancellationToken);
}