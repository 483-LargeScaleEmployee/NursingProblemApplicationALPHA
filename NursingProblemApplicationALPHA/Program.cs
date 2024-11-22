using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using CsvHelper;

namespace ViewModel
{
   public class EmployeeCSV
   {


       public required string EmployeeName { get; set; }
       public required string Department { get; set; }
       public required string Role { get; set; }
       public required string Day { get; set; }
       public required string Shift { get; set; }
   }


   public class Program
   {
       public static Dictionary<string, Employee> employees = new Dictionary<string, Employee>();
       public static dynamic departments = DepartmentInitializer.InitializeDepartments();

        public static void ProgramMain()
       {
           //Initialize departments
            //var departments = DepartmentInitializer.InitializeDepartments();

            //Sets the location for where the output csv is located
            string outputFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NursingProblemApplication", "Output", "schedule.csv");
            string filePath = outputFolderPath;
          
           try
           {
               using (var reader = new StreamReader(filePath))
               using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
               {
                   var records = csv.GetRecords<EmployeeCSV>().ToList();

                   foreach (var employee in records)
                   {
                       string name = employee.EmployeeName;
                       Department department = departments[employee.Department];
                       string role = employee.Role;
                       string day = employee.Day;
                       string shift = employee.Shift;


                       if (employees.ContainsKey(name))
                       {
                           // Update existing employee
                           employees[name].UpdateEmployee(department, day, shift);


                           //Console.WriteLine($"Updated {name} employee schedule ");
                       }
                       else
                       {
                           // Create new employee
                           employees[name] = new Employee(name, department, role, day, shift);
                          
                       }


                   }
                   //Console.WriteLine(employees);
                  
               }
           }
           catch (Exception ex)
           {
               Console.WriteLine($"Error: {ex.Message}");
           }

            // Run the tests
            //EmployeeTests.RunTests(employees);
            //DepartmentTests.RunTests(departments);
       }
   }
}

                    