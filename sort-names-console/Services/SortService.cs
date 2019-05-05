using System.Collections.Generic;
using System.Linq;
using SortNamesConsole.Interfaces;

namespace SortNamesConsole.Services
{
    public class SortService : ISortService
    {
        public List<string> SortNames(List<string> unsortedNames) {
            return unsortedNames.OrderBy(n => n).ToList();
        }
    }
}
