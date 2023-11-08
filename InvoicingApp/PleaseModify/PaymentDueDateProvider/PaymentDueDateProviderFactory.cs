using DeveloperQuestions.LeaveAlone.Models;
using InvoicingApp.PleaseModify.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.PleaseModify.PaymentDueDateProvider
{
    /// <summary>
    /// payment due date provider factory
    /// </summary>
    public class PaymentDueDateProviderFactory
    {
        private readonly IReadOnlyDictionary<PaymentTerms, IPaymentDueDateProvider> _paymentDueDateProviders;
        
        /// <summary>
        /// using reflection add all payment provider from this folder to the dictionary
        /// </summary>
        /// <param name="dateTimeProvider"></param>
        public PaymentDueDateProviderFactory(IDateTimeProvider dateTimeProvider)
        {
            var paymentDueDateProviderType = typeof(IPaymentDueDateProvider);
            _paymentDueDateProviders = paymentDueDateProviderType.Assembly.ExportedTypes
                .Where(p => paymentDueDateProviderType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                .Select(x =>
                {
                    var parameterlessConstructor = x.GetConstructors().SingleOrDefault(i => i.GetParameters().Length == 0);
                    return parameterlessConstructor is not null ? Activator.CreateInstance(x) : Activator.CreateInstance(x, dateTimeProvider);
                })
                .Cast<IPaymentDueDateProvider>()
                .ToImmutableDictionary(x => x.PaymentTerms, x => x);
        }

        public IPaymentDueDateProvider GetProviderByPaymentTerm(PaymentTerms terms)
        {
            return _paymentDueDateProviders.GetValueOrDefault(terms);
        }
    }
}
