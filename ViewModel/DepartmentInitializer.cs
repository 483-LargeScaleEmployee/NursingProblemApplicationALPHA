using System;
using System.Collections.Generic;

namespace ViewModel
{
    public static class DepartmentInitializer
    {
        public static Dictionary<string, Department> InitializeDepartments()
    {
        var departments = new Dictionary<string, Department>();
        
        // List of departments from CSV
        var departmentNames = new List<string>
        {
            "Pediatrics",
            "Neurology",
            "Radiology",
            "Oncology",
            "Emergency",
            "Cardiology",
            "Intensive Care Unit",
            "Psychiatry",
            "Gynecology",
            "Orthopedics"
        };

        // List of possible colors
        var colors = new List<string>
        {
            "#FF6B6B",  // Red
            "#4ECDC4",  // Teal
            "#45B7D1",  // Blue
            "#96CEB4",  // Green
            "#FEEAD",  // Yellow
            "#D4A5A5",  // Pink
            "#9B59B6"  
        };

        // Add your departments here
        foreach (var departmentName in departmentNames)
        {
            var randomColor = colors[new Random().Next(colors.Count)];
            departments.Add(departmentName, new Department(departmentName, randomColor));
        }
        
        return departments;
    }
    }
}