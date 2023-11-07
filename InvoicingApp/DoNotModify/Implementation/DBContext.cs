using DeveloperQuestions.LeaveAlone.Contracts;
using DeveloperQuestions.LeaveAlone.Models;

namespace DeveloperQuestions.LeaveAlone.Implementation
{
    public class DBContext : IContext
    {
        public ICollection<Invoice> Users { get; set; } = new List<Invoice>();

        public void SaveChanges()
        {
            // Probably saves to database
        }
    }
}
