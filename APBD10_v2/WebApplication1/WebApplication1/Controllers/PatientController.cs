using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;

    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet("{PatientId}")]
    public async Task<IActionResult> GetFullPatientInfo(int PatientId, CancellationToken cancellationToken)
    {
        PatientInfoFullDTO patientInfoFullDto = new PatientInfoFullDTO();
        bool verify = await _patientRepository.PatientExists(PatientId, cancellationToken);

        if (!verify)
        {
            return NotFound($"Patient with id {PatientId} not found");
        }

        return Ok(await _patientRepository.GetCompleteInfoPatient(PatientId, cancellationToken));
    }
}