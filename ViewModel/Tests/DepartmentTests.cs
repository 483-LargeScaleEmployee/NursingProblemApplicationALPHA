using System;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel.Tests
{
    public static class DepartmentTests
    {
        public static void RunTests(Dictionary<string, Department> departments)
        {
            Console.WriteLine("\nRunning Department Tests...");
            
            TestDepartmentInitialization(departments);
            TestDepartmentColors(departments);
            TestScheduleInitialization(departments);
            TestEmployeeAssignments(departments);
            
            PrintDepartmentSchedules(departments);
            
            Console.WriteLine("Department Tests Completed.\n");
        }

        private static void TestDepartmentInitialization(Dictionary<string, Department> departments)
        {
            // Check if all expected departments exist
            var expectedDepartments = new[]
            {
                "Pediatrics", "Neurology", "Radiology", "Oncology", "Emergency",
                "Cardiology", "Intensive Care Unit", "Psychiatry", "Gynecology", "Orthopedics"
            };

            foreach (var dept in expectedDepartments)
            {
                if (!departments.ContainsKey(dept))
                {
                    Console.WriteLine($"❌ ERROR: Missing department: {dept}");
                    return;
                }
            }
            Console.WriteLine("✅ All departments initialized correctly");
        }

        private static void TestDepartmentColors(Dictionary<string, Department> departments)
        {
            // Check if all departments have valid color codes
            foreach (var dept in departments.Values)
            {
                if (string.IsNullOrEmpty(dept.Color) || !dept.Color.StartsWith("#"))
                {
                    Console.WriteLine($"❌ ERROR: Invalid color format for department {dept.Name}: {dept.Color}");
                    return;
                }
            }
            Console.WriteLine("✅ All department colors are valid");
        }

        private static void TestScheduleInitialization(Dictionary<string, Department> departments)
        {
            foreach (var dept in departments.Values)
            {
                // Check if schedule has correct number of shifts (42 = 14 days * 3 shifts)
                if (dept.Schedule.Count != 42)
                {
                    Console.WriteLine($"❌ ERROR: Department {dept.Name} has incorrect number of shifts: {dept.Schedule.Count}");
                    return;
                }

                // Check if all shift numbers are present
                for (int i = 1; i <= 42; i++)
                {
                    if (!dept.Schedule.ContainsKey(i))
                    {
                        Console.WriteLine($"❌ ERROR: Department {dept.Name} missing shift number {i}");
                        return;
                    }
                }
            }
            Console.WriteLine("✅ All department schedules properly initialized");
        }

        private static void TestEmployeeAssignments(Dictionary<string, Department> departments)
        {
            foreach (var dept in departments.Values)
            {
                foreach (var shift in dept.Schedule)
                {
                    // Check for null collections
                    if (shift.Value == null)
                    {
                        Console.WriteLine($"❌ ERROR: Null employee collection in {dept.Name} for shift {shift.Key}");
                        return;
                    }

                    // Check that employees in shifts belong to the department
                    foreach (var employee in shift.Value)
                    {
                        if (!employee.Departments.Contains(dept))
                        {
                            Console.WriteLine($"❌ ERROR: Employee {employee.Name} assigned to {dept.Name} but doesn't belong to department");
                            return;
                        }
                    }
                }
            }
            Console.WriteLine("✅ All employee assignments are valid");
        }

        private static void PrintDepartmentSchedules(Dictionary<string, Department> departments)
        {
            Console.WriteLine("\nDepartment Schedules:");
            Console.WriteLine("--------------------");

            foreach (var dept in departments.Values)
            {
                Console.WriteLine($"\nDepartment: {dept.Name}");
                Console.WriteLine($"Color: {dept.Color}");
                
                Console.WriteLine("\nSchedule:");
                Console.WriteLine("Day\tShift 1\tShift 2\tShift 3");
                for (int day = 1; day <= 14; day++)
                {
                    Console.Write($"Day {day}:\t");
                    for (int shift = 1; shift <= 3; shift++)
                    {
                        int shiftNumber = ((day - 1) * 3) + shift;
                        int employeeCount = dept.Schedule[shiftNumber].Count;
                        Console.Write($"{employeeCount}\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("--------------------");
            }
        }
    }
} 