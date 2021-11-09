using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_s_algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Object[]> visited = new Dictionary<string, Object[]>();

            visited.Add("A", new Object[] { 0, null});
            visited.Add("B", new Object[] { 5, "A" });
            visited.Add("C", new Object[] { 8, "A" });
            visited.Add("D", new Object[] { 9, "C" });
            visited.Add("E", new Object[] { 11, "D" });

            
        }
    }
}
