using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volleyball.Client.Views;

namespace Volleyball.Client.ViewModels
{
    public class HomeViewModel
    {
        public RelayCommand NewMatch { get; }
        public HomeViewModel()
        {
            NewMatch = new RelayCommand(OnNewMatch,null);
        }

        private void OnNewMatch(object obj)
        {
            var newMatch = new StatisticMainView();
            newMatch.Show();
        }
        
    }
}
