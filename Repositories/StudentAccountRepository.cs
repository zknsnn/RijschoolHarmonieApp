using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Data;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Repositories
{
    public class StudentAccountRepository : IStudentAccountRepository
    {
        private readonly RijschoolHarmonieAppContext _dbHarmonie;

        public StudentAccountRepository(RijschoolHarmonieAppContext dbHarmonie)
        {
            _dbHarmonie = dbHarmonie;
        }

        // Tüm hesapları getir, Student bilgilerini Include et
        public async Task<List<StudentAccount>> GetAllAsync()
        {
            return await _dbHarmonie.StudentAccounts.Include(sa => sa.Student).ToListAsync();
        }

        // Tek hesap getir, Student bilgisi dahil
        public async Task<StudentAccount?> GetByIdAsync(int id)
        {
            return await _dbHarmonie
                .StudentAccounts.Include(sa => sa.Student)
                .FirstOrDefaultAsync(sa => sa.StudentAccountId == id);
        }

        public async Task<StudentAccount?> GetByStudentIdAsync(int studentId)
        {
            return await _dbHarmonie
                .StudentAccounts.Include(sa => sa.Student)
                .SingleOrDefaultAsync(sa => sa.StudentId == studentId);
        }

        // Yeni hesap ekle
        public async Task<StudentAccount> AddAsync(StudentAccount account)
        {
            _dbHarmonie.StudentAccounts.Add(account);
            await _dbHarmonie.SaveChangesAsync();

            // Student navigation property yükle
            await _dbHarmonie.Entry(account).Reference(sa => sa.Student).LoadAsync();

            return account;
        }

        // Hesap güncelle
        public async Task<StudentAccount> UpdateAsync(StudentAccount account)
        {
            _dbHarmonie.StudentAccounts.Update(account);
            await _dbHarmonie.SaveChangesAsync();
            return account;
        }

        // Hesap sil
        public async Task<bool> DeleteAsync(int id)
        {
            var account = await _dbHarmonie.StudentAccounts.FindAsync(id);
            if (account == null)
                return false;

            _dbHarmonie.StudentAccounts.Remove(account);
            await _dbHarmonie.SaveChangesAsync();
            return true;
        }
    }
}
