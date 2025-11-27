using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Repositories;

namespace RijschoolHarmonieApp.Services
{
    public class InstructorPriceService : IInstructorPriceService
    {
        private readonly IInstructorPriceRepository instructorPriceRepository;

        public InstructorPriceService(IInstructorPriceRepository instructorPriceRepository)
        {
            this.instructorPriceRepository = instructorPriceRepository;
        }

        public async Task<List<InstructorPrice>> GetAllAsync()
        {
            return await instructorPriceRepository.GetAllAsync();
        }

        public async Task<InstructorPrice?> GetByIdAsync(int id)
        {
            return await instructorPriceRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(InstructorPrice price)
        {
            await instructorPriceRepository.AddAsync(price);
        }

        public async Task UpdateAsync(InstructorPrice price)
        {
            var existingPrice = await instructorPriceRepository.GetByIdAsync(price.InstructorId);

            if (existingPrice == null)
                throw new KeyNotFoundException("Price not found");

            existingPrice.InstructorId = price.InstructorId;
            existingPrice.LessonPrice = price.LessonPrice;
            existingPrice.ExamPrice = price.ExamPrice;
            existingPrice.LastUpdateDate = price.LastUpdateDate;

            await instructorPriceRepository.UpdateAsync(existingPrice);
        }

        public async Task DeleteAsync(int id)
        {
            var existingPrice = await instructorPriceRepository.GetByIdAsync(id);
            if (existingPrice == null)
                throw new KeyNotFoundException("Price not found");

            await instructorPriceRepository.DeleteAsync(id);
        }
    }
}
