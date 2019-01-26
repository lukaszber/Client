using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using Volleyball.Client.Dialog;
using Volleyball.Client.Models;
using Volleyball.Client.ServiceConnection;

namespace Volleyball.Client.ViewModels
{
    public class TeamControlViewModel : ViewModelBase
    {

        private readonly IDialogService _dialogService;
        private readonly IVolleyManagmentConnetion _volleyManagmentConnetion;
        private Team _selectedTeam;
        private League _selectedLeague;

        public ICollection<Team> TeamDatas { get; private set; }
        public ICollection<Team> TeamList { get; }
        public ICollection<League> LeagueDatas { get; }
        public ICommand RemoveTeamData { get; set; }
        public ICommand Team { get; }
        public ICommand LeagueChangeCommand { get; private set; }
        public ICommand League { get; set; }

        public TeamControlViewModel(ICollection<Team> teamDatas,ICommand team,ICommand league,
            IDialogService dialogService, IVolleyManagmentConnetion volleyManagmentConnetion,
            ICollection<League> leagueDatas
            )
        {
            _dialogService = dialogService;
            _volleyManagmentConnetion = volleyManagmentConnetion;
            //PlayerDatas = playerDatas;
            LeagueDatas = leagueDatas;
            TeamList = teamDatas;
            Team = team;
            League = league;
            
            Initialize();

            ((INotifyCollectionChanged)teamDatas).CollectionChanged += OnTeamCollectionChanged;
        }


        private void Initialize()
        {
            TeamDatas = new ObservableCollection<Team>();
            RemoveTeamData = new RelayCommand(RemoveTeam, o => o != null);
            LeagueChangeCommand = new RelayCommand(_ => OnSelectedLeagueChange());

        }
        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                if(Equals(value,_selectedTeam))return;
                _selectedTeam = value;
                OnPropertyChanged();
            }
        }

        public League SelectedLeague
        {
            get => _selectedLeague;
            set
            {
                if (Equals(value, _selectedLeague)) return;
                _selectedLeague = value;
                OnPropertyChanged();
            }
        }

        private void OnSelectedLeagueChange()
        {
            TeamDatas.Clear();
            var teams = TeamList.Where(p => p.League.LeagueId == _selectedLeague?.LeagueId).ToList();
            foreach (var team in teams)
            {
                TeamDatas.Add(team);
            }
        }

        private async void RemoveTeam(object obj)
        {
            var result = await _dialogService.AskQuestionAsync("Delete Team",
                "Are you sure you want to delete this Team record?");
            if (result != MessageDialogResult.Affirmative) return;
            _volleyManagmentConnetion.RemoveTeam(SelectedTeam.TeamId);
            TeamList.Remove(SelectedTeam);
            SelectedTeam = null;
        }

        private void OnTeamCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TeamDatas.Clear();
            var teams = (ObservableCollection<Team>)sender;
            foreach (var team in teams.Where(p => p.League.LeagueId == _selectedLeague?.LeagueId).ToList())
            {
                TeamDatas.Add(team);
            }
        }
    }
}
