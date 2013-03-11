using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Spring.Social.Intuit.Api;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Api
{
    public interface IInvoiceOperations
    {
        Task<Invoice> GetInvoice(long id);
        Invoice[] GetInvoices();
        Task<Invoice> Update(Invoice invoice);
        Task<Invoice> Create(Invoice invoice);
        /// <summary>
        /// Depending on if there is a idType create or update is called. Essentially
        /// a wrapper around create and update to hide the logic.
        /// </summary>
        /// <returns>
        /// A <code>Task</code> that represents the asynchronous operation that can return 
        /// a <see cref="Invoice"/>object representing an Intuit Invoice.
        /// </returns>
        /// <exception cref="IntuitApiException">If there is an error while communicating with Intuit.</exception>
        Task<Invoice> Save(Invoice invoice);
        bool Delete(Invoice invoice);
    }
}
