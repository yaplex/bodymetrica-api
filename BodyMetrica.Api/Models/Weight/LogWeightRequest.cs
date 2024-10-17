using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace BodyMetrica.Api.Models.Weight;

public class LogWeightRequest
{
    [Required]
    public decimal Weight { get; set; }
    
    [Required]
    public DateTimeOffset RecordDate { get; set; }
}


public class LogWeightRequestValidator : AbstractValidator<LogWeightRequest>
{
    public LogWeightRequestValidator()
    {
        RuleFor(x => x.Weight).GreaterThan(0);
        RuleFor(x => x.RecordDate).NotEmpty().LessThan(DateTimeOffset.Now);
    }
}