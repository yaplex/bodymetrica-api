using BodyMetrica.Domain.Common.Models;
using FluentResults;
using MediatR;

namespace BodyMetrica.Domain.Weight.Requests;

public class AddNewLogWeightRequest(decimal weight, DateTimeOffset recordDate) : IRequest<Result>
{
    private UserProfile userProfile;
    public DateTimeOffset RecordDate { get; } = recordDate;

    public decimal WeightInKg
    {
        get
        {
            if ("kg".Equals(userProfile.WeightUnits, StringComparison.OrdinalIgnoreCase))
                return weight;
            throw new NotImplementedException();
        }
    }

    public int UserId => userProfile.Id;

    public void SetUserProfile(UserProfile profile)
    {
        userProfile = profile;
    }
}
