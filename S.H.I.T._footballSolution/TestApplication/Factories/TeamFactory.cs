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

        //public static IEnumerable<Team> Create(IEnumerable<IEnumerable<Guid>> playerIds)
        //{
        //    if (playerIds == null)
        //        throw new Exception($"{nameof(playerIds)} is null");
        //    if (playerIds.Count() == 0)
        //        throw new Exception($"{nameof(playerIds)} is empty");
        //    foreach (var collection in playerIds)
        //    {
        //        if (collection == null)
        //            throw new Exception($"{nameof(collection)} is null");
        //        if (collection.Count() == 0)
        //            throw new Exception($"{nameof(collection)} is empty");
        //        if (collection.Count() < 24)
        //            throw new Exception($"Not enough Guid's in {nameof(collection)}");
        //        if (collection.Count() > 30)
        //            throw new Exception($"Too many Guid's in {nameof(collection)}");
        //    }

        //    List<Team> teams = new List<Team>();

        //    for (int i = 0; i < playerIds.Count(); i++)
        //    {
        //        GeneralName name = new GeneralName($"Team-{i + 1}");
        //        GeneralName homeArena = new GeneralName($"Arena-{i + 1}");
        //        Team team = new Team(name, homeArena);
        //        team.PlayerIds = playerIds.ElementAt(i) as List<Guid>;
        //        teams.Add(team);
        //    }

        //    return teams;
        //}
    }
}
