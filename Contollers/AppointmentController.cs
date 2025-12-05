using Microsoft.AspNetCore.Mvc;
using RijschoolHarmonieApp.DTOs.Appointment;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Services;

namespace RijschoolHarmonieApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentsController(IAppointmentService service)
        {
            _service = service;
        }

        // GET: api/appointments
        [HttpGet]
        public async Task<ActionResult<List<AppointmentResponseDto>>> GetAll()
        {
            var appointments = await _service.GetAllAsync();
            return Ok(appointments);
        }

        // GET: api/appointments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentResponseDto>> GetById(int id)
        {
            var appointment = await _service.GetByIdAsync(id);
            if (appointment == null)
                return NotFound();
            return Ok(appointment);
        }

        // GET: api/appointments/instructor/{instructorId}?start=&end=
        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<List<AppointmentResponseDto>>> GetByInstructor(
            int instructorId,
            [FromQuery] DateTime? start = null,
            [FromQuery] DateTime? end = null)
        {
            var appointments = await _service.GetByInstructorAsync(instructorId, start, end);
            return Ok(appointments);
        }

        // GET: api/appointments/student/{studentId}?start=&end=
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<List<AppointmentResponseDto>>> GetByStudent(
            int studentId,
            [FromQuery] DateTime? start = null,
            [FromQuery] DateTime? end = null)
        {
            var appointments = await _service.GetByStudentAsync(studentId, start, end);
            return Ok(appointments);
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<ActionResult<AppointmentResponseDto>> Create([FromBody] CreateAppointmentDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.AppointmentId }, created);
        }

        // PUT: api/appointments
        [HttpPut]
        public async Task<ActionResult<AppointmentResponseDto?>> Update([FromBody] UpdateAppointmentDto dto)
        {
            var updated = await _service.UpdateAsync(dto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        // DELETE: api/appointments/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        // GET: api/appointments/filter?instructorId=&studentId=&start=&end=&type=
        [HttpGet("filter")]
        public async Task<ActionResult<List<AppointmentResponseDto>>> GetFiltered(
            [FromQuery] int? instructorId,
            [FromQuery] int? studentId,
            [FromQuery] DateTime? start,
            [FromQuery] DateTime? end,
            [FromQuery] AppointmentType? type)
        {
            var list = await _service.GetFilteredAsync(instructorId, studentId, start, end, type);
            return Ok(list);
        }
    }
}
