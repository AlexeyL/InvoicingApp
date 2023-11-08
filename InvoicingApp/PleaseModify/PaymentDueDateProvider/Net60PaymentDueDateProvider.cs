using DeveloperQuestions.LeaveAlone.Models;
using InvoicingApp.PleaseModify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.PleaseModify.PaymentDueDateProvider
{
    public class Net60PaymentDueDateProvider : IPaymentDueDateProvider
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public PaymentTerms PaymentTerms { get; } = PaymentTerms.Net60;

        public Net60PaymentDueDateProvider(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public DateTime GetPaymentDueDate()
        {
            return _dateTimeProvider.DateTimeNow.AddDays(60);
        }
    }
}
