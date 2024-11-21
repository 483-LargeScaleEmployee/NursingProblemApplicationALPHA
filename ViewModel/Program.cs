using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using CsvHelper;
using ViewModel.Tests;
using CsvHelper.Configuration.Attributes;

using NursingProblemApplicationALPHA;


namespace ViewModel
{
   public class EmployeeCSV
   {


       public required string Name { get; set; }
       public required string Department { get; set; }
       public required string Role { get; set; }
       public required string Day { get; set; }
       public required string Shift { get; set; }
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


   public class Program
   {
       public static Dictionary<string, Employee> employees = new Dictionary<string, Employee>();
       public static Dictionary<string, Department> departments { get; private set; }

       public static void Main(string[] args)
       {
           //Initialize departments
            departments = DepartmentInitializer.InitializeDepartments();
           
           string filePath = "Model/employee_schedule.csv";
          
           try
           {
               using (var reader = new StreamReader(filePath))
               using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
               {
                   var records = csv.GetRecords<EmployeeCSV>();
                
                   // Console.WriteLine("------------------------");
                   // Console.WriteLine(employees);


                   foreach (var employee in records)
                   {
                       string name = employee.Name;
                       Department department = departments[employee.Department];
                       string role = employee.Role;
                       string day = employee.Day;
                       string shift = employee.Shift;


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
           }
           catch (Exception ex)
           {
               Console.WriteLine($"Error: {ex.Message}");
           }


           // Run the tests
           EmployeeTests.RunTests(employees);
           DepartmentTests.RunTests(departments);
       }
   }
}

                    
