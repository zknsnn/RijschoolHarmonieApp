using AutoMapper;
using RijschoolHarmonieApp.DTOs.User;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Create DTO → Entity
            CreateMap<CreateUserDto, User>();

            // Update DTO → Entity (sadece null olmayanlar güncellenir)
            CreateMap<UpdateUserDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Entity → Response DTO
            CreateMap<User, UserResponseDto>();
        }
    }
}
