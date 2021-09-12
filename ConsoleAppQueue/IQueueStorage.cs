using System.Threading.Tasks;

namespace ConsoleAppQueue
{
    public interface IQueueStorage
    {
        Task CreateMessage(string message);
        Task<string> PeekMessage();
        Task DeleteMessage();

       // https://www.infoworld.com/article/3628229/how-to-work-with-azure-queue-storage-in-csharp.html#jump
    }
}
