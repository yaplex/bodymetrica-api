using AutoMapper;
using BodyMetrica.Domain.Weight.Dtos;
using BodyMetrica.Domain.Weight.Persistence;
using BodyMetrica.Domain.Weight.Requests;

namespace BodyMetrica.Domain.Weight.Mapping;

public class WeightMappingProfile: Profile
{
    public WeightMappingProfile()
    {
        CreateMap<AddNewLogWeightRequest, WeightLogRecord>();
        CreateMap<WeightLogRecord, WeightLogDto>();
    }
}