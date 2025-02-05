using AutoMapper;
using API.Dtos;
using API.Models;

namespace API.Profiles
{
    public class UserCareTeamProfile : Profile
    {
        public UserCareTeamProfile()
        {
            CreateMap<UserCareTeam, UserCareTeamReadDto>();
            CreateMap<UserCareTeamCreateDto, UserCareTeam>();
            CreateMap<UserCareTeamUpdateDto, UserCareTeam>();
            CreateMap<UserCareTeam, UserCareTeamUpdateDto>();
        }
    }
}