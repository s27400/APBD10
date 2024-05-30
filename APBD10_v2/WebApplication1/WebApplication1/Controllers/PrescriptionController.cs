using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/prescriptions")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPatientWithPrescription(AddingPrescriptionDTO dto, CancellationToken cancellationToken)
    {
        string res = await _prescriptionService.AddPatientWithPrescription(dto, cancellationToken);
        if (res.StartsWith("Error"))
        {
            return NotFound(res);
        }

        return Ok(res);
    }
}