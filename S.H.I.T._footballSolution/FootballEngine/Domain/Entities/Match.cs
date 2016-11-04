using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities
{
    public class Match
    {
        public Guid Id { get; set; }
        public GeneralName Location { get; }
        public Guid HomeTeamId { get; set; }
        public Guid VisitorTeamId { get; set; }
        public DateTime Date { get; set; }
        public MatchGoals[] Result { get; set; }
        public MatchGoals HomeGoals { get; set; }
        public MatchGoals VisitorGoals { get; set; }
        public List<Guid> HomeLineup { get; set; }
        public List<Guid> VisitorLineup { get; set; }
        public List<Guid> RedCards { get; set; }
        public List<Guid> YellowCards { get; set; }
        public List<Guid> Assists { get; set; }
        public List<Guid> Goals { get; set; }
        public List<Guid> Injuries { get; set; }

        public Match() { }

        public Match(DateTime date, Guid homeTeamId, Guid visitorTeamId, MatchGoals homeGoals, MatchGoals visitorGoals)
        {
            Id = new Guid();
            Date = date;
            HomeTeamId = homeTeamId;
            VisitorTeamId = visitorTeamId;
            Result = new MatchGoals[2];
            Result[0] = homeGoals;
            Result[1] = visitorGoals;
            HomeLineup = new List<Guid>();
            VisitorLineup = new List<Guid>();
            RedCards = new List<Guid>();
            YellowCards = new List<Guid>();
            Assists = new List<Guid>();
            Goals = new List<Guid>();
            Injuries = new List<Guid>();
        }
        //To do: calculate Location

        public string GetMatchResultAsString()
        {
            return $"{HomeGoals.Value} - {VisitorGoals.Value}";
        }
    }
           

            
}
