using System;
using System.Collections.ObjectModel;
using Volleyball.Client.Enums;
using Volleyball.Client.Models;
using Volleyball.Client.ServiceConnection;

namespace Volleyball.Client.ViewModels
{
    public class PlayerFlayoutViewModel:ViewModelBase
    {
        private Position? _selectedPosition;
        private Team _selectedTeam;
        private string _name;
        private string _surname;
        private string _countryOfOrigin;
        private int? _number;
        private int? _age;
        private IVolleyManagmentConnetion _volleyManagmentConnetion;
        public RelayCommand OkCommand { get; }

        public RelayCommand AddPlayer { get; }
        
        public Array Positions { get; }
        public ObservableCollection<Team> TeamDatas { get; }

        public ObservableCollection<Player> PlayerDatas { get; }

        public PlayerFlayoutViewModel(Action closeAction,ObservableCollection<Team> teamData,
            ObservableCollection<Player> playerDatas,IVolleyManagmentConnetion volleyManagmentConnetion)
        {
            _volleyManagmentConnetion = volleyManagmentConnetion;
            Positions = Enum.GetValues(typeof(Position));
            TeamDatas = teamData;
            PlayerDatas = playerDatas;
            OkCommand = new RelayCommand(_ => closeAction());

            AddPlayer = new RelayCommand(AddNewPlayer, o => ValidatePlayerData());
        }


        private void AddNewPlayer(object obj)
        {
            
            var player = new Player() { Team = _selectedTeam, Name = _name,Surname = _surname,Age = (int) _age,
                Number = (int) _number,CountryOfOrigin = _countryOfOrigin,IsActive = true,Position = (Position) _selectedPosition};
            player.Id = _volleyManagmentConnetion.AddNewPlayer(player);

            SelectedPosition = null;
            SelectedTeam = null;
            Name = string.Empty;
            Surname = string.Empty;
            CountryOfOrigin = string.Empty;
            Age = null;
            Number = null;
            PlayerDatas.Add(player);
        }

        private bool ValidatePlayerData()
        {
            return _selectedPosition != null && _selectedTeam != null && _age != null
                   && _number != null && !string.IsNullOrEmpty(_surname) && !string.IsNullOrEmpty(_name)
                   && !string.IsNullOrEmpty(_countryOfOrigin);
        }

        public Position? SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                if (value == _selectedPosition) return;
                _selectedPosition = value;
                OnPropertyChanged();
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

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (value == _surname) return;
                _surname = value;
                OnPropertyChanged();
            }
        }
        public int? Number
        {
            get => _number;
            set
            {
                if (value == _number) return;
                _number = value;
                OnPropertyChanged();
            }
        }
        
        public string CountryOfOrigin
        {
            get => _countryOfOrigin;
            set
            {
                if (value == _countryOfOrigin) return;
                _countryOfOrigin = value;
                OnPropertyChanged();
            }
        }
        public int? Age
        {
            get => _age;
            set
            {
                if (value == _age) return;
                _age = value;
                OnPropertyChanged();
            }
        }
    }
}
