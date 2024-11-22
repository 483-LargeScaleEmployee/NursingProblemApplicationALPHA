using System.ComponentModel;
using System.Numerics;
using System.Windows.Input;
using ViewModel;

namespace NursingProblemApplicationALPHA;

public partial class SeniorPage : ContentPage
{
    const int NUM_SHIFTS = 3;
    const int NUM_DAYS = 14;

    private readonly BindableProperty MasterEmployeeListProperty = BindableProperty.Create(
        nameof(MasterEmployeeList),
        typeof(IList<Employee>),
        typeof(SeniorPage),
        new List<Employee>() { }
    );

    private readonly BindableProperty EmployeesByRoleProperty = BindableProperty.Create(
        nameof(EmployeesByRole),
        typeof(IList<Employee>),
        typeof(SeniorPage),
        new List<Employee>() { }
    );

    private readonly BindableProperty RoleListProperty = BindableProperty.Create(
        nameof(RoleList),
        typeof(IList<String>),
        typeof(SeniorPage),
        new List<String>() { "Admin", "Nurse", "Doctor" }
    );

    private readonly BindableProperty DepartmentListProperty = BindableProperty.Create(
        nameof(DepartmentList),
        typeof(IList<Department>),
        typeof(SeniorPage),
        DepartmentInitializer.InitializeDepartments().Values.ToList()
    );

    private readonly BindableProperty ColorInfoProperty = BindableProperty.Create(
        nameof(ColorInfo),
        typeof(IList<Color>),
        typeof(SeniorPage),
        new List<Color>()
    );

    private readonly BindableProperty SelectedEmployeeProperty = BindableProperty.Create(
        nameof(SelectedEmployee),
        typeof(Employee),
        typeof(SeniorPage),
        null
    );

    public IList<Department> DepartmentList
    {
        get => (IList<Department>)GetValue(DepartmentListProperty);
        set => SetValue(DepartmentListProperty, value);
    }
    public IList<Employee> MasterEmployeeList
    {
        get => (IList<Employee>)GetValue(MasterEmployeeListProperty);
        set => SetValue(MasterEmployeeListProperty, value);
    }
    public IList<Employee> EmployeesByRole
    {
        get => (IList<Employee>)GetValue(EmployeesByRoleProperty);
        set => SetValue(EmployeesByRoleProperty, value);
    }

    public IList<String> RoleList
    {
        get => (IList<String>)GetValue(RoleListProperty);
        set => SetValue(RoleListProperty, value);
    }

    public IList<Color> ColorInfo
    {
        get => (IList<Color>)GetValue(ColorInfoProperty);
        set => SetValue(ColorInfoProperty, value);
    }

    public Employee SelectedEmployee
    {
        get => (Employee)GetValue(SelectedEmployeeProperty);
        set => SetValue(SelectedEmployeeProperty, value);
    }

    public SeniorPage()
    {
        BindingContext = this;
        InitializeComponent();

        pickerFilter1.SetBinding(Picker.ItemsSourceProperty, "DepartmentList");
        pickerFilter1.ItemDisplayBinding = new Binding("Name");


        pickerFilter2.SetBinding(Picker.ItemsSourceProperty, "RoleList");

        Dispatcher.DispatchAsync(async () =>
        {
            MasterEmployeeList = await CSV_Parser.ParseCSVAsync();
            OnPropertyChanged(nameof(MasterEmployeeList));

            EmployeesByRole = MasterEmployeeList;
        });

        for (int i = 0; i < (NUM_DAYS * NUM_SHIFTS); i++)
        {
            ColorInfo.Add(Colors.White);
        }


        // Hide Role selection until Department selected
        gridFilter2.IsVisible = false;
        listEmployees.IsVisible = false;
        labelEmployeeList.IsVisible = false;

    }

    private void fillColorInfo(Employee selectedEmployee)
    {
        if (selectedEmployee == null)
        {
            return;
        }

        var schedule = selectedEmployee.Schedule;
        int count = 0;

        ColorInfo.Clear();

        foreach (var item in schedule)
        {
            int scheduleBlock = item.Key;
            var dept = item.Value;

            // Employee not scheduled
            if (dept == null)
            {
                //ColorInfo[count] = Colors.White;
                ColorInfo.Add(Colors.Gray);
            }

            // If Role selected
            else if (pickerFilter2.SelectedIndex != -1)
            {
                // Employee scheduled for selected department and role
                if (dept.Name.Equals(DepartmentList[pickerFilter1.SelectedIndex].Name) && selectedEmployee.Role.Equals(RoleList[pickerFilter2.SelectedIndex]))
                {
                    //ColorInfo[count] = Colors.Green;
                    ColorInfo.Add(Colors.Green);
                }
                // Employee scheduled in other department in same role
                else if (!dept.Name.Equals(DepartmentList[pickerFilter1.SelectedIndex].Name) && selectedEmployee.Role.Equals(RoleList[pickerFilter2.SelectedIndex]))
                {
                    //ColorInfo[count] = Colors.Blue;
                    ColorInfo.Add(Colors.Blue);
                }

                // Employee scheduled in selected department but different role
                else if (dept.Name.Equals(DepartmentList[pickerFilter1.SelectedIndex].Name))
                {
                    ColorInfo.Add(Colors.LightGreen);
                }
                // Employee scheduled in different department in different role
                else if (!dept.Name.Equals(DepartmentList[pickerFilter1.SelectedIndex].Name))
                {
                    ColorInfo.Add(Colors.SkyBlue);
                }
            }

            // Redundant to the last 2 conditionals in prior else if, but incase SelectedIndex is -1.
            // Otherwise you'll get IndexOutOfBounds exceptions.
            else if (pickerFilter2.SelectedIndex == -1)
            {
                // Employee scheduled in selected department but different role
                if (dept.Name.Equals(DepartmentList[pickerFilter1.SelectedIndex].Name))
                {
                    ColorInfo.Add(Colors.LightGreen);
                }
                // Employee scheduled in different department in different role
                else if (!dept.Name.Equals(DepartmentList[pickerFilter1.SelectedIndex].Name))
                {
                    ColorInfo.Add(Colors.SkyBlue);
                }
            }
            else
            {
                ColorInfo.Add(Colors.Gray);
            }

            count++;
            OnPropertyChanged(nameof(ColorInfo));
        }
    }

    private void pickerFilter2_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Gather Employee List by Selected Role 
        String selectedRole = RoleList[pickerFilter2.SelectedIndex];
        //EmployeeList = DepartmentList[pickerFilter1.SelectedIndex].GetEmployeesByRole(selectedRole);

        foreach (Employee emp in MasterEmployeeList)
        {
            foreach (Department dept in emp.Departments)
            {
                if ((emp.Role == selectedRole) && (dept.Name.Equals(DepartmentList[pickerFilter1.SelectedIndex].Name)))
                {
                    DepartmentList[pickerFilter1.SelectedIndex].addEmployee(emp);
                }
            }
        }

        EmployeesByRole = DepartmentList[pickerFilter1.SelectedIndex].GetEmployeesByRole(selectedRole);
        OnPropertyChanged(nameof(EmployeesByRole));

        if (listEmployees.SelectedItem != null)
        {

        }
        fillColorInfo(SelectedEmployee);

    }

    private void pickerFilter1_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (Employee emp in MasterEmployeeList)
        {
            foreach (Department dept in emp.Departments)
            {
                if (dept.Name.Equals(DepartmentList[pickerFilter1.SelectedIndex].Name))
                {
                    DepartmentList[pickerFilter1.SelectedIndex].addEmployee(emp);
                }
            }
        }
        EmployeesByRole = DepartmentList[pickerFilter1.SelectedIndex].Employees.ToList<Employee>();
        //listEmployees.SelectedItem = EmployeesByRole[0];
        if (SelectedEmployee != null)
        {
            fillColorInfo(SelectedEmployee);
        }


        gridFilter2.IsVisible = true;
        listEmployees.IsVisible = true;
        labelEmployeeList.IsVisible = true;
    }


    private void listEmployees_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        SelectedEmployee = e.SelectedItem as Employee;
        fillColorInfo(SelectedEmployee);
        OnPropertyChanged(nameof(SelectedEmployee));
    }
}