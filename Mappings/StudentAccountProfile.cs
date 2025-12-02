using AutoMapper;
using RijschoolHarmonieApp.DTOs.StudentAccount;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Mappings
{
    public class StudentAccountProfile : Profile
    {
        public StudentAccountProfile()
        {
            // Create DTO → Entity
            CreateMap<CreateStudentAccountDto, StudentAccount>();

            // Update DTO → Entity (sadece null olmayanlar güncellenir)
            CreateMap<UpdateStudentAccountDto, StudentAccount>()
                .ForMember(dest => dest.StudentAccountId, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Entity → Response DTO
            CreateMap<StudentAccount, StudentAccountResponseDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FirstName))
                .ForMember(
                    dest => dest.StudentAchternaam,
                    opt => opt.MapFrom(src => src.Student.LastName)
                );
        }
    }
}
