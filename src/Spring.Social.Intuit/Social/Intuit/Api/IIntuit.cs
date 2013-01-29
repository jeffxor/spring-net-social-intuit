using System;

using Spring.Rest.Client;

namespace Spring.Social.Intuit.Api
{
    public interface IIntuit : IApiBinding
    {
        IUserOperations UserOperations { get; }
        ICustomerOperations CustomerOperations { get; }
    }
}
