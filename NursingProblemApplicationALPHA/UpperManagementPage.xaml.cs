namespace NursingProblemApplicationALPHA;

public partial class UpperManagementPage : ContentPage
{
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


	public UpperManagementPage()
	{
		InitializeComponent();
		BindingContext = this;  //Lets the XAML file access the property
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
		var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
		{
			{DevicePlatform.WinUI, new[] {".csv"}} //This should only let you import csv files
		
		});



		var result = await FilePicker.PickAsync(new PickOptions
		{
			PickerTitle = "Import CSV",
			FileTypes = customFileType
		});

		if (result == null){
			return;
		}

		var stream = await result.OpenReadAsync();  //I dont know what this line does for sure, I think it reads the file

    }

}