using System;
using System.Collections.Generic;

namespace NursingProblemApplicationALPHA
{
    public static class DepartmentInitializer
    {
            // Modified for this git branch testing, integrate with real. 
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
            var colors = new List<Color>
            {
                Colors.Red,  // Red
                Colors.Teal,  // Teal
                Colors.Blue,  // Blue
                Colors.Green,  // Green
                Colors.Yellow,  // Yellow
                Colors.Pink,  // Pink
                Colors.Lavender,
                Colors.Orange,
                Colors.PowderBlue,
                Colors.SandyBrown
            };

            // Create each Department
            for (int i = 0; i < departmentNames.Count; i++)
            { 
                //departments.Add(departmentName[, new Department(departmentName, randomColor)]);
                departments.Add(departmentNames[i], new Department(departmentNames[i], colors[i]));
            }


            return departments;
        }
    }
}