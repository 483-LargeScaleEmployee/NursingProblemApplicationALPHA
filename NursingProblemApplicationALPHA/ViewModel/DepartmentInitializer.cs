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


        //Sets colors for departments
        for (int i = 0; i < departmentNames.Count; i++)
        {
            //departments.Add(departmentName[, new Department(departmentName, randomColor)]);
            departments.Add(departmentNames[i], new Department(departmentNames[i], colors[i]));
        }


            return departments;
    }
    }
}