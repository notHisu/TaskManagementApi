using AutoMapper;
using TaskManagementApi.Models;
namespace TaskManagementApi.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper() { 
            CreateMap<User, UserResponseDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
