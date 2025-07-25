using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Contracts.Services;
using SIGEBI.Application.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGEBI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationStatusController : ControllerBase
    {
        private readonly IReservationStatusService _reservationStatusService;

        public ReservationStatusController(IReservationStatusService reservationStatusService)
        {
            _reservationStatusService = reservationStatusService;
        }
        // GET: api/<ReservationStatusController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _reservationStatusService.GetAllStatusesAsync(x => true);

                if (!result.IsSuccess)
                {
                    return BadRequest(new
                    {
                        result.Message,
                        result.IsSuccess
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message, Success = false });
            }
        }

        // GET api/<ReservationStatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _reservationStatusService.GetStatusByIdAsync(id);

                if (!result.IsSuccess)
                {
                    return NotFound(new
                    {
                        result.Message,
                        result.IsSuccess
                    });
                }
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { Message = ex.Message, Success = false });
            }
        }

        //// POST api/<ReservationStatusController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ReservationStatusController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ReservationStatusController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
