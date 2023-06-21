namespace Rokalo.Application.Contracts
{
    using Rokalo.Domain;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        //void Update(User user);
        //void Delete(User user);
        Task<User> GetByIdAsyncSafe(Guid id);

        Task<User?> GetByIdAsync(Guid id);

        void Add(User user);
    }
}
