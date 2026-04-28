using System;
using System.Collections.Generic;

namespace EmfuleniSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Welcomes the user
            Console.WriteLine("=== Welcome to Emfuleni Municipality Service Desk ===");

            //Prompts the user to enter the number of residents they want to register
            Console.Write("How many residents do you want to register? ");
            int numResidents = int.Parse(Console.ReadLine());

            List<Resident> residents = new List<Resident>();
            //Prompts the user for info of the resident and adds the resident
            for (int i = 0; i < numResidents; i++)
            {
                Console.WriteLine($"\n--- Resident {i + 1} ---");

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Address: ");
                string address = Console.ReadLine();

                Console.Write("Account Number: ");
                string account = Console.ReadLine();

                Console.Write("Monthly Utility Usage (kWh or litres): ");
                double usage = double.Parse(Console.ReadLine());

                residents.Add(new Resident(name, address, account, usage));
            }

            // Prompts the user to enter the amount of service requests they want to log
            Console.Write("\nHow many service requests do you want to log? ");
            int numRequests = int.Parse(Console.ReadLine());

            List<ServiceRequest> requests = new List<ServiceRequest>();
            //Prompts the user to enter information about the service requests
            for (int i = 0; i < numRequests; i++)
            {
                Console.WriteLine($"\n--- Service Request {i + 1} ---");

                Console.Write($"Select resident by number (1 to {residents.Count}): ");
                int resIndex = int.Parse(Console.ReadLine()) - 1;

                Console.Write("Request Type (e.g., Water Outage, Burst Pipe): ");
                string type = Console.ReadLine();

                Console.Write("Priority Level (1–5): ");
                int priority = int.Parse(Console.ReadLine());

                Console.Write("Severity Level (1–10): ");
                int severity = int.Parse(Console.ReadLine());

                Console.Write("Estimated Resolution Hours: ");
                int hours = int.Parse(Console.ReadLine());

                ServiceRequest req = new ServiceRequest(residents[resIndex], type, priority, severity, hours);

                // Calculations
                req.UrgencyScore = UtilitiesManager.CalculateUrgency(priority, severity);
                req.AdjustedHours = UtilitiesManager.AdjustResolutionReport(hours, req.UrgencyScore);
                req.ImpactScore = UtilitiesManager.CalculateImpact(residents[resIndex].MonthlyUsage, severity);

                requests.Add(req);
            }
            //Displays the service report
            ServiceRequest highest = null;

            foreach (var req in requests)
            {
                Console.WriteLine("\n=== Service Report ===");
                Console.WriteLine($"Resident: {req.Resident.Name}");
                Console.WriteLine($"Service Type: {req.RequestType}");
                Console.WriteLine($"Urgency Score: {req.UrgencyScore}");
                Console.WriteLine($"Adjusted Resolution: {req.AdjustedHours} hours");
                Console.WriteLine($"Household Impact Score: {req.ImpactScore:F2}");

                if (highest == null || req.UrgencyScore > highest.UrgencyScore)
                {
                    highest = req;
                }
            }

            //Final summary
            Console.WriteLine("\n=== FINAL MUNICIPAL SUMMARY ===");
            Console.WriteLine("Highest priority issue:");

            int finalHours = UtilitiesManager.AdjustResolutionFinal(
                highest.EstimatedHours,
                highest.UrgencyScore
            );

            Console.WriteLine($"Resident: {highest.Resident.Name}");
            Console.WriteLine($"Service Type: {highest.RequestType}");
            Console.WriteLine($"Urgency Score: {highest.UrgencyScore}");
            Console.WriteLine($"Adjusted Resolution: {finalHours} hours");
            Console.WriteLine($"Household Impact Score: {highest.ImpactScore:F2}");

            Console.WriteLine("\nThank you for using the Emfuleni Municipality Service Desk.");
        }
    }
}