using System.Security;
using System.Security.Claims;
using BodyMetrica.Domain.Common.Models;
using BodyMetrica.Domain.Common.Repositories;
using BodyMetrica.Domain.Weight;
using BodyMetrica.Domain.Weight.Services;
using Microsoft.AspNetCore.Http;

namespace BodyMetrica.Infrastructure;

public class UserService(IUserRepository userRepo, IHttpContextAccessor contextAccessor) : IUserService
{
    public UserProfile GetCurrentUser()
    {
        var authInfo = GetAuthInfo();
        var user = userRepo.FindByExternalId(authInfo.ExternalIdentifer);
        return user;
    }

    public Auth0LoginInfo GetAuthInfo()
    {
        var user = contextAccessor.HttpContext?.User;
        if (user is { Identity.IsAuthenticated: true })
        {
            var sub = user.FindFirstValue("sub")!;
            return new Auth0LoginInfo
            {
                ExternalIdentifer = sub
            };
        }

        throw new SecurityException("Unauthenticated user");
    }
}