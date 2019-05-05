using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortNamesConsole.Interfaces;
using SortNamesConsole.Services;

namespace unit_test_sort_name
{
    [TestClass]
    public class UnitTestSortNames
    {
        IFileService fileService;
        ISortService sortService;

        [TestInitialize]
        public void TestInitialize()
        {
            var serviceProvider = new ServiceCollection()
                                      .AddSingleton<IFileService, FileService>()
                                      .AddSingleton<ISortService, SortService>()
                                      .BuildServiceProvider();
            fileService = serviceProvider.GetService<IFileService>();
            sortService = serviceProvider.GetService<ISortService>();
        }

        [TestMethod]
        public void TestReadFilePath()
        {
            var filepath = fileService.ReadFilePath();
            Assert.AreEqual(filepath.Length > 0, true);
        }

        [TestMethod]
        public void TestIsFileValid()
        {
            const string FileName = "unsorted-names-list.txt";
            var validFile = fileService.IsFileValid(FileName);
            Assert.AreEqual(validFile.Count > 3, true);
        }

        [TestMethod]
        public void TestSortNames()
        {
            const string FileName = "unsorted-names-list.txt";
            var validFile = fileService.IsFileValid(FileName);
            var sortedNames = sortService.SortNames(validFile);
            Assert.AreEqual(sortedNames.Count == validFile.Count, true);
        }
    }
}
