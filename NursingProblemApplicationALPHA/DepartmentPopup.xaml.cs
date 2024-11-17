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
        Close("IT CHANGED");
        //Should open a popup with department list

    }

}