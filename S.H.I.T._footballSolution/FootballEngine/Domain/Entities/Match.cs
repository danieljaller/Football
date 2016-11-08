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
        public uint MatchTimeInMinutes { get; set; }
       
        public MatchGoals HomeGoals { get; set; }
        public MatchGoals VisitorGoals { get; set; }
        public List<Guid> HomeLineup { get; set; }
        public List<Guid> VisitorLineup { get; set; }
        public List<Event> RedCards { get; set; }
        public List<Event> YellowCards { get; set; }
        public List<Event> Assists { get; set; }
        public List<Event> Goals { get; set; }
        public List<Event> Injuries { get; set; }

        public Match() { }

        public Match(DateTime date, Guid homeTeamId, Guid visitorTeamId)
        {
            Id = Guid.NewGuid();
            Date = date;
            HomeTeamId = homeTeamId;
            VisitorTeamId = visitorTeamId;            
            HomeGoals = new MatchGoals(0);
            VisitorGoals = new MatchGoals(0);
            HomeLineup = new List<Guid>();
            VisitorLineup = new List<Guid>();
            RedCards = new List<Event>();
            YellowCards = new List<Event>();
            Assists = new List<Event>();
            Goals = new List<Event>();
            Injuries = new List<Event>();
        }
        //To do: calculate Location

        public string GetMatchResultAsString()
        {
            return $"{HomeGoals.Value} - {VisitorGoals.Value}";
        }
    }
           

            
}
