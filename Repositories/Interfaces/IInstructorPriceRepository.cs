using RijschoolHarmonieApp.Models;


namespace RijschoolHarmonieApp.Repositories
{
    public interface IInstructorPriceRepository
    {
        Task<List<InstructorPrice>> GetAllAsync();
        Task<InstructorPrice?> GetByIdAsync(int id);
        Task AddAsync(InstructorPrice price);
        Task UpdateAsync(InstructorPrice price);
        Task DeleteAsync(int id);
    }
}
