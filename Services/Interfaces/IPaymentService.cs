using RijschoolHarmonieApp.DTOs;
using RijschoolHarmonieApp.DTOs.Payment;
using RijschoolHarmonieApp.DTOs.StudentAccount;

namespace RijschoolHarmonieApp.Services
{
    public interface IPaymentService
    {
        Task<List<PaymentResponseDto>> GetAllAsync();
        Task<PaymentResponseDto?> GetByIdAsync(int id);

        Task<List<PaymentResponseDto?>> GetByStudentIdAsync(int studentId);
        Task<PaymentResponseDto> AddPaymentAsync(CreatePaymentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
