using AutoMapper;
using BodyMetrica.Domain.Weight.Entities;
using BodyMetrica.Infrastructure.DataAccess.Weight.Records;

namespace BodyMetrica.Infrastructure.DataAccess.Weight.AutoMapping;

public class WeightLogMappingProfile : Profile
{
    public WeightLogMappingProfile()
    {
        CreateMap<WeightLog, WeightLogRecord>();
        CreateMap<WeightLogRecord, WeightLog>();
    }
}