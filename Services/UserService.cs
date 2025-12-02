using AutoMapper;
using RijschoolHarmonieApp.DTOs.User;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Repositories;

namespace RijschoolHarmonieApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> AddAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            await _userRepository.AddAsync(user);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task UpdateAsync(int id, UpdateUserDto dto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
                throw new KeyNotFoundException("User not found");

            _mapper.Map(dto, existingUser);

            await _userRepository.UpdateAsync(existingUser);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
