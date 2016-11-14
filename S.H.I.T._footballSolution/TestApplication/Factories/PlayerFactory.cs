using FootballEngine.Domain.Entities;
using FootballEngine.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Helper;

namespace TestApplication.Factories
{
    public static class PlayerFactory
    {
        public static IEnumerable<Player> Create(uint amount)
        {
            amount = (amount > 30) ? 30 : amount;
            List<Player> players = new List<Player>();
            Random rand = new Random();
            for (int i = 1; i <= amount; i++)
            {
                PlayerName firstName = new PlayerName("Player");
                PlayerName lastName = new PlayerName(i.NumberToWords());
                
                int year = rand.Next(DateTime.Now.Year - 50, DateTime.Now.Year - 17);
                int month = rand.Next(1, 13);
                int day = rand.Next(1, DateTime.DaysInMonth(year, month) + 1);
                DateTime dateOfBirth = new DateTime(year, month, day);
                
                players.Add(new Player(firstName, lastName, dateOfBirth));
            }

            return players;
        }
    }
}
