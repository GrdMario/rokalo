namespace Rokalo.Application.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IEmailService
    {
        Task SendConfirmEmailAsync(string email, Guid userId, string code);
    }
}
