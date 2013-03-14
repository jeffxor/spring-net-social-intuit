using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Builder
{
    public class ItemBuilder
    {
        private Item _item;

        public static ItemBuilder DefaultItem()
        {          
            return new ItemBuilder();
        }

        public ItemBuilder()
        {
            _item = new Item();

            _item.Name = "Pencils";
            _item.Desc = "Pencils HB";
            _item.Taxable = true;
            _item.TaxableSpecified = true;

            Money unitPrice = new Money();
            unitPrice.Amount = decimal.Parse("2");
            unitPrice.AmountSpecified = true;
            _item.UnitPrice = unitPrice;

            AccountRef incomeAccountRef = new AccountRef();
            IdType accountId = new IdType();
            accountId.Value = "30";
            incomeAccountRef.AccountId = accountId;
            _item.IncomeAccountRef = incomeAccountRef;

            _item.PurchaseDesc = "500 pencils purchased";

            Money purchaseCost = new Money();
            purchaseCost.Amount = Decimal.Parse("1");
            purchaseCost.AmountSpecified = true;
            _item.PurchaseCost = purchaseCost;

            AccountRef expenseAccountRef = new AccountRef();
            IdType expenseAccountId = new IdType();
            expenseAccountId.Value = "30";
            expenseAccountRef.AccountId = expenseAccountId;
            _item.ExpenseAccountRef = expenseAccountRef;
            
        }

        public ItemBuilder WithId(long id)
        {
            IdType idType = new IdType();
            idType.Value = id.ToString();
            _item.Id = idType;
            return this;
        }

        public ItemBuilder WithSyncToken(string syncToken)
        {
            _item.SyncToken = syncToken;
            return this;
        }

        public ItemBuilder WithMetaData(String createTimeString, String lastUpdatedTimeString)
        {
            ModificationMetaData metaData = new ModificationMetaData();
            metaData.CreateTime = DateTime.Parse(createTimeString);
            metaData.CreateTimeSpecified = true;
            metaData.LastUpdatedTime = DateTime.Parse(lastUpdatedTimeString);
            metaData.LastUpdatedTimeSpecified = true;
            _item.MetaData = metaData;
            return this;
        }

        public ItemBuilder withDescription(String description)
        {
            _item.Desc = description;
            return this;
        }

        public ItemBuilder WithUnitPrice(decimal unitPriceValue)
        {
            Money unitPrice = new Money();
            unitPrice.Amount = unitPriceValue;
            unitPrice.AmountSpecified = true;
            _item.UnitPrice = unitPrice;
            
            return this;
        }

        public Item Build()
        {
            return _item;
        }

    }
}
