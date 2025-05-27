using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace BodyMetrica.Features.Common;

public interface IAuth0
{
    string GetLoggedInUserId();
    Task<Auth0UserInformation> GetUser();
}

public class Auth0(IHttpContextAccessor contextAccessor, ILogger logger, HttpClient http) : IAuth0
{
    public string GetLoggedInUserId()
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

    public async Task<Auth0UserInformation> GetUser()
    {
        try
        {
            var token = GetBearerTokenFromRequest();
            if (!string.IsNullOrWhiteSpace(token))
            {
                http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                var auth0Info =
                    await http.GetFromJsonAsync<Auth0UserInformation>("https://auth.bodymetrica.com/userinfo");
                return auth0Info!;
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Failed to call Auth0 for user information");
        }

        throw new SecurityException("Failed to get user information from Auth0");
    }

    private string? GetBearerTokenFromRequest()
    {
        if (null != contextAccessor.HttpContext)
        {
            contextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var token);
            if (!string.IsNullOrWhiteSpace(token)) token = token.ToString().Replace("Bearer ", "");

            return token;
        }

        return null;
    }
}