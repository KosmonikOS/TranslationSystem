using FluentValidation;
using TranslationSystem.Domain.Dtos;

namespace TranslationSystem.Domain.Validators;

public class AddWordDtoValidator:AbstractValidator<AddWordDto>
{
    public AddWordDtoValidator()
    {
        RuleFor(x => x.Word).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}

