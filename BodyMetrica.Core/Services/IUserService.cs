using BodyMetrica.Core.Models;

namespace BodyMetrica.Core.Services;

public interface IUserService
{
    User GetCurrentUser();
}