using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Builder
{
    public class CustomerBuilder
    {
        private Customer _customer;

        public static CustomerBuilder DefaultCustomer()
        {
            
            return new CustomerBuilder();
        }

        public CustomerBuilder()
        {
            _customer = new Customer();

            _customer.TypeOf = partyType.Person;
            _customer.Name = "John Doe";
            _customer.FamilyName = "Doe";
            _customer.GivenName = "John";
            _customer.MiddleName = "J";

            WebSiteAddress webSiteAddress = new WebSiteAddress();
            webSiteAddress.URI = "http://www.digitalinsight.mint.com/";
            List<WebSiteAddress> websites = new List<WebSiteAddress>();
            websites.Add(webSiteAddress);
            _customer.WebSite = websites.ToArray();

            _customer.DBAName = "Mint";
            IdType salesTermId = new IdType();
            salesTermId.Value = "5";
            _customer.SalesTermId = salesTermId;


            EmailAddress emailAddress = new EmailAddress();
            emailAddress.Address = "john_doe@digitalinsight.mint.com";
            List<EmailAddress> emails = new List<EmailAddress>();
            emails.Add(emailAddress);
            _customer.Email = emails.ToArray();

            PhysicalAddress address = new PhysicalAddress();
            address.Line1 = "Park Avenue";
            address.City = "Woodland Hills";
            address.CountrySubDivisionCode = "CA";
            address.PostalCode = "91367";
            List<PhysicalAddress> addresses = new List<PhysicalAddress>();
            addresses.Add(address);
            _customer.Address = addresses.ToArray();
        }

        public CustomerBuilder WithId(long id)
        {
            IdType idType = new IdType();
            idType.Value = id.ToString();
            _customer.Id = idType;
            return this;
        }

        public CustomerBuilder WithMobileNumber(string mobileNumber)
        {
            TelephoneNumber mobile = new TelephoneNumber();
            mobile.DeviceType = "Mobile";
            mobile.FreeFormNumber = mobileNumber;
            List<TelephoneNumber> phones = new List<TelephoneNumber>();
            if (_customer.Phone != null)
            {
                phones.AddRange(_customer.Phone);
            }
            phones.Add(mobile);
            _customer.Phone = phones.ToArray();
            return this;
        }

        public CustomerBuilder WithFaxNumber(string faxNumer)
        {
            TelephoneNumber mobile = new TelephoneNumber();
            mobile.DeviceType = "Fax";
            mobile.FreeFormNumber = faxNumer;
            List<TelephoneNumber> phones = new List<TelephoneNumber>();
            if (_customer.Phone != null)
            {
                phones.AddRange(_customer.Phone);
            }
            phones.Add(mobile);
            _customer.Phone = phones.ToArray();
            return this;
        }

        public CustomerBuilder WithSyncToken(string syncToken)
        {
            _customer.SyncToken = syncToken;
            return this;
        }

        public CustomerBuilder WithMetaData(String createTimeString, String lastUpdatedTimeString)
        {
            ModificationMetaData metaData = new ModificationMetaData();
            metaData.CreateTime = DateTime.Parse(createTimeString);
            metaData.CreateTimeSpecified = true;
            metaData.LastUpdatedTime = DateTime.Parse(lastUpdatedTimeString);
            metaData.LastUpdatedTimeSpecified = true;
            _customer.MetaData = metaData;
            return this;
        }

        public CustomerBuilder WithAddressLine2(string addressLine2)
        {
            PhysicalAddress physicalAddress = _customer.Address[0];
            physicalAddress.Line2 = addressLine2;
            return this;
        }
        
        public Customer Build()
        {
            return _customer;
        }
    }
}
