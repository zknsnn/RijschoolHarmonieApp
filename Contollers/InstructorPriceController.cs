using Microsoft.AspNetCore.Mvc;
using RijschoolHarmonieApp.DTOs.InstructorPrice;
using RijschoolHarmonieApp.DTOs.User;
using RijschoolHarmonieApp.Services;

namespace RijschoolHarmonieApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorPriceController : ControllerBase
    {
        private readonly IInstructorPriceService _instructorPriceService;

        public InstructorPriceController(IInstructorPriceService instructorPriceService)
        {
            _instructorPriceService = instructorPriceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<InstructorPriceResponseDto>>> GetAll()
        {
            var prices = await _instructorPriceService.GetAllAsync();
            return Ok(prices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorPriceResponseDto>> GetById(int id)
        {
            var price = await _instructorPriceService.GetByIdAsync(id);
            if (price == null)
                return NotFound("InstructorPrice not found");

            return Ok(price);
        }

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<InstructorPriceResponseDto>> GetByInstructorId(int instructorId)
        {
            var price = await _instructorPriceService.GetByInstructorAsync(instructorId);
            if (price == null)
                return NotFound("InstructorPrice not found");

            return Ok(price);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateInstructorPriceDto dto)
        {
            try
            {
                var createdPrice = await _instructorPriceService.AddAsync(dto);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdPrice.InstructorPriceId },
                    createdPrice
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateInstructorPriceDto dto)
        {
            if (id != dto.InstructorPriceId)
                return BadRequest("ID mismatch");

            try
            {
                var updated = await _instructorPriceService.UpdateAsync(dto);

                if (updated == null)
                    return NotFound("InstructorPrice not found");

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _instructorPriceService.DeleteAsync(id);
            if (!deleted)
                return NotFound("InstructorPrice not found");

            return NoContent();
        }
    }
}
