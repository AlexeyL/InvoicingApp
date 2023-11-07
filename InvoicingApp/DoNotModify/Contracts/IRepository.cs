namespace DeveloperQuestions.LeaveAlone.Contracts
{
    public interface IRepository<TModel>
    {
        void Add(TModel model);
    }
}
