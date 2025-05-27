using FluentResults;
using FluentValidation;
using MediatR;

namespace BodyMetrica.Features.BloodPressure.AddNewBloodPressureLog;

public class LogNewBloodPressureCommand: IRequest<Result>
{
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public int Pulse { get; set; }
    public string? Note { get; set; }
    public DateTimeOffset RecordDate { get; set; }
}

public class LogNewBloodPressureCommandValidator : AbstractValidator<LogNewBloodPressureCommand>
{
    public LogNewBloodPressureCommandValidator()
    {
        RuleFor(x => x.Systolic).GreaterThan(0);
        RuleFor(x => x.Diastolic).GreaterThan(0);
        RuleFor(x => x.Pulse).GreaterThan(0);
        RuleFor(x => x.RecordDate).NotEmpty().LessThan(DateTimeOffset.Now);
    }
}