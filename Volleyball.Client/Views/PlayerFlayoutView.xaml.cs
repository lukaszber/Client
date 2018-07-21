using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Volleyball.Client.Views
{
    /// <summary>
    /// Interaction logic for PlayerFlayoutView.xaml
    /// </summary>
    public partial class PlayerFlayoutView : UserControl
    {
        public PlayerFlayoutView()
        {
            InitializeComponent();
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
