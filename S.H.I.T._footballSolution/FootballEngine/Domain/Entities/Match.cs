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
        public bool IsPlayed { get; set; }
        public GeneralName Location
        {
            get { return teamService.GetBy(HomeTeamId).HomeArena; }
        }
        public Guid HomeTeamId { get; set; }
        public Guid VisitorTeamId { get; set; }
        public MatchDate Date { get; set; }
        public uint MatchTimeInMinutes { get; set; }
        public List<Event> HomeGoals { get; set; }
        public List<Event> VisitorGoals { get; set; }
        public string Result
        {
            get
            {
                if (IsPlayed)
                    return $"{HomeGoals.Count} - {VisitorGoals.Count}";
                else
                    return "-";
            }
        }
        public List<Guid> HomeLineup { get; set; }
        public List<Guid> VisitorLineup { get; set; }
        public List<Event> HomeRedCards { get; set; }
        public List<Event> VisitorRedCards { get; set; }
        public List<Event> HomeYellowCards { get; set; }
        public List<Event> VisitorYellowCards { get; set; }
        public List<Event> HomeAssists { get; set; }
        public List<Event> VisitorAssists { get; set; }
        public List<Exchange> HomeExchanges { get; set; }
        public List<Exchange> VisitorExchanges { get; set; }
        public List<Event> Injuries { get; set; }
        public static DateTime EndDateForMatchCreation
        {
            get
            { return DateTime.Now.AddYears(5); }
        }
        public Match() { }

        public Match(MatchDate date, Guid homeTeamId, Guid visitorTeamId)
        {
            Id = Guid.NewGuid();
            IsValidInparameter(date, homeTeamId, visitorTeamId);
            Date = date;
            MatchTimeInMinutes = 0;
            HomeTeamId = homeTeamId;
            VisitorTeamId = visitorTeamId;
            HomeGoals = new List<Event>();
            VisitorGoals = new List<Event>();
            HomeAssists = new List<ValueObjects.Event>();
            VisitorAssists = new List<ValueObjects.Event>();
            HomeLineup = new List<Guid>();
            VisitorLineup = new List<Guid>();
            HomeRedCards = new List<Event>();
            VisitorRedCards = new List<Event>();
            HomeYellowCards = new List<Event>();
            VisitorYellowCards = new List<Event>();
            Injuries = new List<Event>();
        }
        private bool IsValidInparameter(MatchDate date, Guid homeTeamId, Guid visitorTeamId)
        {
            if(date.Value.Date < DateTime.Now.Date )
            { throw new ArgumentOutOfRangeException($"Date is out of range can only be between now and two years from now."); }
            if (date.Value.Date > EndDateForMatchCreation.Date)
            { throw new ArgumentOutOfRangeException($"Date is out of range can only be between now and two years from now."); }
            if (Guid.Empty == homeTeamId)
            { throw new ArgumentException($"The homeTeramId cannot be null."); }
            if (Guid.Empty == visitorTeamId)
            { throw new ArgumentException($"The visitorTeamId cannot be null."); }
            return true;
        }

        public string GetMatchResultAsString()
        {
            return $"{HomeGoals.Count} - {VisitorGoals.Count}";
        }
    }
           

            
}
