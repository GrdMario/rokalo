namespace Rokalo.Infrastructure.Db.Users.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Rokalo.Application.Contracts;
    using Rokalo.Blocks.Common.Exceptions;
    using Rokalo.Domain;
    using System;
    using System.Threading.Tasks;

    internal sealed class UserRepository : IUserRepository
    {
        private readonly DbSet<User> users;

        public UserRepository(UsersDbContext context)
        {
            this.users = context.Set<User>();
        }

        public void Add(User user)
        {
            this.users.Add(user);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await this.users.FindAsync(new object[] { id });
        }

        public async Task<User> GetByIdAsyncSafe(Guid id)
        {
            return await this.users.FindAsync() ?? throw new ServiceValidationException("Unable to find that user.");
        }
    }
}
