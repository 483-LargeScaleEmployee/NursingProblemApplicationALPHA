using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using CsvHelper;
using NursingProblemApplicationALPHA.Tests;
using Microsoft.Maui.Storage;
using System.ComponentModel;
using CsvHelper.Configuration.Attributes;


namespace NursingProblemApplicationALPHA
{
    public class EmployeeCSV
    {
        [Index(0)]
        public required string EID { get; set; }

        [Index(1)]
        public required string Department { get; set; }
        
        [Index(2)]
        public required string Role { get; set; }

        [Index(3)]
        public required string Days { get; set; }

        [Index(4)]
        public required string Shifts { get; set; }
    }
   

    public class CSV_Parser
    {
        public static Dictionary<string, Employee> employees = new Dictionary<string, Employee>();

        public static async Task<IList<Employee>> ParseCSVAsync()
        {
            Dictionary<String, Department> departments = DepartmentInitializer.InitializeDepartments();

            try
            {
                Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("schedule.csv");
                StreamReader reader = new StreamReader(fileStream);
                IList<String> lines = new List<String>();

                using (reader)
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) 
                    {
                        var records = csv.GetRecords<EmployeeCSV>();

                        foreach (var record in records)
                        {
                            String name = record.EID;
                            Department dept = departments[record.Department];
                            String role = record.Role;
                            String all_days = record.Days;
                            String all_shifts = record.Shifts;

                            if (employees.ContainsKey(name))
                            {
                                // Update existing employee
                                employees[name].UpdateEmployee(dept, all_days, all_shifts);
                                employees[name] = employees[name];
                            }
                            else
                            {
                                // Create new employee
                                employees[name] = new Employee(name, dept, role, all_days, all_shifts);
                                employees[name] = employees[name];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<Employee>();
            }

            return employees.Values.ToList<Employee>();
        }

        public static async Task<IList<Employee>> ParseCSVOldAsync()
        {
            //Initialize departments
            Dictionary<String, Department> departments = DepartmentInitializer.InitializeDepartments();

            //Sets the location for where the output csv is located
            //string outputFolderPath = Path.Combine(FileSystem.Current.AppDataDirectory, "employee_schedule.csv");
            //string filePath = outputFolderPath;


                
            using (var reader = new StreamReader(await FileSystem.Current.OpenAppPackageFileAsync("employee_schedule.csv")))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<EmployeeCSV>();

                    
                foreach (var employee in records)
                {
                    string name = employee.EID;
                    Department department = departments[employee.Department];
                    string role = employee.Role;
                    string day = employee.Days;
                    string shift = employee.Shifts;


                    if (employees.ContainsKey(name))
                    {
                        // Update existing employee
                        employees[name].UpdateEmployee(department, day, shift);


                        Console.WriteLine($"Updated {name} employee schedule ");
                    }
                    else
                    {
                        // Create new employee
                        employees[name] = new Employee(name, department, role, day, shift);

                    }


                }
                Console.WriteLine(employees);

                }
        
            return employees.Values.ToList();

            // Run the tests
            //EmployeeTests.RunTests(employees);
            //DepartmentTests.RunTests(departments);
        }
    }
}

