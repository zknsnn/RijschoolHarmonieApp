using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using RijschoolHarmonieApp.DTOs.Appointment;
using RijschoolHarmonieApp.Models;
using RijschoolHarmonieApp.Repositories;

namespace RijschoolHarmonieApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IInstructorPriceRepository _priceRepo;
        private readonly IUserRepository _userRepo;
        private readonly IStudentAccountRepository _accountRepo;
        private readonly IMapper _mapper;

        public AppointmentService(
            IAppointmentRepository appointmentRepo,
            IInstructorPriceRepository priceRepo,
            IUserRepository userRepository,
            IStudentAccountRepository accountRepo,
            IMapper mapper
        )
        {
            _appointmentRepo = appointmentRepo;
            _priceRepo = priceRepo;
            _userRepo = userRepository;
            _accountRepo = accountRepo;
            _mapper = mapper;
        }

        public async Task<List<AppointmentResponseDto>> GetAllAsync()
        {
            var appointments = await _appointmentRepo.GetAllAsync();
            return _mapper.Map<List<AppointmentResponseDto>>(appointments);
        }

        public async Task<AppointmentResponseDto?> GetByIdAsync(int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if (appointment == null)
                return null;
            return _mapper.Map<AppointmentResponseDto>(appointment);
        }

        public async Task<List<AppointmentResponseDto>> GetByInstructorAsync(
            int instructorId,
            DateTime? start = null,
            DateTime? end = null
        )
        {
            var appointments = await _appointmentRepo.GetByInstructorAsync(
                instructorId,
                start,
                end
            );
            return _mapper.Map<List<AppointmentResponseDto>>(appointments);
        }

        public async Task<List<AppointmentResponseDto>> GetByStudentAsync(
            int studentId,
            DateTime? start = null,
            DateTime? end = null
        )
        {
            var appointments = await _appointmentRepo.GetByStudentAsync(studentId, start, end);
            return _mapper.Map<List<AppointmentResponseDto>>(appointments);
        }

        public async Task<List<AppointmentResponseDto>> GetFilteredAsync(
            int? instructorId,
            int? studentId,
            DateTime? start,
            DateTime? end,
            AppointmentType? type
        )
        {
            var appointments = await _appointmentRepo.GetFilteredAsync(
                instructorId,
                studentId,
                start,
                end,
                type
            );
            return _mapper.Map<List<AppointmentResponseDto>>(appointments);
        }

        public async Task<AppointmentResponseDto?> UpdateAsync(UpdateAppointmentDto dto)
        {
            var existing = await _appointmentRepo.GetByIdAsync(dto.AppointmentId);
            if (existing == null)
                return null;

            _mapper.Map(dto, existing);

            if (await HasConflict(existing, ignoreId: existing.AppointmentId))
                throw new Exception("Instructor has another appointment in this range.");

            decimal oldPrice = existing.Price;
            existing.Price = await CalculatePrice(existing);

            var updated = await _appointmentRepo.UpdateAsync(existing);

            decimal diff = updated.Price - oldPrice;
            if (diff != 0)
                await AddDebitToStudentAccount(updated.StudentId, diff);

            return _mapper.Map<AppointmentResponseDto>(updated);
        }

        public async Task<AppointmentResponseDto> CreateAsync(CreateAppointmentDto dto)
        {
            var appt = _mapper.Map<Appointment>(dto);

            if (await HasConflict(appt))
                throw new Exception("Instructor already has an appointment in this time range.");

            appt.Price = await CalculatePrice(appt);

            appt = await _appointmentRepo.AddAsync(appt);

            await AddDebitToStudentAccount(appt.StudentId, appt.Price);

            return _mapper.Map<AppointmentResponseDto>(appt);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if (appointment == null)
                return false;

            // student account
            var studentAccount = await _accountRepo.GetByStudentIdAsync(appointment.StudentId);
            if (studentAccount != null)
            {
                studentAccount.TotalCredit -= appointment.Price;
                await _accountRepo.UpdateAsync(studentAccount);
            }
            await _appointmentRepo.DeleteAsync(id);
            return true;
        }

        // =============================================
        // PRIVATE HELPERS
        // =============================================
        private async Task<bool> HasConflict(Appointment appt, int? ignoreId = null)
        {
            var list = await _appointmentRepo.GetByInstructorAsync(appt.InstructorId);

            return list.Any(a =>
                (ignoreId == null || a.AppointmentId != ignoreId)
                && a.StartTime < appt.EndTime
                && appt.StartTime < a.EndTime
            );
        }

        private async Task<decimal> CalculatePrice(Appointment appt)
        {
            var price = await _priceRepo.GetByInstructorAsync(appt.InstructorId);
            if (price == null)
                throw new Exception("Instructor price not set.");

            if (appt.Type == AppointmentType.Exam)
                return price.ExamPrice;

            double durationHours = (appt.EndTime - appt.StartTime).TotalHours;
            return Math.Round(price.LessonPrice * (decimal)durationHours, 2);
        }

        private async Task AddDebitToStudentAccount(int studentId, decimal amount)
        {
            var account = await _accountRepo.GetByStudentIdAsync(studentId);
            if (account == null)
            {
                account = new StudentAccount { StudentId = studentId, TotalDebit = amount };
                await _accountRepo.AddAsync(account);
            }
            else
            {
                account.TotalDebit += amount;
                await _accountRepo.UpdateAsync(account);
            }
        }
    }
}
