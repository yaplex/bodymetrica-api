using AutoMapper;
using BodyMetrica.Features.Persistence;
using BodyMetrica.Features.User.GetUserProfile;
using BodyMetrica.Features.Weight.GetRecentWeightLogs;

namespace BodyMetrica.Features.Weight;

public class WeightMappingProfile : Profile
{
    public WeightMappingProfile()
    {
        CreateMap<WeightLogRecord, RecentWeightLogDto>();
    }
}