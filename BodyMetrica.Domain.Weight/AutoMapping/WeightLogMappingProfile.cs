using AutoMapper;
using BodyMetrica.Contracts.Weight.Dtos;
using BodyMetrica.Domain.Weight.Entities;

namespace BodyMetrica.Domain.Weight.AutoMapping;

public class WeightLogMappingProfile : Profile
{
    public WeightLogMappingProfile()
    {
        CreateMap<WeightLog, WeightLogDto>();
    }
}