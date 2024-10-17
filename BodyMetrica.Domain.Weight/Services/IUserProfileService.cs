using BodyMetrica.Domain.Common.Models;

namespace BodyMetrica.Domain.Weight.Services;

public interface IUserProfileService
{
    UserProfile GetUserProfile();
}