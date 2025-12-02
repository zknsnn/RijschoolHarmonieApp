
using AutoMapper;
using RijschoolHarmonieApp.DTOs.InstructorPrice;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Repositories;

namespace RijschoolHarmonieApp.Services
{
    public class InstructorPriceService : IInstructorPriceService
    {
        private readonly IInstructorPriceRepository instructorPriceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public InstructorPriceService(
            IInstructorPriceRepository instructorPriceRepository,
            IMapper mapper,
            IUserRepository userRepository
        )
        {
            this.instructorPriceRepository = instructorPriceRepository;
            this._mapper = mapper;
            this._userRepository = userRepository;
        }

        public async Task<List<InstructorPriceResponseDto>> GetAllAsync()
        {
            var instructorPrices = await instructorPriceRepository.GetAllAsync();
            return _mapper.Map<List<InstructorPriceResponseDto>>(instructorPrices);
        }

        public async Task<InstructorPriceResponseDto?> GetByIdAsync(int id)
        {
            var instructorPrice = await instructorPriceRepository.GetByIdAsync(id);
            return instructorPrice == null
                ? null
                : _mapper.Map<InstructorPriceResponseDto>(instructorPrice);
        }

        public async Task<InstructorPriceResponseDto> AddAsync(CreateInstructorPriceDto dto)
        {
            // Check if the user exists
            var user = await _userRepository.GetByIdAsync(dto.InstructorId);
            if (user == null || user.Role != UserRole.Instructor)
                throw new ArgumentException("Invalid instructor ID");

            var instructorPrice = _mapper.Map<InstructorPrice>(dto);
            await instructorPriceRepository.AddAsync(instructorPrice);
            return _mapper.Map<InstructorPriceResponseDto>(instructorPrice);
        }

        public async Task<InstructorPriceResponseDto?> UpdateAsync(UpdateInstructorPriceDto dto)
        {
            var existing = await instructorPriceRepository.GetByIdAsync(dto.InstructorPriceId);
            if (existing == null)
                return null;

            var user = await _userRepository.GetByIdAsync(dto.InstructorId);
            if (user == null || user.Role != UserRole.Instructor)
                throw new ArgumentException("Invalid instructor ID");

            _mapper.Map(dto, existing);

            await instructorPriceRepository.UpdateAsync(existing);

            return _mapper.Map<InstructorPriceResponseDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await instructorPriceRepository.GetByIdAsync(id);
            if (existing == null)
                return false;
            await instructorPriceRepository.DeleteAsync(id);

            return true;
        }
    }
}
