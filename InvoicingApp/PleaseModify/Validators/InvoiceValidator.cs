using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.PleaseModify.Validators
{
    /// <summary>
    /// invoice validator
    /// </summary>
    public class InvoiceValidator: IInvoiceValidator
    {
        public void ValidateTotalAmount(decimal taxAmount, decimal amount, decimal totalAmount)
        {
            if (taxAmount + amount != totalAmount)
            {
                throw new ArgumentException("Total amount should equal TaxAmount + Amount");
            }
        }

        public void ValidateCreateOnDate(DateTime createdOn, DateTime date)
        {
            if (createdOn > date)
            {
                throw new ArgumentException("CreatedOn date cannot be greater than now");
            }
        }

        public void ValidateInvoceNumber(string invoiceNumber)
        {
            if (string.IsNullOrEmpty(invoiceNumber))
            {
                throw new ArgumentException("InvoiceNumber is required");
            }
        }

        public void ValidateCreatedById(string createdById)
        {
            if (string.IsNullOrEmpty(createdById))
            {
                throw new ArgumentException("CreatedById cannot be null or empty");
            }
        }

        public void ValidateCustomerId(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentException("CustomerId cannot be null or empty");
            }
        }
    }
}
