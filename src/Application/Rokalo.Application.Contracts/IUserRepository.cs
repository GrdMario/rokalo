namespace Rokalo.Application.Contracts
{
    using Rokalo.Domain;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        void Create(User user);
        void Update(User user);
        void Delete(User user);
        Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        // TODO GetAsync -- do we need to get all users?
    }
}
