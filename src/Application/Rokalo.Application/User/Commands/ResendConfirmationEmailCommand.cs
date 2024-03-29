﻿namespace Rokalo.Application.User.Commands
{
    using FluentValidation;
    using MediatR;
    using Rokalo.Application.Contracts;
    using System.Threading;
    using System.Threading.Tasks;
    using Rokalo.Domain;
    using System;

    public record ResendConfirmationEmailCommand(string email) : IRequest;

    internal sealed class ResendConfirmationEmailValidator : AbstractValidator<ResendConfirmationEmailCommand>
    {
        private readonly IUsersUnitOfWork unitOfWork;

        public ResendConfirmationEmailValidator(IUsersUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            RuleFor(u => u.email).EmailAddress().NotEmpty();
        }
    }

    internal sealed class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand>
    {
        private readonly IUsersUnitOfWork unitOfWork;
        private readonly IEmailService emailService;

        public ResendConfirmationEmailCommandHandler(IUsersUnitOfWork unitOfWork, IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.emailService = emailService;
        }

        public async Task Handle(ResendConfirmationEmailCommand command, CancellationToken cancellationToken)
        {
            User user = await this.unitOfWork.Users.GetByEmailAsync(command.email, cancellationToken);

            string newVerificationCode = Guid.NewGuid().ToString();
            
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            else
            {
                user.UpdateEmailVerificationCode(newVerificationCode);

                this.unitOfWork.Users.Update(user);

                await this.unitOfWork.SaveChangesAsync(cancellationToken);

                await this.emailService.SendConfirmEmailAsync(command.email, user.Id, newVerificationCode);
            }
        }
    }
}
