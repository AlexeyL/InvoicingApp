using DeveloperQuestions.LeaveAlone.Contracts;
using DeveloperQuestions.LeaveAlone.Models;

namespace InvoicingApp.PleaseModify.Repositories
{
    /// <summary>
    /// invoice repository
    /// </summary>
    public class InvoiceRepository : IRepository<Invoice>, IInvoiceRepository
    {
        private readonly IContext _context;

        public InvoiceRepository(IContext context)
        {
            _context = context;
        }

        public void Add(Invoice model)
        {
            _context.Users.Add(model);
            _context.SaveChanges();
        }
    }
}
