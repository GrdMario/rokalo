namespace Rokalo.Application.User.Commands
{
    using FluentValidation;
    using MediatR;
    using Rokalo.Application.Contracts;
    using Rokalo.Application.Services;
    using Rokalo.Blocks.Common.Extensions;
    using Rokalo.Domain;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public record RegisterUserCommand(
        string email,
        string password,
        string firstName,
        string lastName,
        string phoneNumber,
        string mobileNumber,
        string address,
        int cityId) : IRequest;

    internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.password).Password();
            RuleFor(u => u.firstName).Length(0, 50);
            RuleFor(u => u.lastName).Length(0, 50);
            RuleFor(u => u.phoneNumber).Length(0, 20);
            RuleFor(u => u.mobileNumber).Length(0, 20);
            RuleFor(u => u.address).Length(0, 100);
            RuleFor(u => u.cityId).NotNull(); // TODO add must be valid city id, where is that city id?
            RuleFor(u => u.email).EmailAddress();
        }
    }

    internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUsersUnitOfWork unitOfWork;
        private readonly IPasswordHashingService hashingService;

        public RegisterUserCommandHandler(IUsersUnitOfWork unitOfWork, IPasswordHashingService hashingService)
        {
            this.unitOfWork = unitOfWork;
            this.hashingService = hashingService;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            string hashedPassword = hashingService.HashPassword(request.password, out byte[] salt);

            User user = new User(
                Guid.NewGuid(),
                request.email,
                hashedPassword,
                false,
                Guid.NewGuid().ToString()
                );

            this.unitOfWork.Users.Add(user);

            await this.unitOfWork.SaveChangesAsync(cancellationToken);

            // TODO
            /*
             * 
            it needs to send email via email service for email confirmation */

        }
    }

}
