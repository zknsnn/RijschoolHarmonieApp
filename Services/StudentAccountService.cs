using AutoMapper;
using RijschoolHarmonieApp.DTOs;
using RijschoolHarmonieApp.DTOs.StudentAccount;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Repositories;

namespace RijschoolHarmonieApp.Services
{
    public class StudentAccountService : IStudentAccountService
    {
        private readonly IStudentAccountRepository _repo;
        private readonly IMapper _mapper;

        public StudentAccountService(IStudentAccountRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<StudentAccountResponseDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<StudentAccountResponseDto>>(entities);
        }

        public async Task<StudentAccountResponseDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return null;

            return _mapper.Map<StudentAccountResponseDto>(entity);
        }

        public async Task<StudentAccountResponseDto?> GetByStudentIdAsync(int id)
        {
            var entity = await _repo.GetByStudentIdAsync(id);
            if (entity == null)
                return null;

            return _mapper.Map<StudentAccountResponseDto>(entity);
        }

        public async Task<StudentAccountResponseDto> AddAsync(CreateStudentAccountDto dto)
        {
            // check student accounts
            var allAccounts = await _repo.GetAllAsync();
            if (allAccounts.Any(a => a.StudentId == dto.StudentId))
            {
                throw new ArgumentException("An account for this student already exists.");
            }

            var entity = _mapper.Map<StudentAccount>(dto);
            entity = await _repo.AddAsync(entity);

            return _mapper.Map<StudentAccountResponseDto>(entity);
        }

        public async Task<StudentAccountResponseDto?> UpdateAsync(UpdateStudentAccountDto dto)
        {
            var entity = await _repo.GetByIdAsync(dto.StudentAccountId);
            if (entity == null)
                return null;

            _mapper.Map(dto, entity); // null olmayan alanlar güncellenecek
            entity = await _repo.UpdateAsync(entity);

            return _mapper.Map<StudentAccountResponseDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

    }
}
