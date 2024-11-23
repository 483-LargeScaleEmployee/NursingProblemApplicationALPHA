using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;

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
                {"Night", 0}
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
            "ICU",
            "Psychiatry",
            "Gynecology",
            "Orthopedics"
        };

        // List of possible colors
        var colors = new List<Color>
        {
            Colors.Red,
            Colors.Teal,
            Colors.LightBlue,
            Colors.Green,
            Colors.LightYellow,
            Colors.Pink,
            Colors.Purple,
            Colors.Orange,
            Colors.LightGreen,
            Colors.PaleVioletRed,
        };


        //Sets colors for departments
        for (int i = 0; i < departmentNames.Count; i++)
        {
            //departments.Add(departmentName[, new Department(departmentName, randomColor)]);
            departments.Add(departmentNames[i], new Department(departmentNames[i], colors[i]));

            //Sets the OptimalStaffing for each department
            string inputLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NursingProblemApplication", "inputLocation.txt");
            StreamReader reader = new StreamReader(inputLocation);
            string departmentFolder;
            using (reader)
            {
                string filePath = reader.ReadLine();
                departmentFolder = Path.Combine(filePath, "departments");   //saves the location of the department folder which has all the department csv's
            }

            //This runs the code that should parse the correct csv OptimalStaffing            
            string departmentLocationCSV = departmentNames[i] + ".csv";
            string csvPath = Path.Combine(departmentFolder, departmentLocationCSV);
            if (File.Exists(csvPath))
            {
                    LoadOptimalStaffing(departments[departmentNames[i]], csvPath);
            }

        }



            return departments;
    }
    }
}