using System;

namespace EmfuleniSystem
{
    internal class UtilitiesManager
    {
        // Urgency Score
        public static int CalculateUrgency(int priority, int severity)
        {
            return priority * severity * 2;
        }

        // Used in the service report 
        public static int AdjustResolutionReport(int hours, int urgency)
        {
            return hours + (urgency / 16);
        }

        // Used in the final summary 
        public static int AdjustResolutionFinal(int hours, int urgency)
        {
            return hours + (urgency / 32);
        }

        // Household Impact Score
        public static double CalculateImpact(double usage, int severity)
        {
            return (usage * severity) / 10;
        }
    }
}