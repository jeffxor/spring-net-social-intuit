using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Spring.Social.Intuit.Api;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Api
{
    public interface IAccountOperations
    {
        Task<Account> GetAccount(long id);
        Account[] GetAccounts();
        Task<Account> Update(Account account);
        Task<Account> Create(Account account);
        /// <summary>
        /// Depending on if there is a idType create or update is called. Essentially
        /// a wrapper around create and update to hide the logic.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="Account"/>object representing an Intuit Account.
        /// </returns>
        /// <exception cref="IntuitApiException">If there is an error while communicating with Intuit.</exception>
        Task<Account> Save(Account account);
        bool Delete(Account account);
    }
}
