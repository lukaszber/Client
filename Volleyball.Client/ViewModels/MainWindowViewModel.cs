using System.Collections.ObjectModel;
using System.Linq;
using Volleyball.Client.Dialog;
using Volleyball.Client.Models;
using Volleyball.Client.ServiceConnection;

namespace Volleyball.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private IDialogService _dialogService;
        private IVolleyManagmentConnetion _volleyManagmentConnetion;
        private bool _isTeamFlyoutOpen;
        private bool _isLeagueFlyoutOpen;
        private bool _isPlayerFlyoutOpen;
        private int _selectedTabIndex;

        public TeamViewModel TeamViewModel { get; set; }
        public LeagueViewModel LeagueViewModel { get; set; }

        public HomeViewModel HomeViewModel { get; set; }
        public TeamControlViewModel TeamControlViewModel { get; set; }
        public PlayerFlayoutViewModel PlayerFlayoutViewModel { get; set; }
        public PlayerViewModel PlayerViewModel { get; set; }
        public ObservableCollection<Team> TeamDatas { get; private set; }
        public ObservableCollection<League> LeagueDatas { get; private set; }
        public ObservableCollection<Player> PlayerDatas { get; private set; }


        public RelayCommand Team { get; set; }
        public RelayCommand League { get; set; }
        public RelayCommand PlayerFlayout { get; set; }

        public MainWindowViewModel(IDialogService dialogService, IVolleyManagmentConnetion volleyManagmentConnetion)
        {
            _dialogService = dialogService;
            _volleyManagmentConnetion = volleyManagmentConnetion;
            Initializate();
        }

        private void Initializate()
        {
            LeagueDatas = new ObservableCollection<League>(_volleyManagmentConnetion.GetLeagueList());
            TeamDatas = new ObservableCollection<Team>(_volleyManagmentConnetion.GetTeamList());
            PlayerDatas = new ObservableCollection<Player>(_volleyManagmentConnetion.GetPlayers());
            Team = new RelayCommand(_ => OnTeamCommand());
            League = new RelayCommand(_ => OnLeagueCommand());
            PlayerFlayout= new RelayCommand(_=> OnPlayerCommand());
            HomeViewModel = new HomeViewModel();
            LeagueViewModel = new LeagueViewModel(() => IsLeagueFlyoutOpen = false, LeagueDatas, _volleyManagmentConnetion);
            PlayerFlayoutViewModel = new PlayerFlayoutViewModel(()=>IsPlayerFlyoutOpen = false,TeamDatas,PlayerDatas,_volleyManagmentConnetion); 
            TeamViewModel = new TeamViewModel(() => IsTeamFlyoutOpen = false, LeagueDatas,_volleyManagmentConnetion,TeamDatas);
            TeamControlViewModel = new TeamControlViewModel(TeamDatas,Team, League, _dialogService,_volleyManagmentConnetion,LeagueDatas);
            PlayerViewModel = new PlayerViewModel(PlayerDatas,LeagueDatas,TeamDatas, _volleyManagmentConnetion,PlayerFlayout, _dialogService);
        }



        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                if (value == _selectedTabIndex) return;
                _selectedTabIndex = value;
                OnPropertyChanged();
            }
        }

        public bool IsTeamFlyoutOpen
        {
            get => _isTeamFlyoutOpen;
            set
            {
                if (value.Equals(_isTeamFlyoutOpen)) return;
                _isTeamFlyoutOpen = value;
                OnPropertyChanged();
            }
        }

        public bool IsLeagueFlyoutOpen
        {
            get => _isLeagueFlyoutOpen;
            set
            {
                if (value.Equals(_isLeagueFlyoutOpen)) return;
                _isLeagueFlyoutOpen = value;
                OnPropertyChanged();
            }
        }
        public bool IsPlayerFlyoutOpen
        {
            get => _isPlayerFlyoutOpen;
            set
            {
                if (value.Equals(_isPlayerFlyoutOpen)) return;
                _isPlayerFlyoutOpen = value;
                OnPropertyChanged();
            }
        }

        

        private void OnTeamCommand()
        {
            IsTeamFlyoutOpen = true;
        }

        private void OnLeagueCommand()
        {
            IsLeagueFlyoutOpen = true;
        }

        private void OnPlayerCommand()
        {
            IsPlayerFlyoutOpen = true;
        }

    }
}
