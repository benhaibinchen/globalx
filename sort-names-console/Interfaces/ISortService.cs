using System.Collections.Generic;

namespace SortNamesConsole.Interfaces
{
    public interface ISortService
    {
        List<string> SortNames(List<string> unsortedNames);
    }
}
