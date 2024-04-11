using System;

namespace CalculateLatencyEmployee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Timeline timeline = new Timeline()
            {
                StartShift = new TimeSpan(12, 23, 0, 0),
                EndShift = new TimeSpan(13, 4, 0, 0),
                StartBreak = new TimeSpan(13, 1, 0, 0),
                EndBreak = new TimeSpan(13, 2, 0, 0),
                LeaveFrom = new TimeSpan(13, 0, 0, 0),
                LeaveTo = new TimeSpan(13, 1, 0, 0),
                SignIn = new TimeSpan(13, 3, 0, 0)
            };
            Console.WriteLine(new Employee().CalculateLate(timeline));
        }
    }

    public class Employee
    {
        public TimeSpan CalculateLate(Timeline timeline)
        {
            var working = TimeSpan.Zero;
            var timeLeave = TimeSpan.Zero;

            if (timeline.StartShift > timeline.SignIn)
                return TimeSpan.Zero;

            var totalBreak = timeline.EndBreak - timeline.StartBreak;
            var workingTheory = timeline.EndShift - timeline.StartShift - totalBreak;

            if (timeline.LeaveFrom.HasValue && timeline.LeaveTo.HasValue)
            {
                if (timeline.LeaveFrom.Value < timeline.SignIn && timeline.LeaveTo.Value > timeline.SignIn) // in leave
                {
                    timeline.SignIn = timeline.LeaveFrom.Value;
                }
                else if (timeline.LeaveFrom.Value < timeline.SignIn) // befor leave
                {
                    timeLeave = timeline.LeaveFrom.Value - timeline.LeaveTo.Value;
                }
            }

            if (timeline.SignIn < timeline.StartBreak)
            {
                working = timeline.EndShift - timeline.SignIn - totalBreak - timeLeave;
            }
            else if (timeline.SignIn > timeline.StartBreak && timeline.SignIn < timeline.EndBreak)
            {
                working = timeline.EndShift - timeline.StartBreak - totalBreak - timeLeave;
            }
            else
            {
                working = timeline.EndShift - timeline.SignIn - timeLeave;
            }

            var latency = workingTheory - working;
            return latency < TimeSpan.Zero ? TimeSpan.Zero : latency;
        }
    }
}
