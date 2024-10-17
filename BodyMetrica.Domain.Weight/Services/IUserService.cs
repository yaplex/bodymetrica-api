using BodyMetrica.Domain.Common.Models;

namespace BodyMetrica.Domain.Weight.Services;

public interface IUserService
{
    UserProfile GetCurrentUser();
}