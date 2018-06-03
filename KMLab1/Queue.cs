using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMLab1
{
    class Queue
    {
        double modelTime;
        int lastAddedTask;
        int counter;
        LinkedList<Task> tasks;

        public Queue()
        {
            tasks = new LinkedList<Task>();
            counter = 0;
        }
        public double getModelTime()
        {
            return modelTime;
        }

        public int size()
        {
            return tasks.Count;
        }
        public void setModelTime(double modelTime)
        {
            this.modelTime = modelTime;
        }

        public int getLastAddedTask()
        {
            return lastAddedTask;
        }

        public void incLastAddedTask()
        {
            lastAddedTask++;
        }


        public void setLastAddedTask(int p)
        {
            lastAddedTask = p;
        }

        public int getCounter()
        {
            return counter;
        }

        public void incCounter()
        {
            counter++;
        }

        public void addTask(Task task)
        {
            tasks.AddLast(task);
        }

        public void deleteFirstTask()
        {
            tasks.RemoveFirst();
        }

        public Task getFirstTask()
        {
            return tasks.ElementAt(0);
        }

        public Task get(int i)
        {
            return tasks.ElementAt(i);
        }

        public Task getMaxPriorityTask(){
            Task buf = tasks.ElementAt(0);
            int min = buf.getPriority();
            for (int i=0; i < tasks.Count; i++)
            {
                if (tasks.ElementAt(i).getPriority()<min)
                {
                    buf = tasks.ElementAt(i);
                    min = buf.getPriority();
                }
            }
            return buf;
        }

        public void deleteMaxPriorityTask()
        {
            Task buf = tasks.ElementAt(0);
            int min = buf.getPriority();
            int index = 0;
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks.ElementAt(i).getPriority() < min)
                {
                    buf = tasks.ElementAt(i);
                    min = buf.getPriority();
                    index = i;
                }
            }
            tasks.Remove(tasks.Find(tasks.ElementAt(index)));
        }

        public void checkPriority() {
            for (int i = 0; i < tasks.Count; i++)
            {
                if ((tasks.ElementAt(i).getEntranceTime()-modelTime > 15) && (tasks.ElementAt(i).getPriority()>0))
                {
                    tasks.ElementAt(i).setPriority(tasks.ElementAt(i).getPriority() - 1);
                }
            }
        }
    }
}
