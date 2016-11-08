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
        public PlayerName FirstName { get; set; }
        public PlayerName LastName { get; set; }
        public string FullName
        {
            get
            { return $"{FirstName.Value} {LastName.Value}"; }
        }
        public DateTime DateOfBirth { get; set; }
        public Status PlayerStatus { get; set; }
        public List<Guid> RedCards { get; set; }
        public List<Guid> YellowCards { get; set; }
        public List<Guid> Assists { get; set; }
        public List<Guid> Goals { get; set; }
        public int MatchesPlayed
        {
            get
            {
                return MatchesPlayedIds.Count;
            }
        }
        public Guid Team { get; set; }
        public List<Guid> MatchesPlayedIds { get; set; }

        public enum Status
        {
            Available, Suspended, Injured, NationalTeam, Other
        }



        public bool Playable
        {
            get
            {
                return PlayableCheck();
            }
        }

        public Player() { }


        public Player(PlayerName firstName, PlayerName lastName, DateTime dateOfBirth)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            MatchesPlayedIds = new List<Guid>();
            YellowCards = new List<Guid>();
            RedCards = new List<Guid>();
            Assists = new List<Guid>();
            Goals = new List<Guid>();
        }

        private bool PlayableCheck()
        {
            if (PlayerStatus == Status.Available)
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
