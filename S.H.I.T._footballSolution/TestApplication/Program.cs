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
using static FootballEngine.Domain.Entities.Player;
using System.Windows.Input;

namespace TestApplication
{
    class Program
    {
        private static SearchService searchService;
        private static MatchService matchService;
        private static SerieService serieService;
        private static TeamService teamService;
        private static PlayerService playerService;
        
        private static readonly string directoryName = "Resources";
        private static readonly string matchesFileName = "Matches.xml";
        private static readonly string playerFileName = "Players.xml";
        private static readonly string seriesFileName = "Series.xml";
        private static readonly string teamFileName = "Teams.xml";
        private bool stopRuuning;

        
        

        public Program()
        {
            
          
        }
        static void Main(string[] args)
        {
            matchService = new MatchService();
            playerService = new PlayerService();
            serieService = new SerieService();
            searchService = new SearchService();
            teamService = new TeamService();

            CreateTestData();

            bool keepRunning = true;
            while (keepRunning)
            {
                PrintMenu();

                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        PrintPlayers(playerService.GetAll().ToList());
                        
                        break;

                    case ConsoleKey.D2:
                        PrintTeams(teamService.GetAll().ToList());
                        break; 

                    case ConsoleKey.Q:
                        keepRunning = false;
                        break;


                }
            }




            
        }
        public static void CreateTestData()
        {
           
            List<List<Player>> listOfPlayerLists = new List<List<Player>>();
            for (int i = 0; i < 16; i++)
            {
                listOfPlayerLists.Add(CreatePlayerList());
            }
            List<Team> teamList = CreateTeamsWithPlayers(listOfPlayerLists);

            foreach (List<Player> playerList in listOfPlayerLists)
            {
                foreach (Player player in playerList)
                {
                    playerService.Add(player);
                }
            }
            for (int i = 0; i < teamList.Count; i++)
            {
                List<Player> l = listOfPlayerLists[i];
                teamList[i].PlayerIds = l.Select(p => p.Id).ToList();
            }
            foreach (Team team in teamList)
            {
               
                teamService.Add(team);
            }

            try
            {
                playerService.Save();
                Console.WriteLine("Save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save failed\n{e}");
            }
            try
            {
                teamService.Save();
                Console.WriteLine("Save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save failed\n{e}");
            }
        }

          

        public static List<Player> CreatePlayerList()
        {
            Random rand = new Random();
            uint numberOfPlayers = Convert.ToUInt32(rand.Next(24, 31));

            return PlayerFactory.Create(numberOfPlayers) as List<Player>;
        }

        public static List<Team> CreateTeamsWithPlayers(List<List<Player>> players)
        {
            List<Team> teams = new List<Team>();
            for (int i = 0; i < players.Count; i++)
            {
                try
                {
                    Team team = TeamFactory.Create($"Team{i + 1}", $"Arena-{i + 1}", players[i].Select(p => p.Id));

                    foreach (Player player in players[i])
                    {
                        player.TeamId = team.Id;
                    }

                    teams.Add(team);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return teams;
        }

        public static void PrintPlayers(List<Player> listOfPlayers)
        {
            foreach (Player player in listOfPlayers)
            {
                Team playersTeam = teamService.GetBy(player.TeamId);
                string teamName = (playersTeam == null) ? "-" : playersTeam.Name.Value;
                StringBuilder playerBuilder = new StringBuilder();
                playerBuilder.AppendLine($"Name: {player.FullName}");
                playerBuilder.AppendLine($"Date of birth: {player.DateOfBirth.ToShortDateString()}");
                playerBuilder.AppendLine($"Team: {teamName}");
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

        private static void PrintTeams(List<Team> teamList)
        {
            foreach (Team team in teamList)
            {
                //Team playersTeam = teamService.GetBy(player.TeamId);
                //string teamName = (playersTeam == null) ? "-" : playersTeam.Name.Value;
                StringBuilder playerBuilder = new StringBuilder();
                playerBuilder.AppendLine($"Name: {team.Name.Value}");
                playerBuilder.AppendLine($"HomeArena: {team.HomeArena.Value}");
                playerBuilder.AppendLine($"Points: {team.Points}");
                playerBuilder.AppendLine($"Wins: {team.Wins}");
                playerBuilder.AppendLine($"Losses: {team.Losses}");
                playerBuilder.AppendLine($"Ties: {team.Ties}");
                playerBuilder.AppendLine($"GoalDifferens: {team.GoalDifferens}");
                playerBuilder.AppendLine($"Matches Played: {team.MatchIds.Count}");
                playerBuilder.AppendLine($"Players: {team.PlayerIds.Count}");


                Console.WriteLine(playerBuilder.ToString());
                Console.WriteLine("----------------------------------------------------------");
            }
        }


      

        private static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine("|   [1] Print Players         [3] Print Series                    |");
            Console.WriteLine("|   [2] Print Teams           [4] Print Matches         [q] Quit  |");
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.WriteLine();
        }
      
    }
}


     

      

     

