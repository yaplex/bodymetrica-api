using AutoMapper;
using BodyMetrica.Features.Persistence;
using BodyMetrica.Features.User.GetUserProfile;

namespace BodyMetrica.Features.Common;

public class CommonMappingProfile : Profile
{
    public CommonMappingProfile()
    {
        CreateMap<BodyMetricaUserRecord, UserProfileResponse>();
    }
}