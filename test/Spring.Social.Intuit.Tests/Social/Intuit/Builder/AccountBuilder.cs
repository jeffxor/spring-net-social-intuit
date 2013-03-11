using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Builder
{
    public class AccountBuilder
    {
        private Account _account;

        public static AccountBuilder DefaultAccount()
        {
            
            return new AccountBuilder();
        }

        public AccountBuilder()
        {
            _account = new Account();

            _account.Name = "Loan Account";
            _account.Desc = "Loan Account";
            _account.Subtype = "Savings";
            _account.AcctNum = "5001";
        }

        public AccountBuilder WithId(long id)
        {
            IdType idType = new IdType();
            idType.Value = id.ToString();
            _account.Id = idType;
            return this;
        }

        public AccountBuilder WithSyncToken(string syncToken)
        {
            _account.SyncToken = syncToken;
            return this;
        }

        public AccountBuilder WithMetaData(String createTimeString, String lastUpdatedTimeString)
        {
            ModificationMetaData metaData = new ModificationMetaData();
            metaData.CreateTime = DateTime.Parse(createTimeString);
            metaData.CreateTimeSpecified = true;
            metaData.LastUpdatedTime = DateTime.Parse(lastUpdatedTimeString);
            metaData.LastUpdatedTimeSpecified = true;
            _account.MetaData = metaData;
            return this;
        }

        public AccountBuilder WithDescription(string description)
        {
            _account.Desc = description;
            return this;
        }

        public AccountBuilder WithOpeningBalanceDate(string openingBalance)
        {
            _account.OpeningBalanceDateSpecified = true;
            _account.OpeningBalanceDate = DateTime.Parse(openingBalance);
            return this;
        }
        
        public Account Build()
        {
            return _account;
        }


    }
}
