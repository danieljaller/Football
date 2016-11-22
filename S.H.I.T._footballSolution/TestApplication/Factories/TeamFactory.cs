using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication.Factories
{
    public static class TeamFactory
    {
        public static Team Create(string name, string homeArena, IEnumerable<Guid> playerIds)
        {
            if (playerIds == null)
                throw new Exception($"{nameof(playerIds)} is null");
            if (playerIds.Count() == 0)
                throw new Exception($"{nameof(playerIds)} is empty");
            if (playerIds.Count() < 24)
               throw new Exception($"Not enough Guid's in {nameof(playerIds)}");
            if (playerIds.Count() > 30)
                throw new Exception($"Too many Guid's in {nameof(playerIds)}");
            
            GeneralName _name = new GeneralName(name);
            GeneralName _homeArena = new GeneralName(homeArena);

            Team team = new Team(_name, _homeArena);
            team.PlayerIds = playerIds as List<Guid>;

            return team;
        }

        public static List<Team> CreateTeamsAndSetPlayersTeamId(List<List<Player>> playersLists)
        {
            if (playersLists == null)
                throw new ArgumentNullException($"{nameof(playersLists)} is null");
            if (playersLists.Count() == 0)
                throw new ArgumentException($"{nameof(playersLists)} is empty");
            foreach (var list in playersLists)
            {
                if (list == null)
                    throw new ArgumentNullException($"{nameof(list)} is null");
                if (list.Count() == 0)
                    throw new ArgumentException($"{nameof(list)} is empty");
                if (list.Count() < 24)
                    throw new ArgumentException($"Not enough Player's in {nameof(list)}");
                if (list.Count() > 30)
                    throw new ArgumentException($"Too many Player's in {nameof(list)}");
            }

            List<Team> teams = new List<Team>();

            for (int i = 0; i < playersLists.Count(); i++)
            {
                GeneralName name = new GeneralName($"Team-{i + 1}");
                GeneralName homeArena = new GeneralName($"Arena-{i + 1}");
                Team team = new Team(name, homeArena);
                //team.PlayerIds = playersLists.ElementAt(i).Select(p => p.Id) as List<Guid>;
                List<Guid> playerIds = new List<Guid>();
                foreach (Player player in playersLists[i])
                {
                    player.TeamId = team.Id;
                    playerIds.Add(player.Id);
                }
                team.PlayerIds = playerIds;
                teams.Add(team);
            }

            return teams;
        }
    }
}
