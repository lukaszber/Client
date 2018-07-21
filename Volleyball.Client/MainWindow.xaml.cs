using System.Windows;
using MahApps.Metro.Controls;
using RestSharp;
using Volleyball.Client.Dialog;
using Volleyball.Client.ServiceConnection;
using Volleyball.Client.ViewModels;

namespace Volleyball.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {

            var client = new RestClient("http://localhost:54866/api");
            DataContext = new MainWindowViewModel(new DialogService(this), new WebClientConnection(client)); 
        }

    }
}
