using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Builder
{
    public class PaymentBuilder
    {
        private Payment _payment;

        public static PaymentBuilder DefaultPayment()
        {          
            return new PaymentBuilder();
        }

        public PaymentBuilder()
        {
            _payment = new Payment();

            PaymentHeader paymentHeader = new PaymentHeader();

            paymentHeader.DocNumber = "54";
            paymentHeader.Note = "Payment against Invoice";
            IdType customerId = new IdType();
            customerId.Value = "5";
            paymentHeader.CustomerId = customerId;
            IdType depositAccountId = new IdType();
            depositAccountId.Value = "41";
            paymentHeader.DepositToAccountId = depositAccountId;
            IdType paymentMethodId = new IdType();
            paymentMethodId.Value = "1";
            paymentHeader.PaymentMethodId = paymentMethodId;
            paymentHeader.TotalAmt = decimal.Parse("20.00");
            paymentHeader.TotalAmtSpecified = true;
            paymentHeader.ProcessPayment = false;
            paymentHeader.ProcessPaymentSpecified = true;
            paymentHeader.TxnDate = DateTime.Parse("2010-08-09");
            paymentHeader.TxnDateSpecified = true;
            _payment.Header = paymentHeader;

            PaymentLine paymentLine = new PaymentLine();
            IdType txnId = new IdType();
            txnId.Value = "8";
            paymentLine.TxnId = txnId;

            paymentLine.Amount = decimal.Parse("20.00");
            paymentLine.AmountSpecified = true;
            List<PaymentLine> lines = new List<PaymentLine>();
            lines.Add(paymentLine);

            _payment.Line = lines.ToArray();
        }

        public PaymentBuilder WithId(long id)
        {
            IdType idType = new IdType();
            idType.Value = id.ToString();
            _payment.Id = idType;
            return this;
        }

        public PaymentBuilder WithSyncToken(string syncToken)
        {
            _payment.SyncToken = syncToken;
            return this;
        }

        public PaymentBuilder WithMetaData(String createTimeString, String lastUpdatedTimeString)
        {
            ModificationMetaData metaData = new ModificationMetaData();
            metaData.CreateTime = DateTime.Parse(createTimeString);
            metaData.CreateTimeSpecified = true;
            metaData.LastUpdatedTime = DateTime.Parse(lastUpdatedTimeString);
            metaData.LastUpdatedTimeSpecified = true;
            _payment.MetaData = metaData;
            return this;
        }

        public PaymentBuilder WithTotalAmount(decimal totalAmount)
        {
            _payment.Header.TotalAmt = totalAmount;
            _payment.Header.TotalAmtSpecified = true;
            _payment.Line[0].Amount = totalAmount;
            _payment.Line[0].AmountSpecified = true;
            return this;
        }

        public Payment Build()
        {
            return _payment;
        }

    }
}
