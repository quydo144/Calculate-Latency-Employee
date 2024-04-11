using System;

namespace CalculateLatencyEmployee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Timeline timeline = new Timeline()
            {
                StartShift = new TimeSpan(8, 0, 0),
                EndShift = new TimeSpan(17, 0, 0),
                StartBreak = new TimeSpan(12, 0, 0),
                EndBreak = new TimeSpan(13, 0, 0),
                LeaveFrom = new TimeSpan(9, 0, 0),
                LeaveTo = new TimeSpan(11, 0, 0),
                SignIn = new TimeSpan(12, 12, 0)
            };
            Console.WriteLine(new Employee().CalculateLate(timeline));
        }
    }

    public class Employee
    {
        public TimeSpan CalculateLate(Timeline timeline)
        {
            var woking = TimeSpan.Zero;
            var timeLeave = TimeSpan.Zero;
            if (timeline.StartShift > timeline.SignIn)
                return TimeSpan.Zero;
            var totalBreak = timeline.EndBreak - timeline.StartBreak;
            var wokingTheory = timeline.EndShift - timeline.StartShift - totalBreak;
            if (timeline.LeaveFrom.HasValue && timeline.LeaveTo.HasValue)
            {
                if (timeline.LeaveFrom.Value < timeline.SignIn && timeline.LeaveTo.Value > timeline.SignIn)
                {
                    timeline.SignIn = timeline.LeaveFrom.Value;
                }
                else if (timeline.LeaveFrom.Value < timeline.SignIn)
                {
                    timeLeave = timeline.LeaveFrom.Value - timeline.LeaveTo.Value;
                }
            }
            if (timeline.SignIn < timeline.StartBreak)
            {
                woking = timeline.EndShift - timeline.SignIn - totalBreak - timeLeave;
            }
            else if (timeline.SignIn > timeline.StartBreak && timeline.SignIn < timeline.EndBreak)
            {
                woking = timeline.EndShift - timeline.StartBreak - totalBreak - timeLeave;
            }
            else woking = timeline.EndShift - timeline.SignIn - timeLeave;
            var latency = wokingTheory - woking;
            return latency < TimeSpan.Zero ? TimeSpan.Zero : latency;
        }
    }
}
