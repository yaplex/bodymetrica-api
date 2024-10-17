using BodyMetrica.Domain.Common.Models;

namespace BodyMetrica.Domain.Common.Repositories;

public interface IUserRepository
{
    UserProfile FindByExternalId(string externalIdentifer);
}