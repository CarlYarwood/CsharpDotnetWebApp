using AutoMapper;
using API.Dtos;
using API.Models;

namespace API.Profiles
{
    public class RecommendationProfile : Profile
    {
        public RecommendationProfile()
        {
            CreateMap<Recommendation, RecommendationReadDto>();
            CreateMap<RecommendationCreateDto, Recommendation>();
            CreateMap<RecommendationUpdateDto, Recommendation>();
            CreateMap<Recommendation, RecommendationUpdateDto>();
        }
    }
}