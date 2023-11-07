using DeveloperQuestions.LeaveAlone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.PleaseModify.Repositories
{
    public interface IInvoiceRepository
    {
        public void Add(Invoice model);
    }
}
