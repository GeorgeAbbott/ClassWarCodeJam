using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_War
{
    class Timer
    {
        TimeSpan avgDuration;
        DateTime t1;
        DateTime t2;
        bool isTiming = false;
        bool firstTiming = true;
        int numTimings = 0;

        public void StartTime()
        {
            t1 = DateTime.Now;
            isTiming = true;
        }

        public void EndTime()
        {
            t2 = DateTime.Now;
            isTiming = false;
            if (firstTiming) firstTiming = false;
            numTimings++;
            avgDuration.Add(t2 - t1);
        }

        public TimeSpan GetLastDuration()
        {
            if (!isTiming) return t2 - t1;
            else throw new InvalidOperationException("Cannot call GetDuration when currently timing");
        }

        public TimeSpan GetAverageDuration()
        {
            if (numTimings == 0) throw new InvalidOperationException("No average duration where no timing has completed yet");
            return TimeSpan.FromTicks(avgDuration.Ticks / numTimings);
        }


    }
}
