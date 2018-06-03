using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMLab1
{
    class Task
    {
        int taskNumber;
        double entranceTime;
        double leftTime;
        int priority;

        public int getTaskNumber()
        {
            return taskNumber;
        }

        public Task(int taskNumber, double entranceTime, double leftTime)
        {
            this.taskNumber = taskNumber;
            this.entranceTime = entranceTime;
            this.leftTime = leftTime;
        }

        public Task(int taskNumber, double entranceTime, double leftTime, int priority)
        {
            this.taskNumber = taskNumber;
            this.entranceTime = entranceTime;
            this.leftTime = leftTime;
            this.priority = priority;
        }

        public double getLeftTime()
        {
            return leftTime;
        }

        public double getEntranceTime()
        {
            return entranceTime;
        }

        public int getPriority()
        {
            return priority;
        }

        public void setPriority(int priority)
        {
            this.priority = priority;
        }
    }
}
