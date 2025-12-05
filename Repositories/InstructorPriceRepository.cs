using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Data;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Repositories
{
    public class InstructorPriceRepository : IInstructorPriceRepository
    {
        private readonly RijschoolHarmonieAppContext dbHarmonie;

        public InstructorPriceRepository(RijschoolHarmonieAppContext dbHarmonie)
        {
            this.dbHarmonie = dbHarmonie;
        }

        public async Task<List<InstructorPrice>> GetAllAsync()
        {
            return await dbHarmonie.InstructorPrices.ToListAsync();
        }

        public async Task<InstructorPrice?> GetByIdAsync(int id)
        {
            return await dbHarmonie.InstructorPrices.FindAsync(id);
        }

        public async Task<InstructorPrice?> GetByInstructorAsync(int instructorId)
        {
            return await dbHarmonie.InstructorPrices.FirstOrDefaultAsync(p =>
                p.InstructorId == instructorId
            );
        }

        public async Task AddAsync(InstructorPrice price)
        {
            dbHarmonie.InstructorPrices.Add(price);
            await dbHarmonie.SaveChangesAsync();
        }

        public async Task UpdateAsync(InstructorPrice price)
        {
            dbHarmonie.InstructorPrices.Update(price);
            await dbHarmonie.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var price = await dbHarmonie.InstructorPrices.FindAsync(id);
            if (price != null)
            {
                dbHarmonie.InstructorPrices.Remove(price);
                await dbHarmonie.SaveChangesAsync();
            }
        }
    }
}
