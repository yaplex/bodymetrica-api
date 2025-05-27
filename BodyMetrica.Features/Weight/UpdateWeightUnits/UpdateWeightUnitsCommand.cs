using FluentValidation;
using MediatR;

namespace BodyMetrica.Features.Weight.UpdateWeightUnits;

public class UpdateWeightUnitsCommand : IRequest
{
    public string WeightUnits { get; set; }
}

public class UpdateWeightUnitsRequestValidator : AbstractValidator<UpdateWeightUnitsCommand>
{
    private static readonly List<string> SupportedWeightUnits = new() { "kg", "lb" };

    public UpdateWeightUnitsRequestValidator()
    {
        RuleFor(x => x.WeightUnits)
            .NotEmpty()
            .Must(x => SupportedWeightUnits.Contains(x));
    }
}