using System.Collections.Generic;
using System.Linq;

namespace TemplateRenamer
{
    public static class Program
    {
        public static void Main()
        {
            List<int> list = new List<int>() { 1, 2, 3 };

            System.Console.WriteLine(string.Join(", ", list.Take(5)));
        }
    }
}
