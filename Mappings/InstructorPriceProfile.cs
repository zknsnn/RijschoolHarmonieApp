using AutoMapper;
using RijschoolHarmonieApp.DTOs.InstructorPrice;
using RijschoolHarmonieApp.DTOs.User;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Mappings
{
    public class InstructorPriceProfile : Profile
    {
        public InstructorPriceProfile()
        {
            // Create DTO → Entity
            CreateMap<CreateInstructorPriceDto, InstructorPrice>();

            // Update DTO → Entity (sadece null olmayanlar güncellenir)
            CreateMap<UpdateInstructorPriceDto, InstructorPrice>()
                .ForMember(dest => dest.InstructorPriceId, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Entity → Response DTO
            CreateMap<InstructorPrice, InstructorPriceResponseDto>();
        }
    }
}
