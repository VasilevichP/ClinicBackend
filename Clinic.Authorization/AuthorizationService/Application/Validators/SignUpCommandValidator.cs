using AuthorizationService.Application.Commands;
using FluentValidation;

namespace AuthorizationService.Application.Validators;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Пожалуйста, введите Email")
            .EmailAddress().WithMessage("Введен невалидный Email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пожалуйста, введите пароль")
            .MinimumLength(6).WithMessage("Пароль должен содержать 6-15 символов")
            .MaximumLength(15).WithMessage("Пароль должен содержать 6-15 символов");

        RuleFor(x => x.ReEnteredPassword)
            .NotEmpty().WithMessage("Пожалуйста, подтвердите пароль")
            .Equal(x => x.Password).WithMessage("Пароли не совпадают");
    }
}