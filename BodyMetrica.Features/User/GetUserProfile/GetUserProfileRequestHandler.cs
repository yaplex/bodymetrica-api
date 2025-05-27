using AutoMapper;
using BodyMetrica.Features.Common;
using MediatR;

namespace BodyMetrica.Features.User.GetUserProfile;

public class GetUserProfileRequestHandler(IUserService userService, IMapper mapper) : IRequestHandler<GetUserProfileRequest, UserProfileResponse>
{
    public async Task<UserProfileResponse> Handle(GetUserProfileRequest request, CancellationToken cancellationToken)
    {
        var user = await userService.GetLoggedInUser();
        return mapper.Map<UserProfileResponse>(user);
    }
}