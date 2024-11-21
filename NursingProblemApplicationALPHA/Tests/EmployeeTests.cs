using System;
using System.Linq;
using System.Collections.Generic;

namespace NursingProblemApplicationALPHA.Tests
{
    public class EmployeeTests
    {
        public static void RunTests(Dictionary<string, Employee> employees)
        {
            Console.WriteLine("\nTesting Employee Data:");
            Console.WriteLine("----------------------");

            // var limitedEmployees = employees.Take(3).ToDictionary(x => x.Key, x => x.Value);
            // employees = limitedEmployees;
            // employees = employees.Where(x => x.Key == "1")
            //                    .ToDictionary(x => x.Key, x => x.Value);

            foreach (var kvp in employees)
            {
                Employee emp = kvp.Value;
                Console.WriteLine($"\nEmployee: {emp.Name}");
                Console.WriteLine($"Role: {emp.Role}");
                Console.WriteLine("Departments: " + string.Join(", ", emp.Departments.Select(d => d.Name)));

                Console.WriteLine("\nSchedule:");
                Console.WriteLine("Day\tShift 1\tShift 2\tShift 3");
                for (int day = 1; day <= 14; day++)
                {
                    Console.Write($"Day {day}:\t");
                    for (int shift = 1; shift <= 3; shift++)
                    {
                        int shiftNumber = ((day - 1) * 3) + shift;
                        Console.Write(emp.Schedule[shiftNumber]?.Name + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("----------------------");
            }
        }
    }
}