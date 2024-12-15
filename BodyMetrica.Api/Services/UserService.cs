using System.Net.Http.Headers;
using System.Security;
using BodyMetrica.Core.Models;
using BodyMetrica.Core.Repositories;
using BodyMetrica.Core.Services;
using ILogger = Serilog.ILogger;

namespace BodyMetrica.Api.Services;

public class UserService(IUserRepository userRepo, IHttpContextAccessor contextAccessor, HttpClient http, ILogger logger) : IUserService
{
    public async Task<User> GetCurrentUser()
    {
        // todo: add caching
        var externalUserId = GetCurrentUserSub();
        var user = await userRepo.FindByExternalId(externalUserId);

        if (user == null)
        {
            // this is first time user logs in or created account, need to create a record on backend.

            var auth0Info = await GetAuth0UserInfo();
            if (null == auth0Info)
            {
                throw new SecurityException("Failed to get user information from Auth0");
            }

            var newUser = new User
            {
                ExternalId = auth0Info.sub,
                Name = auth0Info.name,
                Picture = auth0Info.picture,
                Email = auth0Info.email,
                EmailVerified = auth0Info.email_verified,
                CreatedAt = DateTimeOffset.Now,
                
                WeightUnits = "kg"
            };
            return await userRepo.Create(newUser);
        }

        return user;
    }

    private async Task<Auth0LoginInfo?> GetAuth0UserInfo()
    {
        try
        {
            contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
            if (!string.IsNullOrWhiteSpace(token))
            {
                token = token.ToString().Replace("Bearer ", "");
            }

            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var auth0Info = await http.GetFromJsonAsync<Auth0LoginInfo>("https://auth.bodymetrica.com/userinfo");
            return auth0Info;
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Failed to call Auth0 for user information");
        }
        return null;
    }

    private string GetCurrentUserSub()
    {
        var user = contextAccessor.HttpContext?.User;
        if (user is { Identity.IsAuthenticated: true })
        {
            var sub = user.FindFirst("sub")?.Value;
            if (sub != null)
                return sub;
        }

        throw new SecurityException("Unauthenticated user");
    }
}