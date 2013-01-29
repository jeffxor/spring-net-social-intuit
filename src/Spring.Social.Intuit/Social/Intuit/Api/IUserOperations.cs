using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spring.Social.Intuit.Api
{
    public interface IUserOperations
    {
        /// <summary>
        /// Asynchronously retrieves the authenticated user's Intuit profile details.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="IntuitProfile"/>object representing the user's profile.
        /// </returns>
        /// <exception cref="IntuitApiException">If there is an error while communicating with Intuit.</exception>
        IntuitProfile GetUserProfileAsync();
    }
}
