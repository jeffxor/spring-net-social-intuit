using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Spring.Social.Intuit.Api;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Api
{
    public interface IPaymentMethodOperations
    {
        Task<PaymentMethod> GetPaymentMethod(long id);
        PaymentMethod[] GetPaymentMethods();
    }
}
