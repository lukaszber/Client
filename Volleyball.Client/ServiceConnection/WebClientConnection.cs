using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using Volleyball.Client.Models;

namespace Volleyball.Client.ServiceConnection
{
    public class WebClientConnection : IVolleyManagmentConnetion
    {
        private readonly RestClient _restClient;
        private readonly JsonDeserializer _deserializer = new JsonDeserializer();
        public WebClientConnection(RestClient restClient)
        {
            _restClient = restClient;
        }
        public List<Team> GetTeamList()
        {
            return Get<Team>("teams").ToList();
        }

        public void RemoveTeam(int teamId)
        {

            Delete("teams/", teamId);
        }

        public List<League> GetLeagueList()
        {
            return Get<League>("leagues").ToList();
        }

        public int AddNewTeam(Team teamData)
        {
            return Post("teams", teamData).TeamId;
        }

        public int AddNewLeague(League league)
        {
            return Post("leagues", league).LeagueId;

        }

        public List<Player> GetPlayers()
        {
            return Get<Player>("players").ToList();
        }

        public List<Player> GetPlayers(Team team)
        {
            return Get<Player>("players/team/" + team.TeamId).ToList();

        }

        public int AddNewPlayer(Player playerData)
        {
            return Post("players", playerData).Id;
        }

        public void RemovePlayer(int playerId)
        {
            Delete("leagues/",playerId);
        }

        private T Post<T>(string url,T data)
        {
            var request = new RestRequest(url, Method.POST);
            request.AddJsonBody(data);

            var response = _restClient.Execute(request);

            return _deserializer.Deserialize<T>(response);
        }

        private IEnumerable<T> Get<T>(string url)
        {
            var request = new RestRequest(url, Method.GET);

            var response = _restClient.Execute(request);

            return _deserializer.Deserialize<IEnumerable<T>>(response);
        }

        private void Delete(string url, int id)
        {
            var request = new RestRequest(url + id, Method.DELETE);

            _restClient.Execute(request);
        }
    }
}
