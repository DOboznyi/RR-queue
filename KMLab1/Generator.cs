using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMLab1
{
    public class Generator
    {
        LinkedList<double[]> tasks;
        Queue RR;
        double lamdaTask;
        double lamdaTime;
        double quantum;
        int priority;
        double freeTime = 0;
        double endTime;

        public LinkedList<double[]> getTasks()
        {
            return tasks;
        }

        public void generateSteps(int count)
        {
            RR = new Queue();
            double time = 0;
            for (int i = 0; i < count; i++)
            {
                double[] result = new double[15];
                result[0] = time;
                time += generateEvent();
                Random rand = new Random();
                double temp = rand.NextDouble(); 
                result[1] = generTime();
                Console.WriteLine(((tasks.Count()) + "  entryTime=" + result[0] + "  solutionTime=" + result[1]));
                tasks.AddLast(result);
            }
            for (; RR.getCounter() < (count);)
            {
                generateStepRR();
            }
            endTime = RR.getModelTime();
            Console.WriteLine("END RR");
        }


        public void generateStepRR()
        {            
            if (RR.getModelTime() != 0)
            {
                if ((RR.size() == 0) && (RR.getLastAddedTask() != (tasks.Count - 1)))
                {
                    int indexTask = RR.getLastAddedTask() + 1;
                    int priority = generPriority();
                    RR.addTask(new Task(indexTask, tasks.ElementAt(indexTask)[0], tasks.ElementAt(indexTask)[1], priority));
                    if (RR.getModelTime() < tasks.ElementAt(indexTask)[0])
                    {
                        freeTime += -RR.getModelTime() + tasks.ElementAt(indexTask)[0];
                        RR.setModelTime(tasks.ElementAt(indexTask)[0]);
                    }
                    //Console.WriteLine(" ");
                    //Console.WriteLine("Add task: N=" + indexTask + "entranceTime=" + tasks.ElementAt(indexTask)[0] + "leftTime=" + tasks.ElementAt(indexTask)[1] + "priority = " + priority);
                    //Console.WriteLine("modelingTime=" + RR.getModelTime());
                    RR.incLastAddedTask();
                }
                else
                {
                    if (RR.size() != 0)
                    {
                        if (RR.getMaxPriorityTask().getLeftTime() < quantum)
                        {
                            int index = RR.getMaxPriorityTask().getTaskNumber();
                            if (tasks.ElementAt(index)[5] != 0.5)
                            {
                                tasks.ElementAt(index)[12] = getRelevance(tasks.ElementAt(index)[0], RR.getModelTime());
                            }
                            if (tasks.ElementAt(index)[6] == 0)
                            {
                                tasks.ElementAt(index)[6] = RR.getModelTime();
                            }
                            if (tasks.ElementAt(index)[12] > 0)
                            {
                                tasks.ElementAt(index)[5] = 1;
                                //RR.setModelTime(RR.getModelTime() + RR.getMaxPriorityTask().getLeftTime());
                                RR.setModelTime(RR.getModelTime() + quantum);
                            }
                            else
                            {
                                //Console.WriteLine("task: N=" + index + " not relevant any more!");
                            }
                            tasks.ElementAt(index)[7] = RR.getModelTime();
                            tasks.ElementAt(index)[12] = getRelevance(tasks.ElementAt(index)[0], RR.getModelTime());
                            //Console.WriteLine(" ");
                            //Console.WriteLine("Delete task: N=" + index + "  firstEntryTime=" + tasks.ElementAt(index)[6] + "  solutionTime=" + tasks.ElementAt(index)[1] + "  outTime=" + tasks.ElementAt(index)[7] + "priority = " + RR.getMaxPriorityTask().getPriority());
                            //Console.WriteLine("modelingTime=" + RR.getModelTime());
                            tasks.ElementAt(index)[14] = RR.getMaxPriorityTask().getPriority();
                            RR.deleteMaxPriorityTask();
                            RR.incCounter();
                        }
                        else
                        {
                            int index = RR.getMaxPriorityTask().getTaskNumber();
                            if (tasks.ElementAt(index)[5] != 0.5)
                            {
                                tasks.ElementAt(index)[5] = 0.5;
                                tasks.ElementAt(index)[12] = getRelevance(tasks.ElementAt(index)[0], RR.getModelTime());
                            }
                            if (tasks.ElementAt(index)[6] == 0)
                            {
                                tasks.ElementAt(index)[6] = RR.getModelTime();
                            }
                            if (tasks.ElementAt(index)[12] > 0)
                            {
                                RR.setModelTime(RR.getModelTime() + quantum);
                                RR.addTask(new Task(index, RR.getModelTime(), RR.getMaxPriorityTask().getLeftTime() - quantum,RR.getMaxPriorityTask().getPriority()));
                                //Console.WriteLine(" ");
                                //Console.WriteLine("Add task: N=" + index + "entryTime=" + RR.getModelTime() + "leftTime=" + (RR.getMaxPriorityTask().getLeftTime() - quantum) + "priority = " + RR.getMaxPriorityTask().getPriority());
                                //Console.WriteLine("modelingTime=" + RR.getModelTime());
                                RR.deleteMaxPriorityTask();
                            }
                            else
                            {
                                tasks.ElementAt(index)[5] = 0;
                                tasks.ElementAt(index)[7] = RR.getModelTime();
                                tasks.ElementAt(index)[14] = RR.getMaxPriorityTask().getPriority();
                                //Console.WriteLine("task: N=" + index + " not relevant any more!");
                                RR.deleteMaxPriorityTask();
                                RR.incCounter();
                            }
                        }
                    }
                    while ((RR.getLastAddedTask() != (tasks.Count - 1) && (tasks.ElementAt(RR.getLastAddedTask() + 1)[0] < RR.getModelTime())))
                    {
                        int indexTask = RR.getLastAddedTask() + 1;
                        int priority = generPriority();
                        RR.addTask(new Task(indexTask, tasks.ElementAt(indexTask)[0], tasks.ElementAt(indexTask)[1],priority));
                        RR.incLastAddedTask();
                        //Console.WriteLine("Add task: N=" + indexTask + "entranceTime=" + tasks.ElementAt(indexTask)[0] + "leftTime=" + tasks.ElementAt(indexTask)[1] + "priority = " + priority);
                        //Console.WriteLine("modelingTime=" + RR.getModelTime());
                    }
                }
            }
            else
            {
                int priority = generPriority();
                RR.addTask(new Task(0, tasks.ElementAt(0)[0], tasks.ElementAt(0)[1], priority));
                RR.setModelTime(0.00000000001);
                RR.setLastAddedTask(0);
                //Console.WriteLine(" ");
                //Console.WriteLine("Add task: N=" + 0 + "entranceTime=" + tasks.ElementAt(0)[0] + "leftTime=" + tasks.ElementAt(0)[0] + "priority = " + priority);
                //Console.WriteLine("modelingTime=" + RR.getModelTime());
            }
            RR.checkPriority();
        }

        public Generator(double quantum,double lamdaTask, double lamdaTime, int priority)
        {
            tasks = new LinkedList<double[]>();
            this.quantum = quantum;
            this.lamdaTask = lamdaTask;
            this.lamdaTime = lamdaTime;
            this.priority = priority;
        }

        double generateEvent()
        {
            Random rand = new Random(unchecked((int)(DateTime.Now.Ticks)));
            double result = -1.0 / lamdaTask * Math.Log(rand.NextDouble());
            return result;
        }

        double generTime()
        {
            Random rand = new Random(unchecked((int)(DateTime.Now.Ticks)));
            double result = -1.0 / lamdaTime * Math.Log(rand.NextDouble());
            return result;
        }

        int generPriority()
        {
            Random rand = new Random(unchecked((int)(DateTime.Now.Ticks)));
            int result = rand.Next(priority);
            return result;
        }

        double getRelevance(double time, double currTime)
        {
            if (Math.Abs(currTime - time) > 1000000) {
                if (Math.Abs(currTime - time) > 6)
                {
                    return 0;
                }
                return Math.Abs(currTime - time) * (-0.5) + 3;
            };
            return 1;
        }

        public double getFree() {
            return freeTime/endTime*100;
        }
    }
}
