namespace Rokalo.Domain
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public User(
            Guid id,
            string email,
            string password,
            bool isEmailVerified,
            string emailVerificationCode)
        {
            this.Id = id;
            this.Email = email;
            this.Password = password;
            this.IsEmailVerified = isEmailVerified;
            this.EmailVerificationCode = emailVerificationCode;
        }

        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public bool IsEmailVerified { get; protected set; }
        public string EmailVerificationCode{ get; protected set; }
        public Profile Profile { get; protected set; }
        public List<Claim> Claims { get; protected set; }

        public void Update(string password)
        {
            this.Password = password; 
        }
    }
}
