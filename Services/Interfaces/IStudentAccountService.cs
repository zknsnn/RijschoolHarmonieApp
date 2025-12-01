using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Services

{
    public interface IStudentAccountService
    {
        Task<List<StudentAccount>> GetAllAsync();
        Task<StudentAccount?> GetByIdAsync(int id);
        Task AddAsync(StudentAccount account);
        Task UpdateAsync(StudentAccount account);
        Task DeleteAsync(int id);
    }
}