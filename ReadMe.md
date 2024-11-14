# NSP Application

## Contributions Made by Tomisin Salu

### 1. Mock CSV Generation
- **CSV Generation:** Wrote a Python script to generate a mock CSV dataset according to finalized format, enabling the Application team to begin testing.

### 2. Employee Data Parsing
- **CSV Parsing:** Parsed and organized employee data from CSV files, storing data as strings with relevant variable names.
- **Employee Class Instantiation:** Created instances of the `Employee` class using parsed data.
- **Employee Dictionary:** Stored all `Employee` objects in an `Employees` dictionary, using employee names as unique identifiers (keys).
- **Handling Multiple Department Entries:** Developed logic to update schedules for employees working across multiple departments, handling multiple entries in the CSV.
- **Testing:** Wrote tests to validate CSV parsing, `Employee` class instantiation, and schedule updating logic.

### 3. Department Management
- **Department Class Creation:** Built the `Department` class, defining properties `name`, `color`, and `schedule`.
- **Department Instances:** Created and initialized instances for all 10 departments.
- **Shift Tracking:** Updated each department’s schedule whenever an employee is scheduled for a shift.
  - The department schedule is maintained as a dictionary, with keys for shifts (1 to 42) and values as sets containing employees scheduled for each shift.
  - This structure facilitates easy population of the department calendar on the department view page.
- **Testing:** Developed tests for the `Department` class to ensure accurate schedule updating and data handling.

## Next Steps
- **Employee View:** Populate the Employee calendar using data from the `Employees` dictionary.
- **Department View:** Populate the Department calendar using data from the `Departments` dictionary.
- **Staffing Analysis:** Collaborate with other subgroups to verify optimal staffing numbers for each shift.
  - If provided, add logic to assess shift staffing:
    - **Understaffed:** Marked in **Red**
    - **Optimally Staffed:** Marked in **Green**
    - **Overstaffed:** Marked in **Orange**
- **Department View Enhancements:** Implement hover functionality for popups on the department page.
- **Routing Implementation:** Set up routing for page navigation.
- **UI Optimization:** Optimize UI code to match the Figma design as closely as possible.
- **Testing:** Continue testing with larger mock inputs until real data inputs are available.

## MVVM File Structure
- **View:** Contains files related to the user interface.
- **ViewModel:** Holds files responsible for application logic and data manipulation.
- **Model:** Includes files related to data input and data models.
