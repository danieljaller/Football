using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballEngine.Domain.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public PlayerName FirstName{ get; set; }
        public PlayerName LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Status PlayerStatus { get; set; }
        public int RedCard { get; set; }
        public int YellowCard { get; set; }
        public int Assist { get; set; }
        public int Goals { get; set; }
        public int MatchesPlayed { get; set; }
        public Guid Team { get; set; }
        public List<Guid> PlayerMatchesList { get; set; }

        public enum Status {
        Spelbar, Avstängd, Skadad, Landslagsuppdrag, Övrigt}

        public bool PlayAble {
            get {
                return PlayAbleCheck();
            }
        }

        public Player() { }

        public Player(PlayerName firstName, PlayerName lastName, DateTime dateOfBirth)
            :this(Guid.NewGuid(), firstName, lastName, dateOfBirth)
        {
        }

        public Player(Guid id, PlayerName firstName, PlayerName lastName, DateTime dateOfBirth)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PlayerMatchesList = new List<Guid>();
        }

        private bool PlayAbleCheck()
        {
            if (PlayerStatus == Status.Spelbar)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
