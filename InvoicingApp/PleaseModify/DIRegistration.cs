using DeveloperQuestions.LeaveAlone.Contracts;
using DeveloperQuestions.LeaveAlone.Implementation;
using DeveloperQuestions.LeaveAlone.Models;
using InvoicingApp.PleaseModify.Repositories;
using InvoicingApp.PleaseModify.Services;
using InvoicingApp.PleaseModify.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperQuestions.PleaseModify
{
    public static class DIRegistration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IContext, DBContext>()
                .AddSingleton<IInvoiceValidator, InvoiceValidator>()
                .AddScoped<IDateTimeProvider, DateTimeProvider>()
                .AddScoped<IInvoiceService, InvoiceService>()
                .AddScoped<IRepository<Invoice>, InvoiceRepository>()
                .AddScoped<IInvoiceRepository, InvoiceRepository>();
        }
    }
}
