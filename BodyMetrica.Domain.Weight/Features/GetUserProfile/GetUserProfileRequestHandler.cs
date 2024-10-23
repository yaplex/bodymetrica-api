using AutoMapper;
using BodyMetrica.Core.Repositories;
using BodyMetrica.Core.Services;
using MediatR;

namespace BodyMetrica.Domain.Weight.Features.GetUserProfile;

public class GetUserProfileRequestHandler(IUserService userService, IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserProfileRequest, UserProfileResponse>
{
    public async Task<UserProfileResponse> Handle(GetUserProfileRequest request, CancellationToken cancellationToken)
    {
        var user = await userService.GetCurrentUser();
        var userFromRepo = await userRepository.GetById(user.Id);
        return mapper.Map<UserProfileResponse>(userFromRepo);
    }
}