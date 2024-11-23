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
   

    public class CSV_Parser
    {
        public static Dictionary<string, Employee> employees = new Dictionary<string, Employee>();
        public static dynamic departments = DepartmentInitializer.InitializeDepartments();

        public static IList<Employee> ParseCSV()
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
    }
}

