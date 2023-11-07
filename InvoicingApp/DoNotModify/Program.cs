using DeveloperQuestions.LeaveAlone.Contracts;
using DeveloperQuestions.LeaveAlone.Implementation;
using DeveloperQuestions.LeaveAlone.Models;
using DeveloperQuestions.PleaseModify;
using DeveloperQuestions.PleaseModify.Services;
using Microsoft.Extensions.DependencyInjection;


public class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        DIRegistration.ConfigureServices(services);
        services
            .AddSingleton<Runner, Runner>()
            .BuildServiceProvider()
            .GetRequiredService<Runner>()
            .Run();
    }

    public class Runner
    {
        private readonly IRepository<Invoice> userRepository;

        public Runner(IRepository<Invoice> userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Run()
        {
            userRepository.Add(new Invoice(DateTime.Now, PaymentTerms.COD,"1", "1234", "1234567890", 100, 8.25M, 108.25M));
        }
    }
}