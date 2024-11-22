using CommunityToolkit.Maui.Views;
using ViewModel;

namespace NursingProblemApplicationALPHA;

public partial class UpperManagementPage : ContentPage
{
    //Makes an array that contains shift information (42)
    private int[] _shiftInfo = new int[42];
    public int[] shiftInfo
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

        departments = CSV_Parser.departments;  //this works you dont need to recall csvReader
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
        
        if (result == null)
        {
            return;  //prevents crash from not choosing department
        }

        if (result is string departmentName)
        {
            _departmentName = departmentName;
            OnPropertyChanged(nameof(departmentName));
        }

        //now make it so it populates shiftinfo with how many people are in the department
        //then populate shiftcolor for whether its over, under or equal to the required amount

        var schedule = departments[_departmentName].Schedule;
        //var OptimalStaffing = departments[_departmentName].OptimalStaffing;
        int count = 0;

        foreach (var item in schedule)
        {
            int scheduleBlock = item.Key;
            var department = item.Value;

            shiftInfo[count] = department.Count;

            /* uncomment when optimalstaffing is implemented in department
            var optimalStaff = OptimalStaffing[count];

            //sets color depending on staffAmount
            if (department.Count < optimalStaff) //not enough scheduled
            {
                colorInfo[count] = Colors.Red;
            }
            else if(department.Count > optimalStaff) //too many scheduled
            {
                colorInfo[count] = Colors.Yellow;
            }
            else //optimal scheduled
            {
                colorInfo[count] = Colors.Green;
            }*/

            count++;

        }

        colorInfo = _colorInfo; //needs to be here for color to work
        OnPropertyChanged(nameof(shiftInfo));  //updates so that the shifts change

    }

}