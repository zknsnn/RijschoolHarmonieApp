using AutoMapper;
using RijschoolHarmonieApp.DTOs.Appointment;
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
        CreateMap<Appointment, AppointmentResponseDto>()
            .ForMember(
                dest => dest.InstructorName,
                opt => opt.MapFrom(src => src.Instructor.FirstName + " " + src.Instructor.LastName)
            )
            .ForMember(
                dest => dest.StudentName,
                opt => opt.MapFrom(src => src.Student.FirstName + " " + src.Student.LastName)
            );
    }
}
