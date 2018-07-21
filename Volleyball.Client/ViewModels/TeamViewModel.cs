using System;
using System.Collections.ObjectModel;
using Volleyball.Client.Models;
using Volleyball.Client.ServiceConnection;

namespace Volleyball.Client.ViewModels
{
    public class TeamViewModel : ViewModelBase
    {
        private IVolleyManagmentConnetion _volleyManagmentConnetion;
        private League _selectedLeague;
        private string _teamName;
        public ObservableCollection<League> LeagueDatas { get; }
        private ObservableCollection<Team> _teamDatas;
        public RelayCommand OkCommand { get; }
        public RelayCommand AddTeam { get; }
        public Team TeamData = new Team();

        public TeamViewModel(Action closeAction, ObservableCollection<League> leagueDatas, IVolleyManagmentConnetion volleyManagmentConnetion,
            ObservableCollection<Team> teamDatas)
        {
            _teamDatas = teamDatas;
            _volleyManagmentConnetion = volleyManagmentConnetion;
            LeagueDatas = leagueDatas;
            OkCommand = new RelayCommand(_ => closeAction());
            AddTeam = new RelayCommand(AddNewTeam,o=> !string.IsNullOrEmpty(TeamName) && SelectedLeague != null);
        }

        public string TeamName
        {
            get => _teamName;
            set
            {
                if (value == _teamName) return;
                _teamName = value;
                TeamData.Name = _teamName;
                OnPropertyChanged();
            }
        }
        public League SelectedLeague
        {
            get => _selectedLeague;
            set
            {
                if (value == _selectedLeague) return;
                _selectedLeague = value;
                TeamData.League = _selectedLeague;
                OnPropertyChanged();
            }
        }

        private void AddNewTeam(object obj)
        {
            if(SelectedLeague == null || string.IsNullOrEmpty(TeamName)) return;
            var team = new Team(){League = SelectedLeague, Name = TeamName};
            team.TeamId =_volleyManagmentConnetion.AddNewTeam(team);
           
            SelectedLeague = null;
            TeamName = string.Empty;

            _teamDatas.Add(team);
        }

        
       
    }
}
