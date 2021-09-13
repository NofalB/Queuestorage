using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ConsoleAppQueue
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static void Main(string[] args)
        {
            MainSetupIConf();

            QueueStorage queueStorage = new QueueStorage(configuration);
            queueStorage.CreateMessage("Hi there").Wait();
            queueStorage.PeekMessage().Wait(); ;
            queueStorage.DeleteMessage().Wait();
            queueStorage.DeleteQueue().Wait();
        }

        static void MainSetupIConf()
        {
            // Create service collection
            ServiceCollection serviceCollection = new ServiceCollection();

            //configure service collection
            ConfigureServices(serviceCollection);

            // Create service provider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
        }
    }
}
