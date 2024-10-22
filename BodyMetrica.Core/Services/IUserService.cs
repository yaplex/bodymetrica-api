using BodyMetrica.Domain.Common.Models;

namespace BodyMetrica.Domain.Common.Services;

public interface IUserService
{
    User GetCurrentUser();
}