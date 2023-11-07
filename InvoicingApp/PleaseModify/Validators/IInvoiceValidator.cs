using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.PleaseModify.Validators
{
    public interface IInvoiceValidator
    {
        public void ValidateTotalAmount(decimal taxAmount, decimal amount, decimal totalAmount);
        public void ValidateCreateOnDate(DateTime createdOn, DateTime date);
        public void ValidateInvoceNumber(string invoiceNumber);
        public void ValidateCreatedById(string createdById);
        public void ValidateCustomerId(string customerId);
    }
}
