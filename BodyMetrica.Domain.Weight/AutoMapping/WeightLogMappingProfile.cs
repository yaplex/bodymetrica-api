using AutoMapper;
using BodyMetrica.Contracts.Weight.Dtos;
using BodyMetrica.Core.Models;
using BodyMetrica.Domain.Weight.Entities;
using BodyMetrica.Domain.Weight.Features.GetUserProfile;

namespace BodyMetrica.Domain.Weight.AutoMapping;

public class WeightLogMappingProfile : Profile
{
    public WeightLogMappingProfile()
    {
        CreateMap<WeightLog, WeightLogDto>();
        CreateMap<User, UserProfileResponse>();
    }
}