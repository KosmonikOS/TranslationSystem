﻿using FluentValidation;
using TranslationSystem.Domain.Dtos;

namespace TranslationSystem.Domain.Validators;

public class AddWordDtoValidator:AbstractValidator<AddWordDto>
{
    public AddWordDtoValidator()
    {
        RuleFor(x => x.Word).NotEmpty().WithMessage("Команда должна содержать слово");
        RuleFor(x => x.UserId).NotEmpty();
    }
}

