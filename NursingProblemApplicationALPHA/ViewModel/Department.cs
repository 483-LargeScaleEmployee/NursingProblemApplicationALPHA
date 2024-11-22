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
        AddOptimalStaffing(name);
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

    public void AddOptimalStaffing(string name)
    {
        //locates the input location where the departments folder is
        string inputLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NursingProblemApplication", "inputLocation.txt");

        //read the single line of the text document to get the actual filepath

        //departments should be the name of the folder inside here with all the staffing requirenemnts
        //just add the three roles together and populate the OptimalStaffing from 0-41 in the dict



    }
} 