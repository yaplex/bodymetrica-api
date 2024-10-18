using System.Security;
using BodyMetrica.Domain.Common.Models;
using BodyMetrica.Domain.Common.Repositories;
using BodyMetrica.Domain.Common.Services;
using Microsoft.AspNetCore.Http;

namespace BodyMetrica.Infrastructure;

public class UserService(IUserRepository userRepo, IHttpContextAccessor contextAccessor) : IUserService
{
    public User GetCurrentUser()
    {
        // todo: add caching
        var authInfo = GetAuthInfo();
        var user = userRepo.FindByExternalId(authInfo.ExternalIdentifer);
        return user;
    }

    private Auth0LoginInfo GetAuthInfo()
    {
        var user = contextAccessor.HttpContext?.User;
        if (user is { Identity.IsAuthenticated: true })
        {
            var sub = user.FindFirst("sub")?.Value;
            if (sub != null)
                return new Auth0LoginInfo
                {
                    ExternalIdentifer = sub
                };
        }

        throw new SecurityException("Unauthenticated user");
    }
}