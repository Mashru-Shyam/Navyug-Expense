using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.EmailVerification
{
    public class EmailVerificationCommandValidator : AbstractValidator<EmailVerificationCommand>
    {
        public EmailVerificationCommandValidator()
        {
            RuleFor(v => v.Token)
                .NotEmpty().WithMessage("Token is required.");
        }
    }
}
