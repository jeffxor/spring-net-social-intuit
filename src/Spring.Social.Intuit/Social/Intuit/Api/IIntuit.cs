using System;

using Spring.Rest.Client;

namespace Spring.Social.Intuit.Api
{
    public interface IIntuit : IApiBinding
    {
        IAccountOperations AccountOperations { get; }
        IInvoiceOperations InvoiceOperations { get; }
        IItemOperations ItemOperations { get; }
        ICustomerOperations CustomerOperations { get; }
        ICompanyMetaDataOperations CompanyMetaDataOperations { get; }
        IPaymentMethodOperations PaymentMethodOperations { get; }
        IPaymentOperations PaymentOperations { get; }
        IUserOperations UserOperations { get; }
    }
}
