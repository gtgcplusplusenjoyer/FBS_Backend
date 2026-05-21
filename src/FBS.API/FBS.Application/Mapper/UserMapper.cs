using AutoMapper;
using FBS.Application.Dto.User;
using FBS.Core.Entities.User;

namespace FBS.Application.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.UserRole.ToString()));
        }
    }
}
