using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Data;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RijschoolHarmonieAppContext dbHarmonie;

        public UserRepository(RijschoolHarmonieAppContext context)
        {
            dbHarmonie = context;
        }

        // public async Task<List<User>> GetAllAsync() => await dbHarmonie.Users.ToListAsync();

        public async Task<List<User>> GetAllAsync()
        {
            return await dbHarmonie.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await dbHarmonie.Users.FindAsync(id);
        }

        public async Task AddAsync(User user)
        {
            dbHarmonie.Users.Add(user);
            await dbHarmonie.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            dbHarmonie.Users.Update(user);
            await dbHarmonie.SaveChangesAsync();
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
