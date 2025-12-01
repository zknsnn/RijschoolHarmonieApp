using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Repositories;

namespace RijschoolHarmonieApp.Services
{
    public class StudentAccountService : IStudentAccountService
    {
        private readonly IStudentAccountRepository studentAccountRepository;

        public StudentAccountService(IStudentAccountRepository _studentAccountRepo)
        {
            studentAccountRepository = _studentAccountRepo;
        }

        public async Task<List<StudentAccount>> GetAllAsync()
        {
            return await studentAccountRepository.GetAllAsync();
        }

        public async Task<StudentAccount?> GetByIdAsync(int id)
        {
            return await studentAccountRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(StudentAccount account)
        {
            await studentAccountRepository.AddAsync(account);
        }

        public async Task UpdateAsync(StudentAccount account)
        {
            var existingAccount = await studentAccountRepository.GetByIdAsync(account.StudentAccountId);

            if (existingAccount == null)
                throw new KeyNotFoundException("Student account not found");

            existingAccount.StudentId = account.StudentId;
            existingAccount.TotalCredit = account.TotalCredit;
            existingAccount.TotalDebit = account.TotalDebit;

            await studentAccountRepository.UpdateAsync(existingAccount);
        }

        public async Task DeleteAsync(int id)
        {
            var existingAccount = await studentAccountRepository.GetByIdAsync(id);
            if (existingAccount == null)
                throw new KeyNotFoundException("Student account not found");

            await studentAccountRepository.DeleteAsync(id);
        }
    }
}
