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
        public Match(string plats, DateTime date, Team hemmalag, Team bortaLag)
        {
            Id = new Guid();
             
        }
            
           

            
        public Guid Id { get; set; }
        public GeneralName Plats { get; set; }
        public Guid HemmaLag { get; set; }
        public Guid BortaLag { get; set; }
        public DateTime Date { get; set; }
        // To do add Matchprotkoll
    }
}
