

using CalculateLatencyEmployee;

namespace CalculateLatencyEmployeeTest
{
    public class Programs
    {
        private Employee _employee;
        private Timeline _timeline;

        [SetUp]
        public void Setup()
        {
            _employee = new Employee();
            _timeline = new Timeline()
            {
                StartShift = new TimeSpan(8, 0, 0),
                EndShift = new TimeSpan(17, 0, 0),
                StartBreak = new TimeSpan(12, 0, 0),
                EndBreak = new TimeSpan(13, 0, 0),
            };
        }

        [Test]
        public void TestLateArrival_BeforeBreak_NotLeave()
        {
            _timeline.SignIn = new TimeSpan(10, 15, 0);
            var latency = _employee.CalculateLate(_timeline);
            Assert.That(latency, Is.EqualTo(new TimeSpan(2, 15, 0))); // late 2h15 
        }

        [Test]
        public void TestLateArrival_InBreak_NotLeave()
        {
            _timeline.SignIn = new TimeSpan(12, 15, 0);
            var latency = _employee.CalculateLate(_timeline);
            Assert.That(latency, Is.EqualTo(new TimeSpan(4, 0, 0))); // late 4h
        }

        [Test]
        public void TestLateArrival_AfterBreak_NotLeave()
        {
            _timeline.SignIn = new TimeSpan(18, 15, 0);
            var latency = _employee.CalculateLate(_timeline);
            Assert.That(latency, Is.EqualTo(new TimeSpan(9, 15, 0))); // late 9h15p
        }

        [Test]
        public void TestLateArrival_BeforeBreak_BeforeLeaveFrom()
        {
            _timeline.LeaveFrom = new TimeSpan(9, 0, 0);
            _timeline.LeaveTo = new TimeSpan(11, 0, 0);
            _timeline.SignIn = new TimeSpan(8, 15, 0);
            var latency = _employee.CalculateLate(_timeline);
            Assert.That(latency, Is.EqualTo(new TimeSpan(0, 15, 0))); // late 15p 
        }

        [Test]
        public void TestLateArrival_BeforeBreak_AfterLeaveTo()
        {
            _timeline.LeaveFrom = new TimeSpan(8, 0, 0);
            _timeline.LeaveTo = new TimeSpan(10, 0, 0);
            _timeline.SignIn = new TimeSpan(10, 15, 0);
            var latency = _employee.CalculateLate(_timeline);
            Assert.That(latency, Is.EqualTo(new TimeSpan(0, 15, 0))); // late 15p 
        }

        [Test]
        public void TestLateArrival_BeforeBreak_InLeave()
        {
            _timeline.LeaveFrom = new TimeSpan(9, 0, 0);
            _timeline.LeaveTo = new TimeSpan(11, 0, 0);
            _timeline.SignIn = new TimeSpan(10, 15, 0);
            var latency = _employee.CalculateLate(_timeline);
            Assert.That(latency, Is.EqualTo(new TimeSpan(1, 0, 0))); // late 1h
        }

        [Test]
        public void TestLateArrival_InBreak_Leave()
        {
            _timeline.LeaveFrom = new TimeSpan(9, 0, 0);
            _timeline.LeaveTo = new TimeSpan(11, 0, 0);
            _timeline.SignIn = new TimeSpan(12, 15, 0);
            var latency = _employee.CalculateLate(_timeline);
            Assert.That(latency, Is.EqualTo(new TimeSpan(2, 0, 0))); // late 1h
        }

        [Test]
        public void TestLateArrival_AfterBreak_AfterLeave()
        {
            _timeline.LeaveFrom = new TimeSpan(9, 0, 0);
            _timeline.LeaveTo = new TimeSpan(11, 0, 0);
            _timeline.SignIn = new TimeSpan(13, 15, 0);
            var latency = _employee.CalculateLate(_timeline);
            Assert.That(latency, Is.EqualTo(new TimeSpan(2, 15, 0))); // late 1h
        }

        [Test]
        public void TestLateArrival_AfterBreak_AfterShift()
        {
            _timeline.LeaveFrom = new TimeSpan(9, 0, 0);
            _timeline.LeaveTo = new TimeSpan(11, 0, 0);
            _timeline.SignIn = new TimeSpan(17, 15, 0);
            var latency = _employee.CalculateLate(_timeline);
            Assert.That(latency, Is.EqualTo(new TimeSpan(6, 15, 0))); // late 6h15p
        }

        [Test]
        public void TestLateArrival_Night()
        {
            Timeline timeline = new Timeline()
            {
                StartShift = new TimeSpan(12, 20, 0, 0),
                EndShift = new TimeSpan(13, 5, 0, 0),
                StartBreak = new TimeSpan(13, 0, 0, 0),
                EndBreak = new TimeSpan(13, 1, 0, 0),
                LeaveFrom = new TimeSpan(12, 22, 0, 0),
                LeaveTo = new TimeSpan(12, 23, 0, 0),
                SignIn = new TimeSpan(13, 2, 15, 0),
            };
            var latency = _employee.CalculateLate(timeline);
            Assert.That(latency, Is.EqualTo(new TimeSpan(4, 15, 0))); // late 6h15p
        }
    }
}