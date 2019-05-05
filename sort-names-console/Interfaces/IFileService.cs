using System.Collections.Generic;

namespace SortNamesConsole.Interfaces
{
    public interface IFileService
    {
        string ReadFilePath();

        List<string> IsFileValid(string filepath);

        void StoreSortNames(List<string> unsortedNames);
    }
}
