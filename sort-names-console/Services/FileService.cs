using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using SortNamesConsole.Interfaces;

namespace SortNamesConsole.Services
{
    public class FileService: IFileService
    {
        public string ReadFilePath() {
            var configuration = Configuration();

            var filepath = configuration.GetSection("FilePath").GetSection("UnsortedFile").Value;
            if (File.Exists(filepath)) { return filepath; }
            return string.Empty;
        }

        public List<string> IsFileValid(string filepath) {
            var unsortedNames = new List<string>();
            string line;

            StreamReader file = new StreamReader(filepath);
            while ((line = file.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line)) ;
                unsortedNames.Add(line);
            }

            file.Close();
            return unsortedNames;
        }

        public void StoreSortNames(List<string> unsortedNames) {
            var configuration = Configuration();
            var filepath = configuration.GetSection("FilePath").GetSection("SortedFile").Value;
            using (TextWriter sn = new StreamWriter(filepath))
            {
                foreach (string n in unsortedNames)
                {
                    Console.WriteLine(n);
                    sn.WriteLine(n);
                }
            }
            Console.WriteLine();
        }

        private static IConfigurationRoot Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath: Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            return configuration;
        }

    }
}
