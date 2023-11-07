using DeveloperQuestions.LeaveAlone.Models;

namespace DeveloperQuestions.LeaveAlone.Contracts
{
    public interface IContext
    {
        ICollection<Invoice> Users { get; set; }

        void SaveChanges();
    }
}
