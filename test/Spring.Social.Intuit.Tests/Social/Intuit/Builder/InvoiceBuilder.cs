using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Builder
{
    public class InvoiceBuilder
    {
        private Invoice _invoice;

        public static InvoiceBuilder DefaultInvoice()
        {          
            return new InvoiceBuilder();
        }

        public InvoiceBuilder()
        {
            _invoice = new Invoice();

            InvoiceHeader invoiceHeader = new InvoiceHeader();

            invoiceHeader.DocNumber = "000101";
            invoiceHeader.Msg = "No Black Ink Pens";
            invoiceHeader.Note = "Blue Ink pens only";
            IdType customerId = new IdType();
            customerId.Value = "5";
            invoiceHeader.CustomerId = customerId;
            invoiceHeader.SubTotalAmt = decimal.Parse("200.00");
            invoiceHeader.SubTotalAmtSpecified = true;
            invoiceHeader.TotalAmt = decimal.Parse("298.75");
            invoiceHeader.TotalAmtSpecified = true;
            invoiceHeader.BillEmail = "john_doe@digitalinsight.com";
            invoiceHeader.ItemElementName = ItemChoiceType2.DiscountAmt;
            invoiceHeader.Item = decimal.Parse("-1.25");
            invoiceHeader.TxnDate = DateTime.Parse("2010-08-07");
            invoiceHeader.TxnDateSpecified = true;
            invoiceHeader.DueDate = DateTime.Parse("2010-08-16");
            invoiceHeader.DueDateSpecified = true;
            _invoice.Header = invoiceHeader;

            InvoiceLine invoiceLine = new InvoiceLine();
            invoiceLine.Desc = "Pens";
            invoiceLine.Amount = decimal.Parse("200.00");
            invoiceLine.AmountSpecified = true;
            invoiceLine.Taxable = true;
            invoiceLine.TaxableSpecified = true;
            IdType itemId = new IdType();
            itemId.Value = "4";
            object[] items = new object[3];
            ItemsChoiceType2[] elementNames = new ItemsChoiceType2[3];
            elementNames[0] = ItemsChoiceType2.ItemId;
            items[0] = itemId;
            elementNames[1] = ItemsChoiceType2.UnitPrice;
            items[1] = decimal.Parse("100"); ;
            elementNames[2] = ItemsChoiceType2.Qty;
            items[2] = decimal.Parse("4");

            invoiceLine.ItemsElementName = elementNames;
            invoiceLine.Items = items;
            
            List<InvoiceLine> lines = new List<InvoiceLine>();
            lines.Add(invoiceLine);

            _invoice.Line = lines.ToArray();
            
        }

        public InvoiceBuilder WithId(long id)
        {
            IdType idType = new IdType();
            idType.Value = id.ToString();
            _invoice.Id = idType;
            return this;
        }

        public InvoiceBuilder WithSyncToken(string syncToken)
        {
            _invoice.SyncToken = syncToken;
            return this;
        }

        public InvoiceBuilder WithMetaData(String createTimeString, String lastUpdatedTimeString)
        {
            ModificationMetaData metaData = new ModificationMetaData();
            metaData.CreateTime = DateTime.Parse(createTimeString);
            metaData.CreateTimeSpecified = true;
            metaData.LastUpdatedTime = DateTime.Parse(lastUpdatedTimeString);
            metaData.LastUpdatedTimeSpecified = true;
            _invoice.MetaData = metaData;
            return this;
        }

        public InvoiceBuilder WithTaxAmount(decimal taxAmount)
        {
            _invoice.Header.TaxAmt = taxAmount;
            _invoice.Header.TaxAmtSpecified = true;
            return this;
        }

        public InvoiceBuilder WithTaxRate(decimal taxRate)
        {
            _invoice.Header.TaxRate = taxRate;
            _invoice.Header.TaxRateSpecified = true;
            return this;
        }

        public InvoiceBuilder WithSalesTaxCodeId(string salesTaxCodeId)
        {
            IdType salesTaxCodeIdType = new IdType();
            salesTaxCodeIdType.Value = salesTaxCodeId;
            salesTaxCodeIdType.idDomain = idDomainEnum.QBO;
            _invoice.Header.SalesTaxCodeId = salesTaxCodeIdType;
            return this;
        }

        public InvoiceBuilder WithSalesTaxCodeName(string salesTaxName)
        {
            _invoice.Header.SalesTaxCodeName = salesTaxName;
            return this;
        }
        
        public Invoice Build()
        {
            return _invoice;
        }
    }
}
