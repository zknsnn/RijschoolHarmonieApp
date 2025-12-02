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
            var studentAccount = await _studentAccountRepository.GetByIdAsync(dto.StudentId);
            if (studentAccount == null)
                throw new ArgumentException("Student account does not exist.");

            // Check balance
            var balance = studentAccount.TotalCredit - studentAccount.TotalDebit;
            if (dto.Amount > balance)
                throw new ArgumentException("Payment exceeds the student's debt.");

            // DTO -> Entity
            var paymentEntity = _mapper.Map<Payment>(dto);

            //add repo
            paymentEntity = await _paymentRepository.AddAsync(paymentEntity);

            // update student account
            studentAccount.TotalCredit += paymentEntity.Amount;
            await _studentAccountRepository.UpdateAsync(studentAccount);

            var response = _mapper.Map<PaymentResponseDto>(paymentEntity);
            return response;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                return false;

            // StudentAccount 
            var studentAccount = await _studentAccountRepository.GetByIdAsync(payment.StudentId);
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
