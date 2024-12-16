using BodyMetrica.Features.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BodyMetrica.Features.Common;

public interface IUserService
{
    Task<BodyMetricaUserRecord> GetLoggedInUser();
}

public class UserService(IAuth0 auth0, IMemoryCache cache, BodyMetricaDbContext dbContext) : IUserService
{
    public async Task<BodyMetricaUserRecord> GetLoggedInUser()
    {
        var externalUserId = auth0.GetLoggedInUserId();
        var user = await FindByExternalId(externalUserId);

        if (user == null)
        {
            // this is first time user logs in or created account, need to create a record on db.
            var auth0Info = await auth0.GetUser();
            var newUser = CreateDefaultBodyMetricaUser(auth0Info);

            dbContext.ApplicationUsers.Add(newUser);
            await dbContext.SaveChangesAsync();

            return newUser;
        }

        return user;
    }

    private async Task<BodyMetricaUserRecord?> FindByExternalId(string externalUserId)
    {
        var cachedRecord = cache.Get<BodyMetricaUserRecord>(externalUserId);
        if (null != cachedRecord)
            return cachedRecord;

        var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.ExternalId == externalUserId);
        if (null != user)
        {
            cache.Set(externalUserId, user, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(1), // minimum cache time
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // maximum cache time
            });
        }

        return user;
    }


    private static BodyMetricaUserRecord CreateDefaultBodyMetricaUser(Auth0UserInformation auth0Info)
    {
        var newUser = new BodyMetricaUserRecord
        {
            ExternalId = auth0Info.sub,
            Name = auth0Info.name,
            Picture = auth0Info.picture,
            Email = auth0Info.email,
            EmailVerified = auth0Info.email_verified,
            CreatedAt = DateTimeOffset.Now,

            WeightUnits = "kg"
        };
        return newUser;
    }
}