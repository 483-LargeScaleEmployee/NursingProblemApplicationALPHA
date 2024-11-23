using CsvHelper.Expressions;
using System;
using System.Collections.Generic;


namespace ViewModel;
public class Department
{
    public string Name { get; set; }
    public Color Color { get; set; }
    public HashSet<Employee> Employees { get; set; }
    public Dictionary<int, int> OptimalStaffing { get; set; }  // Key: shift (1-3), Value: optimal staff count
    public Dictionary<int, HashSet<Employee>> Schedule { get; private set; }

    public Department(string name, Color color)
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

    public void AddEmployeeToShift(Employee employee, int shiftNumber)
    {
        Schedule[shiftNumber].Add(employee);
      
    }


    public IList<Employee> GetEmployeesByRole(string role)
    {
        return Employees.Where(emp => emp.Role == role).ToList();
    }

    public IList<String> getAllRoles()
    {
        IList<String> roles = new List<String>();
        foreach (Employee emp in Employees)
        {
            if (!roles.Contains(emp.Role))
            {
                roles.Add(emp.Role);
            }
        }

        return roles;
    }

    // Add a single Employee to the department
    public void addEmployee(Employee emp)
    {
        Employees.Add(emp);
    }

    // Remove a single Employee from department
    public void deleteEmployee(Employee emp)
    {
        Employees.Remove(emp);
    }

    public void addMultipleEmployees(IEnumerable<Employee> employees)
    {
        foreach (Employee emp in Employees)
        {
            Employees.Add(emp);
        }
    }

} 