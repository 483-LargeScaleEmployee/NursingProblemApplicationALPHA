
using CommunityToolkit.Maui.Storage;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Windows.Gaming.Input.ForceFeedback;
using Windows.Media.AppBroadcasting;

namespace NursingProblemApplicationALPHA;

public partial class MainPage : ContentPage
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

    //Makes an array that contains shift information (42)
    private string _Name;
    public string Name
    {
        get => _Name;
        set
        {
            _Name = value;
            OnPropertyChanged(nameof(Name)); // Notify the UI that the property has changed
        }
    }

    //Makes an array that contains shift information (42)
    private int _EID;
    public int EID
    {
        get => _EID;
        set
        {
            _EID = value;
            OnPropertyChanged(nameof(EID)); // Notify the UI that the property has changed
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


	public MainPage()
	{
		InitializeComponent();
		BindingContext = this;  //Lets the XAML file access the property

        //Should be populated with the employee name/id parser info but for now test data is here
        //This should all move to something that is called when the employee name gets changed so the applicatio can live update
        var employee = new Employee("test", 1234);
        string department = "Department A";
        for(int shift = 0; shift < _shiftInfo.Length; shift++){

            Name = employee.Name;
            EID = employee.EID;
            //replace with actual data
            switch(shift % 3){
                case 0:
                    department = "Department Shift: " + (shift + 1);
                    _colorInfo[shift] = Color.FromArgb("#D9544D"); //red but easier to read
                    break;
                case 1:
                    department = "Department Shift: " + (shift + 1);
                    _colorInfo[shift] = Colors.LightBlue;
                    break;
                case 2:
                    department = "Department Shift: " + (shift + 1);
                    _colorInfo[shift] = Colors.LightGreen;
                    break;
                default:
                    break;
            }


            colorInfo = _colorInfo; //needs to be here for color to work
            employee.SetShift(shift,0, department);  //sets department into


        }

        //Sets all shift info when building an employee schedule
        //Might have to move where this is located when we give the option to change employees
        for (int i = 0; i < _shiftInfo.Length; i++){
                _shiftInfo[i] = employee.GetShift(i).Department;  //puts departmnet into shiftInfo
                
        }

        // Notify the UI that the property has changed, if needed
        
        OnPropertyChanged(nameof(shiftInfo));
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


	
    private async void ImportFile_Clicked(object sender, EventArgs e) //Button that lets you import files
    {

        //Prompts User with picking the input folder
        var inputFolder = await FolderPicker.PickAsync(default);

        if (inputFolder.IsSuccessful == false)
        {
            return; // Exit the method if the user canceled
        }

        string inputFolderPath = inputFolder.Folder.Path;


        //Sets the output folder so we always know where it is.
        //Output located @ NursingProblemApplicationALPHA\bin\Debug\net8.0-windows10.0.19041.0\win10-x64\AppX\Output
        string outputFolderPath = Path.Combine(AppContext.BaseDirectory, "Output");
        
        if (!Directory.Exists(outputFolderPath))
        {
            //if there is no output directory, create a new one
            Directory.CreateDirectory(outputFolderPath);
        }

        var processStartInfo = new ProcessStartInfo
        {
            //if new algo.exe is realsed, go to properties, then build action, then send to content
            //Set copy output directory to copy if newer
            //do this with both algo.exe & the .dll file that goes with it
            FileName = Path.Combine(AppContext.BaseDirectory, "linear algebra release 29", "algo.exe"),
            Arguments = $"\"{inputFolderPath}\" \"{outputFolderPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        //Runs algorithm
        using var process = new Process { StartInfo = processStartInfo };
        process.Start();

        //add something here that indicates that the program is loading, you can only tell by hovering your mouse over the top of the window right now
        process.WaitForExit();

    }
}
