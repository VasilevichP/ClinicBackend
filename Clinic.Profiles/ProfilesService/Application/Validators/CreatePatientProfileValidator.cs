using FluentValidation;
using ProfilesService.Application.Commands;

namespace ProfilesService.Application.Validators;

public class CreatePatientProfileValidator : AbstractValidator<CreatePatientProfileCommand>
{
    public CreatePatientProfileValidator()
    {
        RuleFor(command => command.FirstName)
            .NotEmpty().WithMessage("Введите имя");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Введите фамилию");
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Введите дату рождения");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Введите номер телефона")
            .Matches(@"^\+?[0-9]+$").WithMessage("Введен некорректный номер телефона");
    }
}