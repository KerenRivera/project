using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Base;
using SIGEBI.WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGEBI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHistoryController : ControllerBase
    {
        private readonly ReservationHistoryApiService _reservationHistoryApiService;

        public ReservationHistoryController(ReservationHistoryApiService reservationHistoryApiService)
        {
            _reservationHistoryApiService = reservationHistoryApiService;
        }
        // GET: api/<ReservationHistoryController>
        [HttpGet]
        public async Task<IActionResult>GetAll() //fix it
        {
            try
            {
                var result = await _reservationHistoryApiService.GetAllAsync();

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
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new { Message = ex.Message, Success = false });
            }
        }

        // GET api/<ReservationHistoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _reservationHistoryApiService.GetByIdAsync(id);

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
                return NotFound(ex.Message);
            }
        }

        //// POST api/<ReservationHistoryController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ReservationHistoryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ReservationHistoryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
