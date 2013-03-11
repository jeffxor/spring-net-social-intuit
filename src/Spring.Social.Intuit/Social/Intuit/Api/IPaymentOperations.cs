using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Spring.Social.Intuit.Api;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Api
{
    public interface IPaymentOperations
    {
        Task<Payment> GetPayment(long id);
        Payment[] GetPayments();
        Task<Payment> Update(Payment payment);
        Task<Payment> Create(Payment payment);
        /// <summary>
        /// Depending on if there is a idType create or update is called. Essentially
        /// a wrapper around create and update to hide the logic.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="Payment"/>object representing an Intuit Payment.
        /// </returns>
        /// <exception cref="IntuitApiException">If there is an error while communicating with Intuit.</exception>
        Task<Payment> Save(Payment payment);
        bool Delete(Payment payment);
    }
}
