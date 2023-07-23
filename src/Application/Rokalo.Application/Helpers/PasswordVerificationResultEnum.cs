namespace Rokalo.Application.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum PasswordVerificationResultEnum
    {
        Failed = 0,

        Success = 1,

        /// <summary>
        /// Indicates password verification was successful however the password was encoded using a deprecated algorithm
        /// and should be rehashed and updated.
        /// </summary>
        SuccessRehashNeeded = 2
    }
}
