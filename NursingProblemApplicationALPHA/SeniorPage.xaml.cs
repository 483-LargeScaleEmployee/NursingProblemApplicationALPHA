using System.ComponentModel;
using System.Numerics;
using System.Windows.Input;

namespace NursingProblemApplicationALPHA;

public partial class SeniorPage : ContentPage
{
    MainViewModel mainViewModel;
    const double ROTATION_SPEED = 1;

    private readonly BindableProperty DeptListProperty = BindableProperty.Create(
        nameof(DeptList), 
        typeof(IList<String>), 
        typeof(SeniorPage), 
        new List<String>() { "Oncology", "Emergency", "Pediatrics", "Orthopedics", "Neurology", "Psychiatry", "Gynecology", "ICU", "Radiology", "Cardiology" }
    );

    private readonly BindableProperty RoleListProperty = BindableProperty.Create(
        nameof(RoleList),
        typeof(IList<String>),
        typeof(SeniorPage),
        new List<String>() { "Admin", "Nurse", "Doctor" }
    );

    public IList<String> DeptList
    {
        get => (IList<String>)GetValue(DeptListProperty);
        set => SetValue(DeptListProperty, value);
    }

    public IList<String> RoleList
    {
        get => (IList<String>)GetValue(RoleListProperty);
        set => SetValue(RoleListProperty, value);
    }

    public SeniorPage(MainViewModel VM)
	{
            // MVM is for code control
            // BindingContext is for Maui backend control
        mainViewModel = VM;
		BindingContext = VM;
		InitializeComponent();

        pickerFilter1.BindingContext = this;
        pickerFilter2.BindingContext = this;
        
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

}