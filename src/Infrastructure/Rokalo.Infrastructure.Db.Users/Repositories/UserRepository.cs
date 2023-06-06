namespace Rokalo.Infrastructure.Db.Users.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Rokalo.Application.Contracts;
    using Rokalo.Blocks.Common.Exceptions;
    using Rokalo.Domain;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UserRepository : IUserRepository
    {
        private readonly DbSet<User> users;

        public UserRepository(UsersDbContext context)
        {
            this.users = context.Set<User>();
        }
        public void Create(User user)
        {
            this.users.Add(user);
        }

        public void Delete(User user)
        {
            this.users.Remove(user);
        }

        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await this.users.FindAsync(new object[] {id}, cancellationToken) ?? throw new ServiceValidationException("Unable to find that user.");
        }

        public void Update(User user)
        {
            this.users.Update(user);
        }
    }
}
