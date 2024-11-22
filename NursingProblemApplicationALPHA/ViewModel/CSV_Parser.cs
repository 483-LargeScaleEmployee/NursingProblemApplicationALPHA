using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using CsvHelper;
using Microsoft.Maui.Storage;
using System.ComponentModel;
using CsvHelper.Configuration.Attributes;


namespace ViewModel
{
    public class EmployeeCSV
    {
        [Index(0)]
        public required string EmployeeName { get; set; }

        [Index(1)]
        public required string Department { get; set; }
        
        [Index(2)]
        public required string Role { get; set; }

        [Index(3)]
        public required string Days { get; set; }

        [Index(4)]
        public required string Shifts { get; set; }
    }

    public class DepartmentsCSV
    {

        [Name("Sprint Day")]
        public required string Day { get; set; }
        public required string Shift { get; set; }
        public required int Nurse { get; set; }
        public required int Doctor { get; set; }
        public required int Admin { get; set; }
    }

    public class CSV_Parser
    {
        public static Dictionary<string, Employee> employees = new Dictionary<string, Employee>();
        public static dynamic departments = DepartmentInitializer.InitializeDepartments();

        public static async Task<IList<Employee>> ParseCSVAsync()
        {
            //commented out to see if I can just make it public and have no issues at all
            //Dictionary<String, Department> departments = DepartmentInitializer.InitializeDepartments();

            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NursingProblemApplication", "Output", "schedule.csv"); ;
                var reader = new StreamReader(filePath);

                //Cant put new schedules in Raw folder after this is packaged so this method is un usable
                //Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("schedule.csv");
                //StreamReader reader = new StreamReader(fileStream);

                IList<String> lines = new List<String>();

                using (reader)
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) 
                    {
                        var records = csv.GetRecords<EmployeeCSV>();

                        foreach (var record in records)
                        {
                            String name = record.EmployeeName;
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
                    string name = employee.EmployeeName;
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

        }
    }
}

