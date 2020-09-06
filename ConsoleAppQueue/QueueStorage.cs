using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ConsoleAppQueue
{
    public class QueueStorage : IQueueStorage
    {
        private readonly string connectionString;
        private readonly string queueName;
        private CloudStorageAccount storageAccount;
        private CloudQueueClient queueClient;
        private CloudQueue queue;

        //ctor
        public QueueStorage(IConfiguration configuration)
        {
            this.connectionString = configuration["ConnectionStrings:DefaultStorageConnection"];
            this.queueName = configuration["QueueName:demoqueuetst"];

            storageAccount = CloudStorageAccount.Parse(connectionString);
            queueClient = storageAccount.CreateCloudQueueClient();
            queue = queueClient.GetQueueReference(queueName);
        }

        public async Task CreateMessage(string message)
        {
            try
            {
                await queue.CreateIfNotExistsAsync();
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            try
            {
                await queue.AddMessageAsync(new CloudQueueMessage(message));
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public async Task DeleteMessage()
        {
            CloudQueueMessage message = await queue.GetMessageAsync();
            if (message != null)
            {

                try
                {
                    await queue.DeleteMessageAsync(message);
                }
                catch (StorageException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
        }

        public async Task<string> PeekMessage()
        {
            try
            {
                CloudQueueMessage peekedMessage = await queue.PeekMessageAsync();
                if (peekedMessage != null)

                    return peekedMessage.AsString;
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message.ToString());
               
            }
            return "message not peekable";
        }
    }
}
