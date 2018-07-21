using System;
using System.Collections.ObjectModel;
using Volleyball.Client.Models;
using Volleyball.Client.ServiceConnection;

namespace Volleyball.Client.ViewModels
{
    public class LeagueViewModel:ViewModelBase
    {
        private string _leagueName;
        private string _country;
        private IVolleyManagmentConnetion _volleyManagmentConnetion;
        private ObservableCollection<League> _leagueDatas;
        public RelayCommand OkCommand { get; }
        public RelayCommand AddLeague { get; set; }
        public LeagueViewModel(Action closeAction, ObservableCollection<League> leagueDatas, IVolleyManagmentConnetion volleyManagmentConnetion)
        {
            _leagueDatas = leagueDatas;
            _volleyManagmentConnetion = volleyManagmentConnetion;

            OkCommand = new RelayCommand(_ => closeAction());
            AddLeague = new RelayCommand(OnAddLeague,o=> !string.IsNullOrEmpty(_leagueName)&& !string.IsNullOrEmpty(_country));
        }



        public string LeagueName
        {
            get => _leagueName;
            set
            {
                if (value == _leagueName) return;
                _leagueName = value;
               OnPropertyChanged();
            }
        }

        public string Country
        {
            get => _country;
            set
            {
                if (value == _country) return;
                _country = value;
               OnPropertyChanged();
            }
        }

        private void OnAddLeague(object obj)
        {
            var league = new League(){Name = _leagueName, Country = _country };
            league.LeagueId = _volleyManagmentConnetion.AddNewLeague(league);
            _leagueDatas.Add(league);
            Country = string.Empty;
            LeagueName = string.Empty;
           
        }


    }
}
