using System;
using System.Collections.Generic;


namespace ViewModel;
public class Department
{
    public string Name { get; set; }
    public string Color { get; set; }
    public HashSet<Employee> Employees { get; set; }
    public Dictionary<int, int> OptimalStaffing { get; private set; }  // Key: shift (1-3), Value: optimal staff count
    public Dictionary<int, HashSet<Employee>> Schedule { get; private set; }

    public Department(string name, string color)
    {
        Name = name;
        Color = color;
        Employees = new HashSet<Employee>();
        Schedule = Enumerable.Range(1, 42)
            .ToDictionary(i => i, _ => new HashSet<Employee>());
        OptimalStaffing = new Dictionary<int, int>();
    }

     public void SetOptimalStaffing(int shiftNumber, int staffCount)
    {
        OptimalStaffing[shiftNumber] = staffCount;
    }

    public string GetStaffingStatus(int shiftNumber)
    {
        if (!OptimalStaffing.ContainsKey(shiftNumber) || !Schedule.ContainsKey(shiftNumber))
            return "Unknown";

        int currentStaff = Schedule[shiftNumber].Count;
        int optimalStaff = OptimalStaffing[shiftNumber];

        if (currentStaff == optimalStaff)
            return "Optimally Staffed";
        else if (currentStaff < optimalStaff)
            return "Understaffed";
        else
            return "Overstaffed";
    }

    public string GetStatusColor(int shiftNumber)
    {
        string status = GetStaffingStatus(shiftNumber);
        return status switch
        {
            "Optimally Staffed" => "#28a745", // Green
            "Understaffed" => "#dc3545",      // Red
            "Overstaffed" => "#ffa500",       // Orange
            _ => "#6c757d"                    // Grey for unknown
        };
    }


    public void AddEmployeeToShift(Employee employee, int shiftNumber)
    {
        Schedule[shiftNumber].Add(employee);
        
    }

    public IEnumerable<Employee> GetEmployeesByRole(string role)
    {
        return Employees.Where(emp => emp.Role == role);
    }
} 
