using BodyMetrica.Core.Models;
using BodyMetrica.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodyMetrica.Infrastructure.DataAccess.Repositories;

public class UserRepository(BodyMetricaDbContext dbContext) : IUserRepository
{
    public async Task<User?> FindByExternalId(string externalIdentifer)
    {
        var user = await dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.ExternalId == externalIdentifer);
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

    public async Task<User> Create(User user)
    {
        dbContext.ApplicationUsers.Add(user);
        await dbContext.SaveChangesAsync();
        return user;
    }
}