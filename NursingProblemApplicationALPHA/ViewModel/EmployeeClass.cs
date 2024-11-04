using System;
using System.Collections.Generic;

public class Employee{
    public string Name { get; set; }
    public int EID { get; set; }
    public Dictionary<int, List<object>> Schedule { get; }

    public Employee(string name, int eid){
        Name = name;
        EID = eid;
        Schedule = new Dictionary<int, List<object>>();
        
        // Initialize schedule for shift 1 to 42
        for (int shift = 0; shift <= 41; shift++) //every 3 shifts = 1 day
        {
            Schedule[shift] = new List<object> { null, null }; // Shift and Department
        }
    }

    // Method to set a shift
    public void SetShift(int shift, int? shiftNumber, string department){
    if (shift < 0 || shift > 41)
        throw new ArgumentException("Shift must be between 1 and 41");

    Schedule[shift][0] = shiftNumber;   //I changed this work work on a 42 shift workweek instead of 14 days
    Schedule[shift][1] = department;    //since we need to be able to work multiple shifts on the same day
                                        //I dont know if we need shift numbers anymore so we can put something else here instead
}

// Method to get a shift
public (int? ShiftNumber, string Department) GetShift(int shift){
    if (shift < 0 || shift > 41)
        throw new ArgumentException("Shift must be between 1 and 41");

    return ((int?)Schedule[shift][0], (string)Schedule[shift][1]);
}
    }

