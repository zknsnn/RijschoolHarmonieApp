using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Services;

namespace RijschoolHarmonieApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorPriceController : ControllerBase
    {
        private readonly IInstructorPriceService _instructorPriceService;

        public InstructorPriceController(IInstructorPriceService instructorPrice)
        {
            _instructorPriceService = instructorPrice;
        }

        [HttpGet]
        public async Task<ActionResult<List<InstructorPrice>>> GetAll()
        {
            var users = await _instructorPriceService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorPrice>> GetById(int id)
        {
            var user = await _instructorPriceService.GetByIdAsync(id);
            if (user == null)
                return NotFound("Price not found");
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Create(InstructorPrice price)
        {
            try
            {
                await _instructorPriceService.AddAsync(price);
                return CreatedAtAction(nameof(GetById), new { id = price.InstructorId }, price);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, InstructorPrice price)
        {
            if (id != price.InstructorId)
                return BadRequest("ID mismatch");

            try
            {
                await _instructorPriceService.UpdateAsync(price);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _instructorPriceService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
