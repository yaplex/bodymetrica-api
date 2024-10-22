using BodyMetrica.Domain.Common.Models;

namespace BodyMetrica.Domain.Common.Repositories;

public interface IUserRepository
{
    User FindByExternalId(string externalIdentifer);
}