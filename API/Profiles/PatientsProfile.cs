using AutoMapper;
using API.Dtos;
using API.Models;

namespace API.Profiles
{
    public class PatientProfiles : Profile
    {
        public PatientProfiles()
        {
            CreateMap<Patient, PatientReadDto>();
            CreateMap<PatientCreateDto, Patient>();
            CreateMap<PatientUpdateDto, Patient>();
            CreateMap<Patient, PatientUpdateDto>();
        }
    }
}