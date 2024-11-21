using CommunityToolkit.Maui.Views;
using ViewModel;

namespace NursingProblemApplicationALPHA;

public partial class UpperManagementPage : ContentPage
{
    //Makes an array that contains shift information (42)
    private string[] _shiftInfo = new string[42];
    public string[] shiftInfo
    {
        get => _shiftInfo;
        set
        {
            _shiftInfo = value;
            OnPropertyChanged(nameof(shiftInfo)); // Notify the UI that the property has changed
        }
    }

    //Makes an array that contains department color info (42)
    private Color[] _colorInfo = new Color[42];
    public Color[] colorInfo
    {
        get => _colorInfo;
        set
        {
            _colorInfo = value;
            OnPropertyChanged(nameof(colorInfo)); // Notify the UI that the property has changed
        }
    }

    //Used for grid 1 Visablility
    private bool _gridOneBool = true;
    public bool GridOneBool
    {
        get => _gridOneBool;
        set
        {
            _gridOneBool = value;
            OnPropertyChanged(nameof(GridOneBool)); // Notify the UI that the property has changed
        }
    }

	//Used for grid 2 Visablility
    private bool _gridTwoBool = false;
    public bool GridTwoBool
    {
        get => _gridTwoBool;
        set
        {
            _gridTwoBool = value;
            OnPropertyChanged(nameof(GridTwoBool)); // Notify the UI that the property has changed
        }
    }

	//Used for NextImage Visablility
    private bool _NextVisable = true;
    public bool NextVisable
    {
        get => _NextVisable;
        set
        {
            _NextVisable = value;
            OnPropertyChanged(nameof(NextVisable)); // Notify the UI that the property has changed
        }
    }

	//Sets variable for ImageRotation
    private string _ImageRotation = "next.png";
    public string ImageRotation
    {
        get => _ImageRotation;
        set
        {
            _ImageRotation = value;
            OnPropertyChanged(nameof(ImageRotation)); // Notify the UI that the property has changed

        }
    }

    //change department in title
    private string _departmentName = "NONE";
    public string departmentName
    {
        get => _departmentName;
        set
        {
            _departmentName = value;
            OnPropertyChanged(nameof(departmentName)); // Notify the UI that the property has changed
        }
    }

    //department information is saved into this for easier access
    public static dynamic departments = DepartmentInitializer.InitializeDepartments();
    int testbreakpoint;

    public UpperManagementPage()
	{
		InitializeComponent();
        BindingContext = this;  //Lets the XAML file access the property

        departments = Program.departments;  //this works you dont need to recall csvReader
        testbreakpoint++;

        
    }

    private void NextButton_Clicked(object sender, EventArgs e) //changes which week is in view for 1 week view mode
    {
		GridOneBool = !GridOneBool;
		GridTwoBool = !GridTwoBool;
		if(ImageRotation == "next.png"){
			ImageRotation = "back.png";
		} else {
			ImageRotation = "next.png";
		}

    }

    private void WeekView_Clicked(object sender, EventArgs e)	//swaps between 1 week and 2 week view modes
    {
		if(GridOneBool == false|| GridTwoBool == false){
			GridOneBool = true;
			GridTwoBool = true;
			NextVisable = false;
		} else {
			GridOneBool = true;
			GridTwoBool = false;
			ImageRotation = "next.png"; //Prevents button state from getting desynced			
			NextVisable = true;

		}
    }
    private async void Change_Department(object sender, EventArgs e)	//Opens departmentPopup
    {
        var result = await this.ShowPopupAsync(new DepartmentPopup());
        //Should open a popup with department list

        if (result is string departmentName)
        {
            departmentName = departmentName;    //Changes Department title
            _departmentName = departmentName;
        }
    }

}