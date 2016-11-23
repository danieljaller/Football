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
using TestApplication.Helper;

namespace TestApplication
{
    class Program : ConsoleApp
    {
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            new Program();
        }

        protected override void Initialize()
        {
            AddCommand(ConsoleKey.D1, "Print all players", PrintAllPlayers);
            AddCommand(ConsoleKey.D2, "Print all teams", PrintAllTeams);
            AddCommand(ConsoleKey.D3, "Print all matches", PrintAllMatches);
            AddCommand(ConsoleKey.D4, "Print all series", PrintAllSeries);
            AddCommand(ConsoleKey.D5, "Print objects count", PrintObjectsCount);
            AddCommand(ConsoleKey.S, "Free search", FreeSearch);
            AddCommand(ConsoleKey.C, "Create test data", CreateTestData);
            AddCommand(ConsoleKey.Q, "Quit", Quit);
        }

        private void CreateTestData()
        {
            string message;
            bool createTestData;
            if (DataAlreadyExist(out message))
            {
                Console.WriteLine(message);
                AskFor(out createTestData, "Do you want to continue?");
            }
            else
            {
                createTestData = false;
                Console.WriteLine(message);
            }

            if (createTestData)
            {
                int maxInputAttempts = DefaultMaxInputAttempts,
                    maxNumberOfSeries = 20,
                    inputAttempts = 0;
                uint numberOfSeries = 0;
                Console.WriteLine($"Create test data.\nMaximum number of series to create: {maxNumberOfSeries}.\nEnter '0' to abort.");
                
                while (inputAttempts < maxInputAttempts)
                {
                    AskFor(out numberOfSeries, "Enter the number of series that you want to create: ");
                    if (numberOfSeries == 0)
                    {
                        Console.WriteLine("Creating test data was aborted. No test data was created.");
                        inputAttempts = maxInputAttempts;
                        break;
                    }
                    if (numberOfSeries > maxNumberOfSeries)
                    {
                        Console.WriteLine("Will not create that many series. Try again.");
                        continue;
                    }
                    inputAttempts++;
                }
                if (inputAttempts == maxInputAttempts)
                {
                    Console.WriteLine("Returning to main menu.\nPress any key to continue...");
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
                            ServiceLocator.Default.PlayerService.Add(player);

                    foreach (Team team in listOfTeamsInSerie)
                        ServiceLocator.Default.TeamService.Add(team);

                    //List<Guid> teamIds = listOfTeamsInSerie.Select(t => t.Id) as List<Guid>;
                    List<Guid> teamIds = new List<Guid>();
                    foreach (Team team in listOfTeamsInSerie)
                        teamIds.Add(team.Id);

                    List<Guid> matchIds = SerieAndMatchGenerator.SerieGenerator(teamIds,
                        GetRandomDate(DateTime.Now, DateTime.Now.AddYears(2)));
                    Serie serie = new Serie(new GeneralName($"Serie-{s + 1}"), teamIds, matchIds);
                    ServiceLocator.Default.SerieService.Add(serie);

                    foreach (Team team in listOfTeamsInSerie)
                        team.SeriesIds.Add(serie.Id);
                }

                SaveTestData();
            }
        }

        private bool DataAlreadyExist(out string message)
        {
            bool dataExist = true;
            if (ServiceLocator.Default.PlayerService.GetAll().Count() != 0)
            {
                dataExist = true;
                message = "Player data already exist.";
            }
            else
            {
                dataExist = false;
                message = "Player data do not exist.";
            }

            if (ServiceLocator.Default.TeamService.GetAll().Count() != 0)
            {
                dataExist = true;
                message += "\nTeam data already exist.";
            }
            else
            {
                dataExist = false;
                message += "\nTeam data do not exist.";
            }

            if (ServiceLocator.Default.MatchService.GetAll().Count() != 0)
            {
                dataExist = true;
                message += "\nMatch data already exist.";
            }
            else
            {
                dataExist = false;
                message += "\nMatch data do not exist.";
            }

            if (ServiceLocator.Default.SerieService.GetAll().Count() != 0)
            {
                dataExist = true;
                message += "\nSerie data already exist.";
            }
            else
            {
                dataExist = false;
                message += "\nSerie data do not exist.";
            }

            return dataExist;
        }

        private DateTime GetRandomDate(DateTime startDate, DateTime endDate)
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

        private void SaveTestData()
        {
            try
            {
                Console.Write("Players: ");
                ServiceLocator.Default.PlayerService.Save();
                Console.WriteLine("Save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save failed\n{e}");
            }

            try
            {
                Console.Write("Teams: ");
                ServiceLocator.Default.TeamService.Save();
                Console.WriteLine("Save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save failed\n{e}");
            }

            try
            {
                Console.Write("Matches: ");
                ServiceLocator.Default.MatchService.Save();
                Console.WriteLine("Save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save failed\n{e}");
            }

            try
            {
                Console.Write("Series: ");
                ServiceLocator.Default.SerieService.Save();
                Console.WriteLine("Save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save failed\n{e}");
            }
        }

        public void PrintAllPlayers()
        {
            var allPlayers = ServiceLocator.Default.PlayerService.GetAll();
            foreach (Player player in allPlayers)
            {
                Team playersTeam = ServiceLocator.Default.TeamService.GetBy(player.TeamId);
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
            Console.WriteLine($"Total number of players: {allPlayers.Count()}");
        }

        private void PrintAllTeams()
        {
            var allTeams = ServiceLocator.Default.TeamService.GetAll();
            foreach (Team team in allTeams)
            {
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
            Console.WriteLine($"Total number of teams: {allTeams.Count()}");
        }

        private void PrintAllMatches()
        {
            var allMatches = ServiceLocator.Default.MatchService.GetAll();
            foreach (var match in allMatches)
            {
                StringBuilder playerBuilder = new StringBuilder();
                playerBuilder.AppendLine($"Home team: {ServiceLocator.Default.TeamService.GetBy(match.HomeTeamId).Name} - Goals: {match.HomeGoals.Value}");
                playerBuilder.AppendLine($"Visitor team: {ServiceLocator.Default.TeamService.GetBy(match.VisitorTeamId).Name} - Goals: {match.VisitorGoals.Value}");
                playerBuilder.AppendLine($"Total match time: {match.MatchTimeInMinutes}");
                playerBuilder.AppendLine($"Goals:");
                foreach (var goal in match.Goals)
                {
                    playerBuilder.AppendLine($"{ServiceLocator.Default.PlayerService.GetBy(goal.PlayerId)} - {goal.TimeOfEvent}");
                }
                playerBuilder.AppendLine($"Assists:");
                foreach (var assist in match.Assists)
                {
                    playerBuilder.AppendLine($"{ServiceLocator.Default.PlayerService.GetBy(assist.PlayerId)} - {assist.TimeOfEvent}");
                }
                playerBuilder.AppendLine("Red cards: ");
                foreach (var card in match.RedCards)
                {
                    playerBuilder.AppendLine($"{ServiceLocator.Default.PlayerService.GetBy(card.PlayerId)} - {card.TimeOfEvent}");
                }
                playerBuilder.AppendLine("Yellow cards:");
                foreach (var card in match.YellowCards)
                {
                    playerBuilder.AppendLine($"{ServiceLocator.Default.PlayerService.GetBy(card.PlayerId)} - {card.TimeOfEvent}");
                }
                playerBuilder.AppendLine("Injuries: ");
                foreach (var injury in match.Injuries)
                {
                    playerBuilder.AppendLine($"{ServiceLocator.Default.PlayerService.GetBy(injury.PlayerId)} - {injury.TimeOfEvent}");
                }
                playerBuilder.AppendLine($"Location: {match.Location.Value}  ");
                playerBuilder.AppendLine($"Date: {match.Date.Date.ToShortDateString()} ");
                playerBuilder.AppendLine($"Home team lineup:");
                foreach (var player in match.HomeLineup)
                {
                    playerBuilder.AppendLine($"{ServiceLocator.Default.PlayerService.GetBy(player).FullName}");
                }
                playerBuilder.AppendLine($"Visitor team lineup:");
                foreach (var player in match.VisitorLineup)
                {
                    playerBuilder.AppendLine($"{ServiceLocator.Default.PlayerService.GetBy(player).FullName}");
                }
                Console.WriteLine(playerBuilder.ToString());
                Console.WriteLine("----------------------------------------------------------");
            }
            Console.WriteLine($"Total number matches: {allMatches.Count()}");
        }

        private void PrintAllSeries()
        {
            var allSeries = ServiceLocator.Default.SerieService.GetAll();
            foreach (var serie in allSeries)
            {
                StringBuilder serieBuilder = new StringBuilder();
                serieBuilder.AppendLine($"Name: {serie.Name.Value}");
                serieBuilder.AppendLine($"Number of teams: {serie.TeamTable.Count}");
                serieBuilder.AppendLine($"Matches:");
                foreach (var match in serie.MatchTable)
                {
                    serieBuilder.Append($"{ServiceLocator.Default.TeamService.GetBy(ServiceLocator.Default.MatchService.GetBy(match).HomeTeamId).Name} - ");
                    serieBuilder.Append($"{ServiceLocator.Default.TeamService.GetBy(ServiceLocator.Default.MatchService.GetBy(match).VisitorTeamId).Name} - ");
                    serieBuilder.Append($"{ServiceLocator.Default.MatchService.GetBy(match).Location.ToString()} - ");
                    serieBuilder.AppendLine($"{ServiceLocator.Default.MatchService.GetBy(match).Date.ToShortDateString()}");
                }

                Console.WriteLine(serieBuilder.ToString());
                Console.WriteLine("----------------------------------------------------------");
            }
            Console.WriteLine($"Total number of series: {allSeries.Count()}");
        }

        private void FreeSearch()
        {
            Console.Write("Enter text:");
            string userInput = Console.ReadLine();

            var searchResult = ServiceLocator.Default.SearchService.Search(userInput, true, true, true, false, true);

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

        private void PrintObjectsCount()
        {
            Console.WriteLine($"Total number of players: {ServiceLocator.Default.PlayerService.GetAll().Count()}");
            Console.WriteLine($"Total number of teams: {ServiceLocator.Default.TeamService.GetAll().Count()}");
            Console.WriteLine($"Total number of matches: {ServiceLocator.Default.MatchService.GetAll().Count()}");
            Console.WriteLine($"Total number of series: {ServiceLocator.Default.SerieService.GetAll().Count()}");
            Console.WriteLine();
        }

        private void Quit()
        {
            bool quit;
            AskFor(out quit, "Do you really want to quit?");
            if (quit)
                EndLoop();
        }
    }
}








