using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Repositories
{
    public interface IStudentAccountRepository
    {
        Task<List<StudentAccount>> GetAllAsync();
        Task<StudentAccount?> GetByIdAsync(int id);
        Task AddAsync(StudentAccount account);
        Task UpdateAsync(StudentAccount account);
        Task DeleteAsync(int id);
    }
}
