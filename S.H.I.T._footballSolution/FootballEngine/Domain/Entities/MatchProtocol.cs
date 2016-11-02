using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities
{
    class MatchProtocol
    {
        public Guid Id { get; set; }
        public Guid MatchId { get; set; }
        public int[] Result { get; set; }
        public int HomeGoals { get; set; }
        public int VisitorGoals { get; set; }
        public Team HomeTeam { get; set; }
        public Team VisitorTeam { get; set; }
        public List<Player> HomeLineup { get; set; }
        public List<Player> VisitorLineup { get; set; }
        public List<Player> RedCards { get; set; }
        public List<Player> YellowCards { get; set; }
        public List<Player> Assists { get; set; }
        public List<Player> Goals { get; set; }
        public List<Player> Injuries { get; set; }

        public MatchProtocol() { }

        public MatchProtocol(Guid matchId, int homeGoals, int visitorGoals, Team homeTeam, Team visitorTeam)
        {
            Id = Guid.NewGuid();
            MatchId = matchId;
            Result = new int[2];
            Result[0] = homeGoals;
            Result[1] = visitorGoals;
            HomeTeam = homeTeam;
            VisitorTeam = visitorTeam;
            HomeLineup = new List<Player>();
            VisitorLineup = new List<Player>();
            RedCards = new List<Player>();
            YellowCards = new List<Player>();
            Assists = new List<Player>();
            Goals = new List<Player>();
            Injuries = new List<Player>();
        }
    }
}
