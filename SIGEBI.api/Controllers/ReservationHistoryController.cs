using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Contracts.Services;

namespace SIGEBI.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHistoryController : ControllerBase
    {
        private readonly IReservationHistoryService _service;

        public ReservationHistoryController(IReservationHistoryService service)
        {
            _service = service;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllHistoriesAsync();
            if (!result.IsSuccess) return NotFound(result.Message);
            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetHistoryByIdAsync(id);
            if (!result.IsSuccess) return NotFound(result.Message);
            return Ok(result.Data);
        }
    }
}

