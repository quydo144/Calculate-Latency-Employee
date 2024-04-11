using System;
using System.ComponentModel.DataAnnotations;

namespace CalculateLatencyEmployee
{
    public class Timeline
    {
        [Required]
        public TimeSpan StartShift {  get; set; }
        [Required]
        public TimeSpan EndShift { get; set; }
        [Required]
        public TimeSpan StartBreak { get; set; }
        [Required]
        public TimeSpan EndBreak { get; set; }
        [Required]
        public TimeSpan SignIn { get; set; }
        public TimeSpan? LeaveFrom { get; set; }
        public TimeSpan? LeaveTo { get; set; }
    }
}
