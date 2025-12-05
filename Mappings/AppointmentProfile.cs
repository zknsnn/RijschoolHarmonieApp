using AutoMapper;
using RijschoolHarmonieApp.DTOs.InstructorPrice;
using RijschoolHarmonieApp.Models;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateMap<CreateAppointmentDto, Appointment>();

        // Update DTO → Entity (sadece null olmayan alanları güncelle)
        CreateMap<UpdateAppointmentDto, Appointment>()
            .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

        // Entity → Response DTO
        CreateMap<Appointment, AppointmentResponseDto>();
    }
}
