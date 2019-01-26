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
    public class PlayerViewModel : ViewModelBase
    {
        private ICollection<Player> _playerDatas;
        private IVolleyManagmentConnetion _volleyManagmentConnetion;
        private League _selectedLeague;
        private Team _selectedTeam;

        private Player _selectedPlayer;
        private IDialogService _dialogService;
        public ICommand PlayerFlayout { get; }
        public ObservableCollection<Player> PlayerDatas { get; set; }
        public ICollection<League> LeagueDatas { get; }

        public ObservableCollection<Team> TeamDatas { get; }
        public ICollection<Team> TeamList { get; set; }
        public RelayCommand CreateTeamDatas { get; set; }
        public RelayCommand CreatePlayerDatas { get; set; }

        public RelayCommand RemovePlayerData { get; set; }

        public PlayerViewModel(ICollection<Player> playerDatas, ICollection<League> leagueDatas,
            ICollection<Team> teamDatas, IVolleyManagmentConnetion volleyballService,
            ICommand playerFlayout
            , IDialogService dialogService)
        {
            _playerDatas = playerDatas;
            LeagueDatas = leagueDatas;
            TeamList = teamDatas;
            _volleyManagmentConnetion = volleyballService;
            PlayerFlayout = playerFlayout;
            _dialogService = dialogService;
            PlayerDatas = new ObservableCollection<Player>();
            TeamDatas = new ObservableCollection<Team>();
            ((INotifyCollectionChanged) TeamList).CollectionChanged += OnCreateTeamCommand;

            CreateTeamDatas = new RelayCommand(_ => OnCreateTeamCommand());

            CreatePlayerDatas = new RelayCommand(_ => OnCreatePlayerCommand());

            RemovePlayerData = new RelayCommand(RemovePlayer, o =>  o != null);
        }



        private void OnCreatePlayerCommand()
        {
            PlayerDatas.Clear();
            var players = _playerDatas?.Where(p => p.Team.TeamId == _selectedTeam?.TeamId).ToList();
            if (players == null) return;
            foreach (var player in players)
            {
                PlayerDatas.Add(player);
            }
        }

        public League SelectedLeague
        {
            get => _selectedLeague;
            set
            {
                if (value == _selectedLeague) return;
                _selectedLeague = value;
                OnPropertyChanged();
            }
        }

        private void OnCreateTeamCommand()
        {
            TeamDatas.Clear();

            var teams = TeamList.ToList().Where(l => l.League.LeagueId == _selectedLeague?.LeagueId).ToList();
            foreach (var team in teams)
            {
                TeamDatas.Add(team);
            }
        }

        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                if (value == _selectedTeam) return;
                _selectedTeam = value;
                OnPropertyChanged();
            }
        }

        public Player SelectedPlayer
        {
            get => _selectedPlayer;
            set
            {
                if (value == _selectedPlayer) return;
                _selectedPlayer = value;
                OnPropertyChanged();
            }
        }

        private async void RemovePlayer(object obj)
        {
            var result = await _dialogService.AskQuestionAsync("Delete Player",
                "Are you sure you want to delete this Player record?");
            if (result != MessageDialogResult.Affirmative) return;
            _volleyManagmentConnetion.RemovePlayer(SelectedPlayer.Id);
            _playerDatas.Remove(SelectedPlayer);
            SelectedTeam = null;
        }


        private void OnCreateTeamCommand(object sender, NotifyCollectionChangedEventArgs e)
        {
            TeamDatas.Clear();

            var teams = TeamList.ToList().Where(l => l.League.LeagueId == _selectedLeague?.LeagueId).ToList();
            foreach (var team in teams)
            {
                TeamDatas.Add(team);

            }
        }
    }
}
