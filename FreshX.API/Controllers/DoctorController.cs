using FreshX.Application.Dtos;
using FreshX.Application.Dtos.CommonDtos;
using FreshX.Application.Dtos.Doctor;
using FreshX.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshX.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorController(IDoctorService doctorService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<DoctorDto>> Create([FromForm] DoctorCreateUpdateDto request, CancellationToken cancellationToken)
    {
        var doctor = await doctorService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = doctor.DoctorId }, doctor);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DoctorDto>>> Get([FromQuery] Parameters parameters, CancellationToken cancellationToken)
    {
        var doctors = await doctorService.GetAsync(parameters, cancellationToken);
        return Ok(doctors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DoctorDetailDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var doctor = await doctorService.GetByIdAsync(id, cancellationToken);
        return doctor is null ? NotFound() : Ok(doctor);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<DoctorDto>> Update(int id, [FromForm] DoctorCreateUpdateDto request, CancellationToken cancellationToken)
    {
        var doctor = await doctorService.UpdateAsync(id, request, cancellationToken);
        return doctor is null ? NotFound() : Ok(doctor);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<DoctorDto>> Delete(int id, CancellationToken cancellationToken)
    {
        var doctor = await doctorService.DeleteAsync(id, cancellationToken);
        return doctor is null ? NotFound() : Ok(doctor);
    }
}
