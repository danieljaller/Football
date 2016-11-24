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

        public Program()
        {


        }
        static void Main(string[] args)
        {

            matchService = new MatchService();
            teamService = new TeamService();
            playerService = new PlayerService(teamService);
            serieService = new SerieService();
            searchService = new SearchService();

            //CreateTestData();

            bool keepRunning = true;
            while (keepRunning)
            {
                PrintMenu();

                var key = Console.ReadKey().Key;
                Console.WriteLine();
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

                    case ConsoleKey.D3:
                        PrintSerie(serieService.GetAll().ToList());
                        break;

                    case ConsoleKey.D4:
                        PrintMatches(matchService.GetAll().ToList());
                        break;

                    case ConsoleKey.D5:
                        FreeSearch();
                        break;

                    case ConsoleKey.D6:
                        CreateMatchTable();
                        break;

                    case ConsoleKey.C:
                        CreateTestData_2();
                        break;

                    case ConsoleKey.P:
                        PrintObjectsCount();
                        break;
                }
            }
        }

        public static void CreateTestData_2()
        {
            const int maxInputAttempts = 5, maxNumberOfSeries = 20;
            int inputAttempts = 0, numberOfSeries = 0;
            Console.WriteLine($"Create test data.\nMaximum number of series to create: {maxNumberOfSeries}. Enter '0' to abort.");

            while (inputAttempts < maxInputAttempts)
            {
                Console.Write($"Enter the number of series that you want to create (Input attempts left: {maxInputAttempts - inputAttempts}): ");
                Console.WriteLine();
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out numberOfSeries))
                {
                    if (numberOfSeries == 0)
                        return;
                    if (numberOfSeries > maxNumberOfSeries)
                    {
                        Console.WriteLine("Will not create that many series. Try again.");
                        continue;
                    }
                    break;
                }
                inputAttempts++;
            }
            if (inputAttempts == maxInputAttempts)
            {
                Console.WriteLine("Maximum input attempts reached! Returning to main menu.\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            Random rand = new Random();
            List<int> numberOfPlayersInEachTeam = new List<int>();
            for (int i = 0; i < (numberOfSeries * 16); i++)
                numberOfPlayersInEachTeam.Add(rand.Next(24, 31));

            for (int s = 0; s < numberOfSeries; s++)
            {
                List<List<Player>> listOfPlayersInSerie = new List<List<Player>>();
                for (int t = 0; t < 16; t++)
                {
                    listOfPlayersInSerie.Add(PlayerFactory.Create(Convert.ToUInt32(numberOfPlayersInEachTeam[0])) as List<Player>);
                    numberOfPlayersInEachTeam.RemoveAt(0);
                }
                List<Team> listOfTeamsInSerie = TeamFactory.CreateTeamsAndSetPlayersTeamId(listOfPlayersInSerie);

                foreach (List<Player> playerList in listOfPlayersInSerie)
                    foreach (Player player in playerList)
                        playerService.Add(player);

                foreach (Team team in listOfTeamsInSerie)
                    teamService.Add(team);

                //List<Guid> teamIds = listOfTeamsInSerie.Select(t => t.Id) as List<Guid>;
                List<Guid> teamIds = new List<Guid>();
                foreach (Team team in listOfTeamsInSerie)
                    teamIds.Add(team.Id);

                List<Guid> matchIds = SerieAndMatchGenerator.SerieGenerator(teamIds,
                    GetRandomDate(DateTime.Now, DateTime.Now.AddYears(2)));
                Serie serie = new Serie(new GeneralName($"Serie-{s + 1}"), teamIds, matchIds);
                serieService.Add(serie);

                foreach (Team team in listOfTeamsInSerie)
                    team.SeriesIds.Add(serie.Id);
            }

            try
            {
                Console.Write("Players: ");
                playerService.Save();
                Console.WriteLine("save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"failed\n{e}");
            }

            try
            {
                Console.Write("Teams: ");
                teamService.Save();
                Console.WriteLine("save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"failed\n{e}");
            }

            try
            {
                Console.Write("Matches: ");
                matchService.Save();
                Console.WriteLine("save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"failed\n{e}");
            }

            try
            {
                Console.Write("Series: ");
                serieService.Save();
                Console.WriteLine("save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"failed\n{e}");
            }
        }

        private static Random rand = new Random();

        public static DateTime GetRandomDate(DateTime startDate, DateTime endDate)
        {
            int year = rand.Next(startDate.Year, endDate.Year + 1);

            int month;
            if (year == startDate.Year)
                month = rand.Next(startDate.Month, 13);
            else if (year == endDate.Year)
                month = rand.Next(1, endDate.Month + 1);
            else
                month = rand.Next(1, 13);

            int day;
            if (year == startDate.Year && month == startDate.Month)
                day = rand.Next(startDate.Day, DateTime.DaysInMonth(year, month) + 1);
            else if (year == endDate.Year)
                day = rand.Next(1, endDate.Day + 1);
            else
                day = rand.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return new DateTime(year, month, day);
        }

        //public static void CreateTestData()
        //{

        //    List<List<Player>> listOfPlayerLists = new List<List<Player>>();
        //    for (int i = 0; i < 16; i++)
        //    {
        //        listOfPlayerLists.Add(CreatePlayerList());
        //    }
        //    List<Team> teamList = CreateTeamsWithPlayers(listOfPlayerLists);

        //    foreach (List<Player> playerList in listOfPlayerLists)
        //    {
        //        foreach (Player player in playerList)
        //        {
        //            playerService.Add(player);
        //        }
        //    }
        //    for (int i = 0; i < teamList.Count; i++)
        //    {
        //        List<Player> l = listOfPlayerLists[i];
        //        teamList[i].PlayerIds = l.Select(p => p.Id).ToList();
        //    }
       
        //    List<Guid> teamIds = teamList.Select(p => p.Id).ToList();
        //    List<Guid> matchIds = SerieAndMatchGenerator.SerieGenerator(teamIds, DateTime.Now);
        //    Serie serie = new Serie(new GeneralName("Serie1"), teamIds, matchIds);
          

        //    serieService.Add(serie);

        //    foreach (Team team in teamList)
        //    {
        //        team.SeriesIds.Add(serie.Id);
        //        teamService.Add(team);
        //    }

        //    try
        //    {
        //        Console.Write("Players: ");
        //        playerService.Save();
        //        Console.WriteLine("successful");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"failed\n{e}");
        //    }

        //    try
        //    {
        //        Console.Write("Teams: ");
        //        teamService.Save();
        //        Console.WriteLine("successful");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"failed\n{e}");
        //    }

        //    try
        //    {
        //        Console.Write("Matches: ");
        //        matchService.Save();
        //        Console.WriteLine("successful");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"failed\n{e}");
        //    }

        //    try
        //    {
        //        Console.Write("Series: ");
        //        serieService.Save();
        //        Console.WriteLine("successful");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine($"failed\n{e}");
        //    }
        //}
        //private static void GetPlayers()
        //{
        //    var list = playerService.GetAllPlayersBySerie();
        //}



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
            Console.WriteLine($"Total number of players: {listOfPlayers.Count}");
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
                playerBuilder.AppendLine($"GoalDifferens: {team.GoalDifference}");
                playerBuilder.AppendLine($"Matches Played: {team.MatchIds.Count}");
                playerBuilder.AppendLine($"Players: {team.PlayerIds.Count}");


                Console.WriteLine(playerBuilder.ToString());
                Console.WriteLine("----------------------------------------------------------");
            }
            Console.WriteLine($"Total number of teams: {teamList.Count}");
        }

        private static void PrintSerie(List<Serie> serieList)
        {

            foreach (var serie in serieList)
            {

                StringBuilder serieBuilder = new StringBuilder();
                serieBuilder.AppendLine($"Name: {serie.Name.Value}");
                serieBuilder.AppendLine($"Number of teams: {serie.TeamTable.Count}");
                serieBuilder.AppendLine($"Matches:");
                foreach (var match in serie.MatchTable)
                {
                    serieBuilder.AppendLine($"{teamService.GetBy(matchService.GetBy(match).HomeTeamId).Name} - {teamService.GetBy(matchService.GetBy(match).VisitorTeamId).Name} - {matchService.GetBy(match).Location.ToString()} - {matchService.GetBy(match).Date.ToString()}");
                }


                Console.WriteLine(serieBuilder.ToString());
                Console.WriteLine("----------------------------------------------------------");
            }
            Console.WriteLine($"Total number of series: {serieList.Count}");
        }

        private static void PrintMatches(List<Match> matchList)
        {
            foreach (var match in matchList)
            {
                StringBuilder playerBuilder = new StringBuilder();
                playerBuilder.AppendLine($"Home team: {teamService.GetBy(match.HomeTeamId).Name} - Goals: {match.HomeGoals.Count()}");
                playerBuilder.AppendLine($"Visitor team: {teamService.GetBy(match.VisitorTeamId).Name} - Goals: {match.VisitorGoals.Count()}");
                playerBuilder.AppendLine($"Total match time: {match.MatchTimeInMinutes}");
                playerBuilder.AppendLine($"Goals:");
                foreach (var goal in match.Goals)
                {
                    playerBuilder.AppendLine($"{playerService.GetBy(goal.PlayerId)} - {goal.TimeOfEvent}");
                }
                playerBuilder.AppendLine($"Assists:");
                foreach (var assist in match.Assists)
                {
                    playerBuilder.AppendLine($"{playerService.GetBy(assist.PlayerId)} - {assist.TimeOfEvent}");
                }
                playerBuilder.AppendLine("Red cards: ");
                foreach (var card in match.RedCards)
                {
                    playerBuilder.AppendLine($"{playerService.GetBy(card.PlayerId)} - {card.TimeOfEvent}");
                }
                playerBuilder.AppendLine("Yellow cards:");
                foreach (var card in match.YellowCards)
                {
                    playerBuilder.AppendLine($"{playerService.GetBy(card.PlayerId)} - {card.TimeOfEvent}");
                }
                playerBuilder.AppendLine("Injuries: ");
                foreach (var injury in match.Injuries)
                {
                    playerBuilder.AppendLine($"{playerService.GetBy(injury.PlayerId)} - {injury.TimeOfEvent}");
                }
                playerBuilder.AppendLine($"Location: {match.Location.Value}  ");
                playerBuilder.AppendLine($"Date: {match.Date.ToString()} ");
                playerBuilder.AppendLine($"Home team lineup:");
                foreach (var player in match.HomeLineup)
                {
                    playerBuilder.AppendLine($"{playerService.GetBy(player).FullName}");
                }
                playerBuilder.AppendLine($"Visitor team lineup:");
                foreach (var player in match.VisitorLineup)
                {
                    playerBuilder.AppendLine($"{playerService.GetBy(player).FullName}");
                }
                Console.WriteLine(playerBuilder.ToString());
                Console.WriteLine("----------------------------------------------------------");
            }
            Console.WriteLine($"Total number matches: {matchList.Count}");
        }

        private static void FreeSearch()
        {
            Console.Write("Enter text:");
            string userInput = Console.ReadLine();

            var searchResult = searchService.Search(userInput, true, true, true, false);

            foreach (var item in searchResult)
            {
                if (item.GetType() == typeof(Team))
                {
                    var team = item as Team;
                    Console.WriteLine(team.Name.Value);
                }

                if (item.GetType() == typeof(Player))
                {
                    var player = item as Player;
                    Console.WriteLine(player.FullName, player.DateOfBirth.ToString());
                }
            }
        }

        private static void CreateMatchTable()
        {
            Console.Write("Namn på serie: ");
            string serieName = Console.ReadLine();
            DateTime startDate = new DateTime();
            while (true)
            {
                Console.Write("Startdatum: ");
                try
                {
                    startDate = DateTime.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Felaktigt format");
                }
            }

            List<Guid> teams = new List<Guid>();
            teams = teamService.GetAll().Select(t => t.Id).ToList().GetRange(0, 16);
            List<Guid> matchTable = SerieAndMatchGenerator.SerieGenerator(teams, startDate);
            foreach (var match in matchTable)
            {
                matchService.Add(matchService.GetBy(match));
            }
            serieService.Add(new Serie(new GeneralName(serieName), teams, matchTable));

        }

        private static void PrintObjectsCount()
        {
            Console.WriteLine($"Total number of series: {serieService.GetAll().Count()}");
            Console.WriteLine($"Total number of matches: {matchService.GetAll().Count()}");
            Console.WriteLine($"Total number of teams: {teamService.GetAll().Count()}");
            Console.WriteLine($"Total number of players: {playerService.GetAll().Count()}");
            Console.WriteLine();
        }

        private static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("+--------------------------------------------------------------------------------+");
            Console.WriteLine("|   [1] Print Players         [3] Print Series          [5] FreeSearch           |");
            Console.WriteLine("|   [2] Print Teams           [4] Print Matches         [q] Quit                 |");
            Console.WriteLine("|   [6] Create Serie          [c] Create test data      [p] Print objects count  |");
            Console.WriteLine("+--------------------------------------------------------------------------------+");
            Console.WriteLine();
        }

    }
}








