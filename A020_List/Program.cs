using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A020_List
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> a = new List<int>();
            a.Add(3);
            a.Add(6);
            a.Add(9);
            a.Add(12);
            a.Add(15);
            a.Add(18);
            a.Add(21);
            a.Add(24);
            a.Add(27);
            a.Add(1);

            foreach(var item in a)
            {
                Console.WriteLine(item);
            }

            a.Sort();

            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
        }
    }
}
