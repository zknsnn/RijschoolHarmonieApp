using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RijschoolHarmonieApp.DTOs.Payment;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Repositories;

namespace RijschoolHarmonieApp.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IStudentAccountRepository _studentAccountRepository;
        private readonly IMapper _mapper;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IStudentAccountRepository studentAccountRepository,
            IMapper mapper
        )
        {
            _paymentRepository = paymentRepository;
            _studentAccountRepository = studentAccountRepository;
            _mapper = mapper;
        }

        public async Task<List<PaymentResponseDto>> GetAllAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return _mapper.Map<List<PaymentResponseDto>>(payments);
        }

        public async Task<PaymentResponseDto?> GetByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                return null;
            return _mapper.Map<PaymentResponseDto>(payment);
        }

        public async Task<PaymentResponseDto> AddPaymentAsync(CreatePaymentDto dto)
        {
            // Check student
            var studentAccount = await _studentAccountRepository.GetByIdAsync(dto.StudentAccountId);
            if (studentAccount == null)
                throw new ArgumentException("Student account does not exist.");

            // Check balance

            if (dto.Amount > studentAccount.Balance)
                throw new ArgumentException(
                    $"Payment exceeds the student's debt.balance {studentAccount.Balance}"
                );

            // DTO -> Entity
            var paymentEntity = _mapper.Map<Payment>(dto);

            // Repo'ya ekle
            paymentEntity = await _paymentRepository.AddAsync(paymentEntity);

            // StudentAccount güncelle
            studentAccount.TotalCredit += paymentEntity.Amount;
            await _studentAccountRepository.UpdateAsync(studentAccount);

            // Response DTO
            return _mapper.Map<PaymentResponseDto>(paymentEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                return false;

            // StudentAccount
            var studentAccount = await _studentAccountRepository.GetByIdAsync(
                payment.StudentAccountId
            );
            if (studentAccount != null)
            {
                studentAccount.TotalCredit -= payment.Amount;
                await _studentAccountRepository.UpdateAsync(studentAccount);
            }

            await _paymentRepository.DeleteAsync(id);
            return true;
        }
    }
}
