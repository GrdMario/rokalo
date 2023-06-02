namespace Rokalo.Domain
{
    using System;

    public class Claim
    {
        public Claim(
            Guid id,
            Guid userId,
            string value,
            User user
            )
        {
            this.Id = id;
            this.UserId = userId;
            this.Value = value;
            this.User = user;
        }

        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public string Value { get; protected set; }
        public User User { get; protected set; }
    }
}
