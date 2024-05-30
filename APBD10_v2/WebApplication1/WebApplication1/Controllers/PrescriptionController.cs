using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/prescriptions")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    
    public PrescriptionController(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    [HttpPost]
    public async Task<IActionResult> AddPatientWithPrescription(AddingPrescriptionDTO dto, CancellationToken cancellationToken)
    {
        string res = await _prescriptionRepository.AddPatientWithPrescription(dto, cancellationToken);
        if (res.StartsWith("Error"))
        {
            return NotFound(res);
        }

        return Ok(res);
    }
}