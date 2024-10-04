


namespace NursingProblemApplicationALPHA;


public partial class MainPage : ContentPage
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


	public MainPage()
	{
		InitializeComponent();
		BindingContext = this;  //Lets the XAML file access the property
	}

    private void NextButton_Clicked(object sender, EventArgs e)
    {
		GridOneBool = !GridOneBool;
		GridTwoBool = !GridTwoBool;
    }

}

