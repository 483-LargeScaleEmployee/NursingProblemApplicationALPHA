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
            "ICU",
            "Psychiatry",
            "Gynecology",
            "Orthopedics"
        };

        // List of possible colors
        var colors = new List<Color>
        {
            Colors.Red,
            Colors.Teal,
            Colors.LightBlue,
            Colors.Green,
            Colors.LightYellow,
            Colors.Pink,
            Colors.Purple,
            Colors.Orange,
            Colors.LightGreen,
            Colors.PaleVioletRed,
        };


        //There is currently nothing that prevents departments from sharing the same color, need to fix
        foreach (var departmentName in departmentNames)
        {
            var randomColor = colors[new Random().Next(colors.Count)];
            departments.Add(departmentName, new Department(departmentName, randomColor));
        }
        

        return departments;
    }
    }
}