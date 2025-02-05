using AutoMapper;
using API.Dtos;
using API.Models;

namespace API.Profiles
{
    public class CareTeamProfile : Profile
    {
        public CareTeamProfile()
        {
            // source -> target
            CreateMap<CareTeam, CareTeamReadDto>();
            CreateMap<CareTeamCreateDto, CareTeam>();
            CreateMap<CareTeamUpdateDto, CareTeam>();
            CreateMap<CareTeam, CareTeamUpdateDto>();
        }
    }
}