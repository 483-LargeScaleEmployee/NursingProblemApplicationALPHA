using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using CsvHelper;

namespace ViewModel
{
    public static class DepartmentInitializer
    {

        private static int CalculateShiftNumber(string day, string shift)
        {
            Dictionary<string, int> dayMapping = new()
            {
                {"Monday", 0},
                {"Tuesday", 3},
                {"Wednesday", 6},
                {"Thursday", 9},
                {"Friday", 12},
                {"Saturday", 15},
                {"Sunday", 18},
                {"Next Monday", 21},
                {"Next Tuesday", 24},
                {"Next Wednesday", 27},
                {"Next Thursday", 30},
                {"Next Friday", 33},
                {"Next Saturday", 36},
                {"Next Sunday", 39}
            };

            Dictionary<string, int> shiftMapping = new()
            {
                {"Morning", 1},
                {"Evening", 2},
                {"Night", 3}
            };

            return dayMapping[day] + shiftMapping[shift];
        }

        private static void LoadOptimalStaffing(Department department, string csvPath)
{
    Console.WriteLine($"Attempting to load CSV from: {csvPath}");
    try
    {
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<DepartmentsCSV>();

        foreach (var record in records)
        {
            int shiftNumber = CalculateShiftNumber(record.Day, record.Shift);
            int totalStaff = record.Nurse + record.Doctor + record.Admin;
            department.SetOptimalStaffing(shiftNumber, totalStaff);
            Console.WriteLine($"Added shift {shiftNumber} with {totalStaff} staff to {department.Name}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading optimal staffing for {department.Name}: {ex.Message}");
    }
}


        public static Dictionary<string, Department> InitializeDepartments()
    {
        var departments = new Dictionary<string, Department>();
        
        // List of departments from CSV
        var departmentNames = new List<string>
        {
            "Pediatrics",
            "Neurology",
            "Radiology",
            "Oncology",
            "Emergency",
            "Cardiology",
            "Intensive Care Unit",
            "Psychiatry",
            "Gynecology",
            "Orthopedics"
        };

        // List of possible colors
        var colors = new List<string>
        {
            "#FF6B6B",  // Red
            "#4ECDC4",  // Teal
            "#45B7D1",  // Blue
            "#96CEB4",  // Green
            "#FEEAD",  // Yellow
            "#D4A5A5",  // Pink
            "#9B59B6"  
        };

        // Add your departments here
        foreach (var departmentName in departmentNames)
        {
            var randomColor = colors[new Random().Next(colors.Count)];
            var department = new Department(departmentName, randomColor);
            departments.Add(departmentName, department);

            // Try to load department-specific CSV
                string csvPath = $"Model/Departments/{departmentName}.csv";
                if (File.Exists(csvPath))
                {
                    LoadOptimalStaffing(department, csvPath);
                }
            }
        
        return departments;

        
    }
    }
}
