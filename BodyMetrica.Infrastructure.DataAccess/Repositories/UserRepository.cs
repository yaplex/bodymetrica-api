using BodyMetrica.Core.Models;
using BodyMetrica.Core.Repositories;

namespace BodyMetrica.Infrastructure.DataAccess.Repositories;

public class UserRepository(BodyMetricaDbContext dbContext) : IUserRepository
{
    public User FindByExternalId(string externalIdentifer)
    {
        var user = dbContext.ApplicationUsers.FirstOrDefault(x => x.ExternalId == externalIdentifer);

        if (user == null)
        {
            // create and add to DB - this is first call for this user
            var defaultUser = new User
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

    public async Task<User> GetById(int id)
    {
        var user = await dbContext.ApplicationUsers.FindAsync(id);
        return user;
    }

    public async Task Update(User user)
    {
        dbContext.ApplicationUsers.Update(user);
        await dbContext.SaveChangesAsync();
    }
}