using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIGEBI.api.Services;
using SIGEBI.Application.DTOs;
using SIGEBI.Domain.Entities.circulation;
using SIGEBI.Persistence.Context;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SIGEBI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationApiService _reservationApiService; // Injecting the ReservationApiService
        private readonly SIGEBIContext _context;

        public ReservationController(ReservationApiService reservationApiService, SIGEBIContext context)
        {
            _reservationApiService = reservationApiService;
            _context = context;
        }

        [HttpGet]
        //[ProducesResponseType(typeof(List<ReservationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() //probar
        {
            try
            {
                Expression<Func<Reservation, bool>> filter = null;

                var result = await _reservationApiService.GetAllReservationsAsync(filter);

                if (!result.IsSuccess)
                    return BadRequest(new
                    {
                        Message = result.Message,
                        Success = result.IsSuccess
                    });
                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }

        // GET api/<ReservationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _reservationApiService.GetReservationByIdAsync(id);

                if (!result.IsSuccess)
                    return NotFound(new
                    {
                        result.Message,
                        result.IsSuccess
                    });
                return Ok(result);

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }

        //POST api/<ReservationController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequestDto requestDto) //probar
        {
            try
            {
                var createdReservation = await _reservationApiService.CreateReservationAsync(requestDto);
                //return CreatedAtAction(nameof(GetById), new { id = createdReservation.Data }, createdReservation.Data); 

                if (!createdReservation.IsSuccess)
                    return BadRequest(new
                    {
                        createdReservation.Message,
                        createdReservation.IsSuccess
                    });
                return Ok(createdReservation);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateReservationRequestDto requestDto)
        {
            try
            {
                var updatedReservation = await _reservationApiService.UpdateReservationAsync(requestDto);

                if (!updatedReservation.IsSuccess)
                return BadRequest(new
                {
                    updatedReservation.Message,
                    updatedReservation.IsSuccess
                });
                return Ok(updatedReservation);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new
                {
                    Message = ex.Message,
                    Success = false
                });
            }
        }

        // DELETE api/<ReservationController>/5
        [HttpDelete("{id}")]
        public async Task< IActionResult> Delete(int id)
        {
            try
            {
               var deleted = await _reservationApiService.DeleteReservationAsync(id);

                if (!deleted.IsSuccess)
                    return NotFound(new
                    {
                        deleted.Message,
                        deleted.IsSuccess
                    });

                return Ok(deleted);

            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message); // Return 404 

            }
        }
    }
}

