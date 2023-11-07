using DeveloperQuestions.LeaveAlone.Contracts;
using DeveloperQuestions.LeaveAlone.Models;

namespace DeveloperQuestions.PleaseModify.Services
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        private readonly IContext _context;

        public InvoiceRepository(IContext context)
        {
            _context = context;
        }

        public void Add(Invoice model)
        {
            if (model.TaxAmount + model.Amount != model.TotalAmount)
            {
                throw new ArgumentException("Total amount should equal TaxAmount + Amount");
            }

            if (model.CreatedOn > DateTime.Now)
            {
                throw new ArgumentException("CreatedOn date cannot be greater than now");
            }

            if (string.IsNullOrEmpty(model.InvoiceNumber))
            {
                throw new ArgumentException("InvoiceNumber is required");
            }

            if (string.IsNullOrEmpty(model.CreatedById))
            {
               throw new ArgumentException("CreatedById cannot be null or empty");
            }

            if (string.IsNullOrEmpty(model.CustomerId))
            {
                throw new ArgumentException("CustomerId cannot be null or empty");
            }

            switch (model.PaymentTerms)
            {
                case PaymentTerms.COD: model.PaymentDueDate = DateTime.Now; break;
                case PaymentTerms.Net20: model.PaymentDueDate = DateTime.Now.AddDays(20); break;
                case PaymentTerms.Net30: model.PaymentDueDate = DateTime.Now.AddDays(30); break;
                case PaymentTerms.Net60: model.PaymentDueDate = DateTime.Now.AddDays(60); break;
            }

            _context.Users.Add(model);
            _context.SaveChanges();
        }
    }
}
