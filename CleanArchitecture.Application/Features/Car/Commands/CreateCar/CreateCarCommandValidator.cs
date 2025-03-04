using FluentValidation;

namespace CleanArchitecture.Application.Features.Car.Commands.CreateCar;

public sealed class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .MinimumLength(3);

        RuleFor(p => p.Model)
            .NotNull()
            .MinimumLength(3);

        RuleFor(p => p.EnginePower)
            .GreaterThanOrEqualTo(1);
    }
}