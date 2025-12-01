using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Services;

namespace RijschoolHarmonieApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentAccountController : ControllerBase
    {
        private readonly IStudentAccountService _studentAccount;

        public StudentAccountController(IStudentAccountService studentAccountService)
        {
            _studentAccount = studentAccountService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentAccount>>> GetAll()
        {
            var accounts = await _studentAccount.GetAllAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentAccount>> GetById(int id)
        {
            var account = await _studentAccount.GetByIdAsync(id);
            if (account == null)
                return NotFound("Account not found");
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentAccount account)
        {
            try
            {
                await _studentAccount.AddAsync(account);
                return CreatedAtAction(
                    nameof(GetById),
                    new { id = account.StudentAccountId },
                    account
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, StudentAccount account)
        {
            if (id != account.StudentAccountId)
                return BadRequest("ID mismatch");

            try
            {
                await _studentAccount.UpdateAsync(account);
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
                await _studentAccount.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
