using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using SortNamesConsole.Interfaces;
using SortNamesConsole.Services;

namespace SortNamesConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ILogger<Program> logger;
            var serviceProvider = new ServiceCollection()
                          .AddLogging()
                          .AddSingleton<IFileService, FileService>()
                          .AddSingleton<ISortService, SortService>()
                          .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            try
            {
                logger.LogDebug("Starting application\n");
                var fileService = serviceProvider.GetService<IFileService>();
                var filepath = fileService.ReadFilePath();
                if (filepath.Length == 0)
                {
                    Console.WriteLine("The file set in the configuration file could not be read, please check if the file path is correct.");
                    logger.LogError("The file set in the configuration file could not be read, please check if the file path is correct.");
                    return;
                }

                var validFile = fileService.IsFileValid(filepath);

                if (validFile.Count < 3)
                {
                    Console.WriteLine("The file is invalid because it contains less than 3 names.");
                    logger.LogError("The file is invalid because it contains less than 3 names.");
                    return;
                }

                var sortService = serviceProvider.GetService<ISortService>();
                var sortedNames = sortService.SortNames(validFile);
                fileService.StoreSortNames(sortedNames);
                logger.LogDebug("Exiting application successfully\n\nPress Enter to exit...");
                Console.ReadLine();
                
            }
            catch(Exception ex){
                Console.WriteLine("There is an error in the application with the following message:\r" + ex.InnerException.Message);
                logger.LogCritical("There is an error in the application with the following message:\r" + ex.InnerException.Message);
            }
        }
    }
}
