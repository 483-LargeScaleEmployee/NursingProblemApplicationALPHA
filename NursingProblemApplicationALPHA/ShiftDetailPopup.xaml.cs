
using CommunityToolkit.Maui.Views;

namespace NursingProblemApplicationALPHA
{
    public partial class ShiftDetailPopup : Popup
    {
        public ShiftDetailPopup(IEnumerable<string> employees)
        {
            InitializeComponent();

            if (!employees.Any())
            {
                EmployeeList.Children.Add(new Label { Text = "No employees scheduled", FontAttributes = FontAttributes.Italic });
            }
            else
            {
                foreach (var employee in employees)
                {
                    EmployeeList.Children.Add(new Label { Text = employee });
                }
            }
        }

        private void ClosePopup(object sender, EventArgs e)
        {
            Close();
        }
    }
}
