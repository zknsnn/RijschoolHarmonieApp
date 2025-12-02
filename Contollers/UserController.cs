using Microsoft.AspNetCore.Mvc;
using RijschoolHarmonieApp.DTOs.User;
using RijschoolHarmonieApp.Services;

namespace RijschoolHarmonieApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponseDto>>> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserDto dto)
        {
            var createdUser = await _userService.AddAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateUserDto dto)
        {
            try
            {
                await _userService.UpdateAsync(id, dto);
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
                await _userService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
