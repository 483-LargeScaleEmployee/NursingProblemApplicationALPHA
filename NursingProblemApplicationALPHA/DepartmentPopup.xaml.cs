using CommunityToolkit.Maui.Views;

namespace NursingProblemApplicationALPHA;

public partial class DepartmentPopup : Popup
{

    public DepartmentPopup()
	{
		InitializeComponent();
    }

    private void Change_Department(object sender, EventArgs e)
    {
        //changes departmentname to the department chosen
        Close(((Microsoft.Maui.Controls.Button)sender).Text);
        

    }

}