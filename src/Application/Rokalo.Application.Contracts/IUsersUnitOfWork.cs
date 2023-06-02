namespace Rokalo.Application.Contracts
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUsersUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);

        IUserRepository Users { get; }
    }
}
