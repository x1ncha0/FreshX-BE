using Azure.Core;
using Freshx_API.Dtos;
using Freshx_API.Dtos.CommonDtos;
using Freshx_API.Interfaces;
using Freshx_API.Services.CommonServices;
using Microsoft.AspNetCore.Mvc;

namespace Freshx_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalServiceRequestsController : ControllerBase
    {
        private readonly IMedicalServiceRequestService _service;

        public MedicalServiceRequestsController(IMedicalServiceRequestService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<MedicalServiceRequestDto>> GetById()
        {
            var result = await _service.GetAllAsync();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalServiceRequestDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalServiceRequestDto>> Create(CreateMedicalServiceRequestDto dto)
        {
            var result = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.MedicalServiceRequestId }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MedicalServiceRequestDto>> Update(int id, CreateMedicalServiceRequestDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }


}
