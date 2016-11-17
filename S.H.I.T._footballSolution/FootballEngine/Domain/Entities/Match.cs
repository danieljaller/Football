using FootballEngine.Domain.ValueObjects;
using FootballEngine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities
{
    public class Match
    {
        private TeamService teamService = new TeamService();
        public Guid Id { get; set; }
        public GeneralName Location
        {
            get { return teamService.GetBy(HomeTeamId).HomeArena; }
        }
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
        public static DateTime EndDateForMatchCreation
        {
            get
            { return DateTime.Now.AddYears(2); }
        }
        public Match() { }

        public Match(DateTime date, Guid homeTeamId, Guid visitorTeamId)
        {
            Id = Guid.NewGuid();
            IsValidInparameter(date, homeTeamId, visitorTeamId);
            Date = date;
            MatchTimeInMinutes = 0;
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
        private bool IsValidInparameter(DateTime date, Guid homeTeamId, Guid visitorTeamId)
        {
            if(date.Date < DateTime.Now.Date )
            { throw new ArgumentOutOfRangeException($"Date is out of range can only be between now and two years from now."); }
            if (date > EndDateForMatchCreation)
            { throw new ArgumentOutOfRangeException($"Date is out of range can only be between now and two years from now."); }
            if (Guid.Empty == homeTeamId)
            { throw new ArgumentException($"The homeTeramId cannot be null."); }
            if (Guid.Empty == visitorTeamId)
            { throw new ArgumentException($"The visitorTeamId cannot be null."); }
            return true;
        }

        public string GetMatchResultAsString()
        {
            return $"{HomeGoals.Value} - {VisitorGoals.Value}";
        }
    }
           

            
}
