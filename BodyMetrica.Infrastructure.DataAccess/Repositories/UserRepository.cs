using BodyMetrica.Domain.Common.Models;
using BodyMetrica.Domain.Common.Repositories;

namespace BodyMetrica.Infrastructure.DataAccess.Repositories;

public class UserRepository(BodyMetricaDbContext dbContext) : IUserRepository
{
    public User FindByExternalId(string externalIdentifer)
    {
        var user = dbContext.ApplicationUsers.FirstOrDefault(x => x.ExternalId == externalIdentifer);

        if (user == null)
        {
            // create and add to DB - this is first call for this user
            var defaultUser = new User()
            {
                ExternalId = externalIdentifer,
                CreatedAt = DateTimeOffset.Now
            };
            dbContext.ApplicationUsers.Add(defaultUser);
            dbContext.SaveChanges();
            return defaultUser;
        }

        return user;
    }
}