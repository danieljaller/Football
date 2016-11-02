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
        public int[] Result { get; set; }
        public int HomeGoals { get; set; }
        public int VisitorGoals { get; set; }
        public List<Guid> HomeLineup { get; set; }
        public List<Guid> VisitorLineup { get; set; }
        public List<Guid> RedCards { get; set; }
        public List<Guid> YellowCards { get; set; }
        public List<Guid> Assists { get; set; }
        public List<Guid> Goals { get; set; }
        public List<Guid> Injuries { get; set; }

        public Match() { }

        public Match(string plats, DateTime date, Guid homeTeamId, Guid visitorTeamId, int homeGoals, int visitorGoals)
        {
            Id = new Guid();
            Date = date;
            HomeTeamId = homeTeamId;
            VisitorTeamId = visitorTeamId;
            Result = new int[2];
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
    }
           

            
}
