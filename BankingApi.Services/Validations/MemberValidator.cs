using BankingApi.Core.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApi.Services.Validations
{
    public class MemberValidator: AbstractValidator<Member>
    {
        public MemberValidator()
        {
            SetUpRules();
        }

        private void SetUpRules()
        {
            RuleSet("Basic Validation", () => {

                RuleFor(x => x.Id)
                    .GreaterThan(0)
                        .WithMessage(x => $"InValid Member Id identified by {x.Id}");

                RuleFor(x => x.InstitutionId)
                    .GreaterThan(0)
                        .WithMessage(x => $"InValid Institution Id identified by {x.Id}");

                RuleFor(x => x.SurName)
                    .NotEmpty()
                        .WithMessage(x => $"SurName cannot be empty");

                RuleFor(x => x.GivenName)
                    .NotEmpty()
                        .WithMessage(x => $"GivenName cannot be empty");

            });

            RuleSet("New Member Validation", () => {

                RuleFor(x => x.Id)
                    .GreaterThan(0)
                        .WithMessage(x => $"InValid Member Id identified by {x.Id}");

                RuleFor(x => x.InstitutionId)
                    .GreaterThan(0)
                        .WithMessage(x => $"InValid Institution Id identified by {x.Id}");

                RuleFor(x => x.SurName)
                    .NotEmpty()
                        .WithMessage(x => $"SurName cannot be empty");

                RuleFor(x => x.GivenName)
                    .NotEmpty()
                        .WithMessage(x => $"GivenName cannot be empty");
            });
        }
    }
}
