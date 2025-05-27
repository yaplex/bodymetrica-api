using FluentResults;
using FluentValidation;
using MediatR;

namespace BodyMetrica.Features.Weight.AddNewWeightLog;

public class AddNewWeightLogRequest : IRequest<Result>
{
    public DateTimeOffset RecordDate { get; set; }

    public decimal Weight { get; set; }
}

public class AddNewLogWeightRequestValidator : AbstractValidator<AddNewWeightLogRequest>
{
    public AddNewLogWeightRequestValidator()
    {
        RuleFor(x => x.Weight).GreaterThan(0);
        RuleFor(x => x.RecordDate).NotEmpty().LessThan(DateTimeOffset.Now);
    }
}