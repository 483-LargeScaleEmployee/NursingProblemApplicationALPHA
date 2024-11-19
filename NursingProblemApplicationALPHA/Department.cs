using System;
using System.Collections.Generic;


namespace NursingProblemApplicationALPHA;
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
}