using FootballEngine.Domain.Entities;
using FootballEngine.Services;
using System;
using FootballEngine.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballEngine.Helper;
using System.Xml.Serialization;
using System.IO;
using FootballEngine.Domain.ValueObjects;
using TestApplication.Factories;

namespace TestApplication
{
    class Program
    {
        private static SearchService searchService;
        private static MatchService matchService;
        private static SerieService serieService;
        private static TeamService teamService;
        private static PlayerService playerService;
        private static List<Player> players;
        private static readonly string directoryName = "Resources";
        private static readonly string matchesFileName = "Matches.xml";
        private static readonly string playerFileName = "Players.xml";
        private static readonly string seriesFileName = "Series.xml";
        private static readonly string teamFileName = "Teams.xml";

        public Program()
        {
        }
        static void Main(string[] args)
        {
            players = new List<Player>();
            //players = LoadPlayers();
            AddAndSavePlayers();

            Console.ReadLine();
        }

        public static List<Team> CreateTeamsWithPlayers(uint amount)
        {
            List<Team> teams = new List<Team>();
            for (int i = 1; i <= amount; i++)
            {
                Random rand = new Random();
                uint numberOfPlayers = Convert.ToUInt32(rand.Next(24, 31));
                List<Player> players = PlayerFactory.Create(numberOfPlayers) as List<Player>;

                Team team = TeamFactory.Create($"Team{i}", $"Arena-{i}", players.Select(p => p.Id));

                foreach (Player player in players)
                    player.TeamId = team.Id;

                teams.Add(team);
            }

            return teams;
        }

        public static void PrintList(List<object> list)
        {
            foreach (var item in list)
            {
                if (item is Player)
                {
                    Player player = item as Player;
                    Team playersTeam = teamService.GetBy(player.TeamId);
                    string teamName = (playersTeam == null) ? "-" : playersTeam.Name.Value;
                    StringBuilder playerBuilder = new StringBuilder();
                    playerBuilder.AppendLine(player.FullName);
                    playerBuilder.AppendLine(player.DateOfBirth.ToShortDateString());
                    playerBuilder.AppendLine(teamName);
                    playerBuilder.AppendLine($"Assists: {player.Assists.Count}");
                    playerBuilder.AppendLine($"Goals: {player.Goals.Count}");
                    playerBuilder.AppendLine($"Yellow cards: {player.YellowCards.Count}");
                    playerBuilder.AppendLine($"Red cards: {player.RedCards.Count}");
                    playerBuilder.AppendLine($"Matches played: {player.MatchesPlayed}");
                    playerBuilder.AppendLine($"Status: {player.PlayerStatus.ToString()}");
                    playerBuilder.AppendLine($"Playable: {player.Playable}");
                    Console.WriteLine(playerBuilder.ToString());
                    Console.WriteLine("----------------------------------------------------------");
                }
            }
        }

        public static void AddAndSaveTeam()
        {

        }

        public static void AddAndSavePlayers()
        {
            Guid teamId = Guid.NewGuid();
            for (int i = 1; i <= 24; i++)
            {
                var player = new Player(new PlayerName("Peter"), new PlayerName("Hanson"), new DateTime(1992 - 08 - 06));
                player.TeamId = teamId;
                players.Add(player);
            }
            try
            {
                string path;
                if (TryGetFilePath.InProjectDirectory(playerFileName, directoryName, true, out path))
                {
                    XmlHandler.SaveTo(path, players);
                    Console.WriteLine("Save successful");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

        }

        public static List<Player> LoadPlayers()
        {
            string path;
            List<Player> playerList;

            try
            {
                if (TryGetFilePath.InProjectDirectory(playerFileName, directoryName, false, out path))
                {
                    playerList = (List<Player>)XmlHandler.LoadFrom(path, typeof(List<Player>));
                    Console.WriteLine("Load successful");
                    return playerList;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }

}

