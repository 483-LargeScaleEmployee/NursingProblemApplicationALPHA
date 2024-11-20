using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace ViewModel
{
   public class Employee{


   // public int  { get; set; }
   public string Name { get; set; }


   public string Role { get; set; }

   public HashSet<Department> Departments { get; set; }
   public Dictionary<int, Department?> Schedule { get; private set; }






   public Employee(string name, Department department, string role, string day, string shift){
       Name = name;
       Role = role;
       Departments = new HashSet<Department> { department };
       Schedule = Enumerable.Range(1, 42)
           .ToDictionary(key => key, value => (Department?)null);


       GetSchedule(department, day, shift);
   }


   public void UpdateEmployee(Department department, string day, string shift)
   {
       Departments.Add(department);
       GetSchedule(department, day, shift);
   }


   private void GetSchedule(Department department, string dayStr, string shiftStr){
    
        List<int> days = dayStr.Trim('(', ')').Split(',').Select(int.Parse).ToList();
            List<string> shifts = shiftStr.Trim('(', ')').Split(',').Select(s => s.Trim()).ToList();
            //modified to remove the white space before " 000".

       // Console.WriteLine(days);
       // Console.WriteLine(shifts);
      


       // Fill in the schedule based on days and shifts
       for (int i = 0; i < days.Count; i++)
       {
           int day = days[i];
           string shift = shifts[i];
           // Console.WriteLine(shift);


           for (int j = 0; j < 3; j++) {
               if (shift[j] == '1') {
                   int shiftNumber = ((day - 1) * 3) + j + 1;
                   // Console.WriteLine(shiftNumber);


                   // Only update if the shift is not already scheduled
                   if (Schedule[shiftNumber] == null
                   || Schedule[shiftNumber] == department)
                   {
                       Schedule[shiftNumber] = department;
                       department.AddEmployeeToShift(this, shiftNumber);
                   }
                   else
                   {
                       throw new InvalidOperationException($"Shift conflict for employee {Name} on day {day}, shift {j + 1}");
                   }
               }
           }
   }


   }
}
}


