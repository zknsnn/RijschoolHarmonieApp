using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Data;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RijschoolHarmonieAppContext dbHarmonie;

        public PaymentRepository(RijschoolHarmonieAppContext context)
        {
            dbHarmonie = context;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await dbHarmonie.Payments.Include(p => p.Student).ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await dbHarmonie
                .Payments.Include(p => p.Student)
                .FirstOrDefaultAsync(p => p.PaymentId == id);
        }

        public async Task<Payment> AddAsync(Payment payment)
        {
            dbHarmonie.Payments.Add(payment);
            await dbHarmonie.SaveChangesAsync();
            return payment;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await dbHarmonie.Users.FindAsync(id);
            if (user != null)
            {
                dbHarmonie.Users.Remove(user);
                await dbHarmonie.SaveChangesAsync();
            }
        }
    }
}
