﻿namespace Rokalo.Application.Helpers
{
    using FluentValidation;

    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string>
            ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty()
                .MinimumLength(12).WithMessage("Password must be at least 12 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least 1 uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least 1 lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least 1 number.")
                .Matches("[@#$%^&+!=]").WithMessage("Password must contain at least 1 special character.");

            return options;
        }


    }
}
