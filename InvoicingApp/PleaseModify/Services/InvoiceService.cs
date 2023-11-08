using DeveloperQuestions.LeaveAlone.Models;
using InvoicingApp.PleaseModify.PaymentDueDateProvider;
using InvoicingApp.PleaseModify.Repositories;
using InvoicingApp.PleaseModify.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.PleaseModify.Services
{
    /// <summary>
    /// invoice service
    /// </summary>
    public class InvoiceService : IInvoiceService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceValidator _invoiceValidator;
        private readonly PaymentDueDateProviderFactory _paymentDueDateProviderFactory;

        public InvoiceService(IDateTimeProvider dateTimeProvider, IInvoiceRepository invoiceRepository, IInvoiceValidator invoiceValidator)
        {
            _dateTimeProvider = dateTimeProvider;
            _invoiceRepository = invoiceRepository;
            _invoiceValidator = invoiceValidator;
            _paymentDueDateProviderFactory = new PaymentDueDateProviderFactory(_dateTimeProvider);
        }

        /// <summary>
        /// add new invoice
        /// </summary>
        /// <param name="model"></param>
        public void Add(Invoice model)
        {
            _invoiceValidator.ValidateTotalAmount(model.TaxAmount, model.Amount, model.TotalAmount);
            _invoiceValidator.ValidateCreateOnDate(model.CreatedOn, _dateTimeProvider.DateTimeNow);
            _invoiceValidator.ValidateInvoceNumber(model.InvoiceNumber);
            _invoiceValidator.ValidateCreatedById(model.CreatedById);
            _invoiceValidator.ValidateCustomerId(model.CustomerId);

            var provider = _paymentDueDateProviderFactory.GetProviderByPaymentTerm(model.PaymentTerms);

            if (provider is not null)
            {
                var paymentDueDate = provider.GetPaymentDueDate();

                model.PaymentDueDate = paymentDueDate;
            }

            _invoiceRepository.Add(model);
        }

       
    }
}
