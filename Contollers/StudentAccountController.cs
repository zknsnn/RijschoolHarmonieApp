using Microsoft.AspNetCore.Mvc;
using RijschoolHarmonieApp.DTOs;
using RijschoolHarmonieApp.DTOs.StudentAccount;
using RijschoolHarmonieApp.Services;

namespace RijschoolHarmonieApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentAccountsController : ControllerBase
    {
        private readonly IStudentAccountService _studentAccountService;

        public StudentAccountsController(IStudentAccountService studentAccountService)
        {
            _studentAccountService = studentAccountService;
        }

        // GET: api/StudentAccounts
        [HttpGet]
        public async Task<ActionResult<List<StudentAccountResponseDto>>> GetAll()
        {
            var accounts = await _studentAccountService.GetAllAsync();
            return Ok(accounts);
        }

        // GET: api/StudentAccounts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentAccountResponseDto>> GetById(int id)
        {
            var account = await _studentAccountService.GetByIdAsync(id);
            if (account == null)
                return NotFound("StudentAccount not found");

            return Ok(account);
        }

        // POST: api/StudentAccounts
        [HttpPost]
        public async Task<ActionResult> Create(CreateStudentAccountDto dto)
        {
            try
            {
                var createdAccount = await _studentAccountService.AddAsync(dto);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdAccount.StudentAccountId },
                    createdAccount
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/StudentAccounts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateStudentAccountDto dto)
        {
            if (id != dto.StudentAccountId)
                return BadRequest("ID mismatch");

            try
            {
                var updated = await _studentAccountService.UpdateAsync(dto);
                if (updated == null)
                    return NotFound("StudentAccount not found");

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/StudentAccounts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _studentAccountService.DeleteAsync(id);
            if (!deleted)
                return NotFound("StudentAccount not found");

            return NoContent();
        }
    }
}
