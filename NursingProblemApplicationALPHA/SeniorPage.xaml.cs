using System.ComponentModel;
using System.Numerics;
using System.Windows.Input;

namespace NursingProblemApplicationALPHA;

public partial class SeniorPage : ContentPage
{
	public SeniorPage(MainViewModel VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}