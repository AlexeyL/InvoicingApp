using DeveloperQuestions.LeaveAlone.Contracts;
using DeveloperQuestions.LeaveAlone.Implementation;
using DeveloperQuestions.LeaveAlone.Models;
using DeveloperQuestions.PleaseModify.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperQuestions.PleaseModify
{
    public static class DIRegistration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IContext, DBContext>()
                .AddScoped<IRepository<Invoice>, InvoiceRepository>();
        }
    }
}
