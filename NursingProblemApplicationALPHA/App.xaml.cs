namespace NursingProblemApplicationALPHA;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).

    protected override Window CreateWindow(IActivationState activationState)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).

    {
        var window = base.CreateWindow(activationState);

        const int newWidth = 1090;
        const int newHeight = 675;
        
        window.Width = newWidth;
        window.Height = newHeight;

        return window;
    }
}
