using System.ComponentModel;
using System.Numerics;
using System.Windows.Input;

namespace NursingProblemApplicationALPHA;

public partial class SeniorPage : ContentPage
{
    MainViewModel mainViewModel;
    const double ROTATION_SPEED = 0.8;

	public SeniorPage(MainViewModel VM)
	{
            // MVM is for code control
            // BindingContext is for Maui backend control
        mainViewModel = VM;
		BindingContext = VM;
		InitializeComponent();
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