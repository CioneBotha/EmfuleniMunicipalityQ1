using System;

namespace EmfuleniSystem
{
    internal class ServiceRequest
    {
        public Resident Resident { get; set; }
        public string RequestType { get; set; }
        public int PriorityLevel { get; set; }
        public int SeverityLevel { get; set; }
        public int EstimatedHours { get; set; }

        public int UrgencyScore { get; set; }
        public int AdjustedHours { get; set; }
        public double ImpactScore { get; set; }

        public ServiceRequest(Resident resident, string type, int priority, int severity, int hours)
        {
            Resident = resident;
            RequestType = type;
            PriorityLevel = priority;
            SeverityLevel = severity;
            EstimatedHours = hours;
        }
    }
}