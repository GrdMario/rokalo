namespace Rokalo.Application.User.Commands
{
    using FluentValidation;
    using MediatR;
    using Rokalo.Application.Contracts;
    using Rokalo.Application.Helpers;
    using Rokalo.Application.Services;
    using Rokalo.Domain;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public record RegisterUserCommand(
        string Email,
        string Password,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string MobileNumber,
        string Address,
        int CityId) : IRequest;

    internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.Password).Password();
            RuleFor(u => u.FirstName).Length(0, 50);
            RuleFor(u => u.LastName).Length(0, 50);
            RuleFor(u => u.PhoneNumber).Length(0, 20);
            RuleFor(u => u.MobileNumber).Length(0, 20);
            RuleFor(u => u.Address).Length(0, 100);
            RuleFor(u => u.CityId).NotNull(); // TODO add must be valid city id, where is that city id?
            RuleFor(u => u.Email).EmailAddress();
        }
    }

    internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUsersUnitOfWork unitOfWork;
        private readonly IPasswordHashingService hashingService;
        private readonly IEmailService emailService;

        public RegisterUserCommandHandler(IUsersUnitOfWork unitOfWork, IPasswordHashingService hashingService, IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.hashingService = hashingService;
            this.emailService = emailService;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            string hashedPassword = this.hashingService.Hash(request.Password);

            User user = new (
                Guid.NewGuid(),
                request.Email,
                hashedPassword,
                false,
                Guid.NewGuid().ToString()
                );

            this.unitOfWork.Users.Add(user);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            await this.emailService.SendConfirmEmailAsync(user.Email, user.Id, user.EmailVerificationCode);
        }
    }

}
