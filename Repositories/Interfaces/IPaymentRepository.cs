using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Repositories
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment> AddAsync(Payment payment);
        Task DeleteAsync(int id);
    }
}
