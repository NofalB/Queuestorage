using System.Threading.Tasks;

namespace ConsoleAppQueue
{
    public interface IQueueStorage
    {
        Task CreateMessage(string message);
        Task<string> PeekMessage();
        Task DeleteMessage();
    }
}
