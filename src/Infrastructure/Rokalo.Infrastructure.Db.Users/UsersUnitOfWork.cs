namespace Rokalo.Infrastructure.Db.Users
{
    using Rokalo.Application.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    internal sealed class UsersUnitOfWork : IUsersUnitOfWork
    {
        private readonly UsersDbContext context;

        public UsersUnitOfWork(IUserRepository users, UsersDbContext context)
        {
            this.context = context;
            this.Users = users;
        }
        public IUserRepository Users { get; }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
