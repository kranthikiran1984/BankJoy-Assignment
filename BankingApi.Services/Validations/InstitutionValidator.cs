using BankingApi.Core.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Validations
{
    public class InstitutionValidator: AbstractValidator<Institution>
    {
        public InstitutionValidator()
        {
            SetUpRules();
        }

        private void SetUpRules()
        {
            RuleSet("Basic Validation", () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0)
                        .WithMessage(x => $"Invalid institution id identified by {x.Id}");

                RuleFor(x => x.Name)
                    .NotEmpty()
                        .WithMessage(x => $"Institution name cannot be empty");
            });

            RuleSet("New Institution Validation", () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                        .WithMessage(x => $"Institution name cannot be empty");
            });
        }
    }
}
