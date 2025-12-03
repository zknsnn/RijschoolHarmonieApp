using AutoMapper;
using RijschoolHarmonieApp.DTOs.Payment;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            // Create DTO → Entity
            CreateMap<CreatePaymentDto, Payment>();

            // Entity → Response DTO
            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(
                    dest => dest.StudentName,
                    opt => opt.MapFrom(src => src.StudentAccount.Student.FirstName)
                )
                .ForMember(
                    dest => dest.StudentLastName,
                    opt => opt.MapFrom(src => src.StudentAccount.Student.LastName)
                );
        }
    }
}
