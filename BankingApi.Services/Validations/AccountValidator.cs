using BankingApi.Core.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Validations
{
    public class AccountValidator: AbstractValidator<Account>
    {
        public AccountValidator()
        {
            SetUpRules();
        }

        private void SetUpRules()
        {
            RuleSet("Basic Validation", () =>
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0)
                    .WithMessage(x => $"Invalid AccountId identified by {x.Id}");
            });

            RuleSet("New Account Validation", () =>
            {
            });
        }
    }
}
