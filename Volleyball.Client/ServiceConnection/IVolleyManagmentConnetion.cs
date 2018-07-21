using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volleyball.Client.Models;

namespace Volleyball.Client.ServiceConnection
{
    public interface IVolleyManagmentConnetion
    {
        List<Team> GetTeamList();
        void RemoveTeam(int teamId);
        List<League> GetLeagueList();
        int AddNewTeam(Team teamData);
        int AddNewLeague(League league);
        List<Player> GetPlayers();
        List<Player> GetPlayers(Team team);
        int AddNewPlayer(Player playerData);
        void RemovePlayer(int playerId);
    }
}
