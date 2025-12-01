using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Services

{
    public interface IInstructorPriceService
    {
        Task<List<InstructorPrice>> GetAllAsync();
        Task<InstructorPrice?> GetByIdAsync(int id);
        Task AddAsync(InstructorPrice price);
        Task UpdateAsync(InstructorPrice price);
        Task DeleteAsync(int id);
    }
}