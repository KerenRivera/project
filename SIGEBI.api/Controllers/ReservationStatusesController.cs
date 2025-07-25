using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Contracts.Services;

namespace SIGEBI.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationStatusesController : ControllerBase
    {
        private readonly IReservationStatusService service;

        public ReservationStatusesController(IReservationStatusService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllStatusesAsync();
            if (!result.IsSuccess) return NotFound(result.Message);
            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetStatusByIdAsync(id);
            if (!result.IsSuccess) return NotFound(result.Message);
            return Ok(result.Data);
        }
    }
}

