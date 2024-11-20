
using CommunityToolkit.Maui.Storage;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using ViewModel;
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

    //sends Name to the UI
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

    //sends EID to the UI
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

    Dictionary<string, Employee> employees = new Dictionary<string, Employee>();
    int testbreakpoint;
    public MainPage()
	{
		InitializeComponent();
		BindingContext = this;  //Lets the XAML file access the property

        employees = Program.ProgramMain();
        testbreakpoint++;

        //Make a thing that sets a random employee as the current schedule on launch
        //Error trap it so if there is currently no generated schedule it doesn't crash.

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

    //This is what populates the schedule with the searched employee
    private void OnEntryCompleted(object sender, EventArgs e)
    {
        //This should basically just fill in the xaml
        string userInput = entry.Text;

        //error catch: exits function if name isn't in system
        if (!employees.ContainsKey(userInput))
        {
            Name = "Employee Not Found";
            //purges current employee
            for(int i = 0;i < 42; i++)
            {
                shiftInfo[i] = "Error";
                colorInfo[i] = Colors.White;
            }

            colorInfo = _colorInfo; //needs to be here for color to work
            OnPropertyChanged(nameof(shiftInfo));  //updates so that the shifts change
            return;
        }

        //sets name to user input
        Name = employees[userInput].Name;
        var schedule = employees[userInput].Schedule;
        int count = 0;

        //assigns schedule into the calender
        foreach (var item in schedule)
        {
            int scheduleBlock = item.Key;       // gets schedule block
            var department = item.Value;        // gets department info

            //sets color and department name
            if(department == null)
            {
                shiftInfo[count] = "None";
                colorInfo[count] = Colors.White;    //resets color if another schedule changed it
            }
            else
            {
                shiftInfo[count] = department.Name;
                colorInfo[count] = department.Color;
            }

            count++;
        }

        colorInfo = _colorInfo; //needs to be here for color to work
        OnPropertyChanged(nameof(shiftInfo));  //updates so that the shifts change

    }

    private async void ImportFile_Clicked(object sender, EventArgs e) //Button that lets you import files
    {

        //Prompts User with picking the input folder
        var inputFolder = await FolderPicker.PickAsync(default);

        //Exits if the user does not select an input folder
        if (inputFolder.IsSuccessful == false)
        {
            return;
        }

        string inputFolderPath = inputFolder.Folder.Path;


        //Sets the output folder so we always know where it is.
        //Output located @ MyDoccuments/NursingProblemApplication/Output
        string outputFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NursingProblemApplication","Output" );

        if (!Directory.Exists(outputFolderPath))
        {
            //if there is no output directory, create a new one
            Directory.CreateDirectory(outputFolderPath);
        }

        var processStartInfo = new ProcessStartInfo
        {
            //if new algo.exe is realsed, go to properties, then build action, then select content
            //Set copy output directory to copy if newer
            //do this with both algo.exe & the .dll file that goes with it
            FileName = Path.Combine(AppContext.BaseDirectory, "linear algebra release 59", "algo.exe"),
            Arguments = $"\"{inputFolderPath}\" \"{outputFolderPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        //Runs algorithm
        using var process = new Process { StartInfo = processStartInfo };
        process.Start();

        //add something here that indicates that the program is loading
        //add something that tells the user if the algorithm didn't run for whatever reason
        process.WaitForExit();

        //runs the output and updates employees
        employees = Program.ProgramMain();

    }
}

