using BodyMetrica.Core.Models;

namespace BodyMetrica.Core.Repositories;

public interface IUserRepository
{
    Task<User?> FindByExternalId(string externalIdentifer);
    Task<User> GetById(int id);
    Task Update(User user);
    Task<User> Create(User user);
}