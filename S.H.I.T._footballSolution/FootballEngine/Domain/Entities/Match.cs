using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities
{
    class Match
    {
        public Match(string plats, DateTime date, Guid homeTeamId, Guid visitorTeamId)
        {
            Id = new Guid();
            Date = date;
            HomeTeamId = homeTeamId;
            VisitorTeamId = visitorTeamId;
        }
            
        public Guid Id { get; set; }
        public GeneralName location { get; set; }
        public Guid HomeTeamId { get; }
        public Guid VisitorTeamId { get; }
        public DateTime Date { get; set; }
        // To do add Matchprotkoll
    }
           

            
}
