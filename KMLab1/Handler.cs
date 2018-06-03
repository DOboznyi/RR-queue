using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMLab1
{
    class Handler
    {
        Generator generator;
        double averageTimeRR;
        double dispersionRR;
        double relaxationTimeRR;
        double ratioRR;
        double currencyRR;
        double[,] Time = new double[2, 32];

        public Handler(double quantum, double lamdaTask, double lamdaTime, int priority)
        {
            generator = new Generator(quantum, lamdaTask, lamdaTime, priority);
        }

        public void generateSteps(int count)
        {
            generator.generateSteps(count);
        }


        public void hand()
        {
            LinkedList<double[]> tasks = generator.getTasks();
            for (int i = 0; i < tasks.Count; i++)
            {
                averageTimeRR += (tasks.ElementAt(i)[7] - tasks.ElementAt(i)[0]);
                relaxationTimeRR += (tasks.ElementAt(i)[6] - tasks.ElementAt(i)[0]);
                ratioRR += tasks.ElementAt(i)[5];
                currencyRR += tasks.ElementAt(i)[12];
                Time[0,(int)tasks.ElementAt(i)[14]]++;
                Time[1,(int)tasks.ElementAt(i)[14]] += tasks.ElementAt(i)[6] - tasks.ElementAt(i)[0];
            }
            averageTimeRR = averageTimeRR / tasks.Count;
            relaxationTimeRR = relaxationTimeRR / tasks.Count;
            ratioRR = ratioRR / tasks.Count;
            currencyRR = currencyRR / tasks.Count;
            for (int i = 0; i < tasks.Count; i++)
            {
                dispersionRR += (tasks.ElementAt(i)[7] - tasks.ElementAt(i)[0] - averageTimeRR) * (tasks.ElementAt(i)[7] - tasks.ElementAt(i)[0] - averageTimeRR);
            }            
            dispersionRR = dispersionRR / (tasks.Count - 1);
            dispersionRR -= averageTimeRR * averageTimeRR;
            dispersionRR = Math.Abs(dispersionRR);
            Console.WriteLine(" ");
            Console.WriteLine("RR:");
            Console.WriteLine("averageTimeRR: " + averageTimeRR);
            //Console.WriteLine("dispersionRR: " + dispersionRR);
            Console.WriteLine("reactionTimeRR: " + relaxationTimeRR);
            Console.WriteLine("freeTime: " + generator.getFree() + " %");
            //Console.WriteLine("ratioRR: " + ratioRR);
            //Console.WriteLine("currencyRR: " + currencyRR);
            for (int i = 0; i < 32; i++)
            {
                if (Time[0, i] != 0&& Time[1, i]> 0)
                {
                    Console.WriteLine("Priority " + i + " " + Time[1, i] / Time[0, i]);
                }
            }
        }
    }
}
