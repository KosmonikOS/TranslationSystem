using FluentValidation;
using TranslationSystem.Domain.Dtos;

namespace TranslationSystem.Domain.Validators;

public class GetWordByIdDtoValidator:AbstractValidator<GetWordByIdDto>
{
    public GetWordByIdDtoValidator()
    {
        RuleFor(x => x.WordId).NotEmpty();
    }
}

