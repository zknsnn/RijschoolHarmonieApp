using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Data;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Repositories
{
    public class StudentAccountRepository : IStudentAccountRepository
    {
        private readonly RijschoolHarmonieAppContext dbHarmonie;

        public StudentAccountRepository(RijschoolHarmonieAppContext dbHarmonie)
        {
            this.dbHarmonie = dbHarmonie;
        }

        public async Task<List<StudentAccount>> GetAllAsync()
        {
            return await dbHarmonie.StudentAccounts.ToListAsync();
        }

        public async Task<StudentAccount?> GetByIdAsync(int id)
        {
            return await dbHarmonie.StudentAccounts.FindAsync(id);
        }

        public async Task AddAsync(StudentAccount account)
        {
            dbHarmonie.StudentAccounts.Add(account);
            await dbHarmonie.SaveChangesAsync();
        }

        public async Task UpdateAsync(StudentAccount account)
        {
            dbHarmonie.StudentAccounts.Add(account);
            await dbHarmonie.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var account = await dbHarmonie.StudentAccounts.FindAsync(id);
            if (account != null)
            {
                dbHarmonie.StudentAccounts.Remove(account);
                await dbHarmonie.SaveChangesAsync();
            }
        }
    }
}
