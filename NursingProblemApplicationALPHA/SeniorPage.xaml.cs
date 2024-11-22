using System.ComponentModel;
using System.Numerics;
using System.Windows.Input;
using ViewModel;

namespace NursingProblemApplicationALPHA;

public partial class SeniorPage : ContentPage
{
    MainViewModel mainViewModel;
    const double ROTATION_SPEED = 1;

    private readonly BindableProperty EmployeeListProperty = BindableProperty.Create(
        nameof(EmployeeList), 
        typeof(IList<Employee>), 
        typeof(SeniorPage), 
        new List<Employee>() {  }
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

    public IList<Department> DepartmentList
    {
        get => (IList<Department>)GetValue(DepartmentListProperty); 
        set => SetValue(DepartmentListProperty, value);
    }
    public IList<Employee> EmployeeList
    {
        get => (IList<Employee>)GetValue(EmployeeListProperty);
        set => SetValue(EmployeeListProperty, value);
    }

    public IList<String> RoleList
    {
        get => (IList<String>)GetValue(RoleListProperty);
        set => SetValue(RoleListProperty, value);
    }

    public SeniorPage(MainViewModel VM)
	{
            // mainViewModel used for getters/setters between here and MainViewModel.
            // BindingContext used for XAML/CS interactions
        mainViewModel = VM;
		BindingContext = VM;
		InitializeComponent();

        pickerFilter1.BindingContext = this;
        pickerFilter2.BindingContext = this;
        gridFilter1.BindingContext = this;
        gridFilter2.BindingContext = this;

        pickerFilter1.SetBinding(Picker.ItemsSourceProperty, "DepartmentList");
        pickerFilter1.ItemDisplayBinding = new Binding("Name");

        pickerFilter2.SetBinding(Picker.ItemsSourceProperty, "RoleList");

        Dispatcher.DispatchAsync(async () =>
        {
            EmployeeList = await CSV_Parser.ParseCSVAsync();
            EmployeeList = EmployeeList;
            OnPropertyChanged(nameof(EmployeeList));

        });



        // Hide Role selection until Department selected
        gridFilter2.IsVisible = false;
        
	}

    private void buttonRotateRight_Released(object sender, EventArgs e)
    {
            // Remove rotation delta
        mainViewModel.setDelta(Matrix4x4.CreateRotationY((float)(0.0 * (Math.PI / 180))));
    }

    private void buttonRotateRight_Pressed(object sender, EventArgs e)
    {
        // Apply rotation delta
        mainViewModel.setDelta(Matrix4x4.CreateRotationY((float)(ROTATION_SPEED * (Math.PI / 180))));
    }

    private void buttonRotateLeft_Pressed(object sender, EventArgs e)
    {
            // Apply rotation delta
        mainViewModel.setDelta(Matrix4x4.CreateRotationY((float)(-ROTATION_SPEED * (Math.PI / 180))));
    }

    private void buttonRotateLeft_Released(object sender, EventArgs e)
    {
        // Apply rotation delta
        mainViewModel.setDelta(Matrix4x4.CreateRotationY((float)(0.0 * (Math.PI / 180))));
    }

    private void pickerFilter2_SelectedIndexChanged(object sender, EventArgs e)
    {
            // Gather Employee List by Selected Role 
        String selectedRole = RoleList[pickerFilter2.SelectedIndex];
        //EmployeeList = DepartmentList[pickerFilter1.SelectedIndex].GetEmployeesByRole(selectedRole);

        foreach (Employee emp in EmployeeList)
        {
            foreach (Department dept in emp.Departments)
            {
                if ((emp.Role == selectedRole) && (dept.Name.Equals(DepartmentList[pickerFilter1.SelectedIndex].Name)))
                {
                    DepartmentList[pickerFilter1.SelectedIndex].addEmployee(emp);
                }
            }
        }


        mainViewModel.setCubeDepth(DepartmentList[pickerFilter1.SelectedIndex].Employees.Count);
    }

    private void pickerFilter1_SelectedIndexChanged(object sender, EventArgs e)
    {


            // Gather Roles scheduled for department
        //RoleList = DepartmentList[pickerFilter1.SelectedIndex].getAllRoles();
        
            // Show role selection list
        gridFilter2.IsVisible = true;
    }

    private void buttonUserBack_Clicked(object sender, EventArgs e)
    {

    }

    private void buttonUserFwd_Clicked(object sender, EventArgs e)
    {

    }
}