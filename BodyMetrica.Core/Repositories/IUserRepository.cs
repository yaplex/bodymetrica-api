using BodyMetrica.Core.Models;
using BodyMetrica.Domain.Common.Models;

namespace BodyMetrica.Core.Repositories;

public interface IUserRepository
{
    User FindByExternalId(string externalIdentifer);
    Task<User> GetById(int id);
    Task Update(User user);
}