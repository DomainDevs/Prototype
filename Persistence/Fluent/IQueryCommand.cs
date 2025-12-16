
namespace Persistence.Fluent
{
    internal interface IQueryCommand
    {
        IQueryCommand Params(object parameters);
        Task<IEnumerable<T>> RunAsync<T>();
        IQueryCommand Timeout(int seconds);
    }
}