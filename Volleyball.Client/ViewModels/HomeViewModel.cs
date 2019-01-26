using Volleyball.Client.Views.MatchViews;

namespace Volleyball.Client.ViewModels
{
    public class HomeViewModel
    {
        public RelayCommand NewMatch { get; }
        public HomeViewModel()
        {
            NewMatch = new RelayCommand(OnNewMatch,null);
        }

        private static void OnNewMatch(object obj)
        {
            var newMatch = new StatisticMainView();
            newMatch.Show();
        }
        
    }
}
