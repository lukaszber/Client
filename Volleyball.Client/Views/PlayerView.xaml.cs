using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Volleyball.Client.ViewModels;

namespace Volleyball.Client.Views
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        public PlayerView()
        {
            InitializeComponent();
        }

        private void Selector_OnLeagueChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (PlayerViewModel)DataContext;
            viewModel.CreateTeamDatas.Execute(null);
        }

        private void Selector_OnTeamChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (PlayerViewModel)DataContext;
            viewModel.CreatePlayerDatas.Execute(null);
        }
    }
}
