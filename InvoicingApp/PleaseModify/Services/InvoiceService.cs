using DeveloperQuestions.LeaveAlone.Models;
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
    public class InvoiceService : IInvoiceService
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceValidator _invoiceValidator;

        public InvoiceService(IDateTimeProvider dateTimeProvider, IInvoiceRepository invoiceRepository, IInvoiceValidator invoiceValidator)
        {
            _dateTimeProvider = dateTimeProvider;
            _invoiceRepository = invoiceRepository;
            _invoiceValidator = invoiceValidator;
        }

        public void Add(Invoice model)
        {
            _invoiceValidator.ValidateTotalAmount(model.TaxAmount, model.Amount, model.TotalAmount);
            _invoiceValidator.ValidateCreateOnDate(model.CreatedOn, _dateTimeProvider.DateTimeNow);
            _invoiceValidator.ValidateInvoceNumber(model.InvoiceNumber);
            _invoiceValidator.ValidateCreatedById(model.CreatedById);
            _invoiceValidator.ValidateCustomerId(model.CustomerId);

            switch (model.PaymentTerms)
            {
                case PaymentTerms.COD: model.PaymentDueDate = _dateTimeProvider.DateTimeNow; break;
                case PaymentTerms.Net20: model.PaymentDueDate = _dateTimeProvider.DateTimeNow.AddDays(20); break;
                case PaymentTerms.Net30: model.PaymentDueDate = _dateTimeProvider.DateTimeNow.AddDays(30); break;
                case PaymentTerms.Net60: model.PaymentDueDate = _dateTimeProvider.DateTimeNow.AddDays(60); break;
            }

            _invoiceRepository.Add(model);
        }

       
    }
}
