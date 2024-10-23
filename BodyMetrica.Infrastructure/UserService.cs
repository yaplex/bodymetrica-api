using System.Security;
using BodyMetrica.Core.Models;
using BodyMetrica.Core.Repositories;
using BodyMetrica.Core.Services;
using BodyMetrica.Domain.Common.Models;
using Microsoft.AspNetCore.Http;

namespace BodyMetrica.Infrastructure;

public class UserService(IUserRepository userRepo, IHttpContextAccessor contextAccessor) : IUserService
{
    public async Task<User> GetCurrentUser()
    {
        // todo: add caching
        var authInfo = GetAuthInfo();
        var user = await userRepo.FindByExternalId(authInfo.ExternalIdentifer);

        if (user == null)
        {
            // this is first time user logs in or created account, need to create a record on backend.
            var defaultUser = new User
            {
                ExternalId = authInfo.ExternalIdentifer,
                CreatedAt = DateTimeOffset.Now,
                WeightUnits = "kg"
            };
            return await userRepo.Create(defaultUser);
        }

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