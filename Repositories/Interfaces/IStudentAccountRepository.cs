using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Repositories
{
    public interface IStudentAccountRepository
    {
        Task<List<StudentAccount>> GetAllAsync();
        Task<StudentAccount?> GetByIdAsync(int id);
        Task<StudentAccount?> GetByStudentIdAsync(int studentId);
        Task<StudentAccount> AddAsync(StudentAccount account);
        Task<StudentAccount> UpdateAsync(StudentAccount account);
        Task<bool> DeleteAsync(int id);
    }
}
