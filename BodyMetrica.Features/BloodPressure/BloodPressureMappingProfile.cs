using AutoMapper;
using BodyMetrica.Features.BloodPressure.GetRecentBloodPressureLogs;
using BodyMetrica.Features.Persistence;

namespace BodyMetrica.Features.BloodPressure;

public class BloodPressureMappingProfile : Profile
{
    public BloodPressureMappingProfile()
    {
        CreateMap<BloodPressureLogRecord, RecentBloodPressureLogDto>();
    }
}