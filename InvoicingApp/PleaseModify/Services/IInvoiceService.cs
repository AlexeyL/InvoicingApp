using DeveloperQuestions.LeaveAlone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicingApp.PleaseModify.Services
{
    public interface IInvoiceService
    {
        public void Add(Invoice model);
    }
}
