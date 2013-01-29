using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Spring.Social.Intuit.Api;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Api
{
    public interface ICustomerOperations
    {
        Task<Customer> GetCustomer(long id);
        Customer[] GetCustomers();
        Task<Customer> Update(Customer customer);
        Task<Customer> Create(Customer customer);
        /// <summary>
        /// Depending on if there is a idType create or update is called. Essentially
        /// a wrapper around create and update to hide the logic.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="Customer"/>object representing an Intuit Customer.
        /// </returns>
        /// <exception cref="IntuitApiException">If there is an error while communicating with Intuit.</exception>
        Task<Customer> Save(Customer customer);
        bool Delete(Customer customer);
    }
}
