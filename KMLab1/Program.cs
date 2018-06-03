using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random(unchecked((int)(DateTime.Now.Ticks)));
            Random rand1 = new Random(unchecked((int)(DateTime.Now.Ticks)));
            Random rand2 = new Random(unchecked((int)(DateTime.Now.Ticks)));
            Console.SetBufferSize(1000, Int16.MaxValue-1);
            Handler h = new Handler(0.001, 1, 1, 32);
            h.generateSteps(100);
            h.hand();
            Console.ReadKey();
        }
    }
}
