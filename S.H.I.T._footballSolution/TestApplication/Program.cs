using FootballEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FootballEngine.Helper;
using FootballEngine.Domain.ValueObjects;
using FootballEngine.Factories;

namespace TestApplication
{
    class Program : ConsoleApp
    {
        private static readonly Random _Random = new Random();

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
                Console.WriteLine("-------------------------");
                Console.WriteLine(message);
                Console.WriteLine("-------------------------");
                AskFor(out createTestData, "Do you want to continue?");
                if (!createTestData)
                    return;

                bool replaceData;
                AskFor(out replaceData, "Do you want to replace or add to existing data?", ConsoleKey.R, ConsoleKey.A, false);
                if (replaceData)
                    RemoveCurrentData();
            }
            else
            {
                createTestData = true;
                Console.WriteLine("-------------------------");
                Console.WriteLine(message);
                Console.WriteLine("-------------------------");
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
                    if (0 < numberOfSeries && numberOfSeries <= maxNumberOfSeries)
                        break;
                    if (numberOfSeries == 0)
                    {
                        Console.WriteLine("Creating test data was aborted.");
                        inputAttempts = maxInputAttempts;
                        break;
                    }
                    if (numberOfSeries > maxNumberOfSeries)
                    {
                        Console.WriteLine("Will not create that many series. Try again.");
                        inputAttempts++;
                    }
                }
                if (inputAttempts == maxInputAttempts)
                {
                    Console.WriteLine("No test data was created. Returning to main menu.\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                int[] numberOfPlayersInEachTeam = new int[numberOfSeries * 16];
                for (int i = 0; i < numberOfPlayersInEachTeam.Length; i++)
                    numberOfPlayersInEachTeam[i] = _Random.Next(24, 31);

                int playerCounter = 1,
                    teamCounter = 1;

                List<Player> players = new List<Player>();
                List<Team> teams = new List<Team>();
                List<Match> matches = new List<Match>();
                List<Serie> series = new List<Serie>();

                for (int s = 0; s < numberOfSeries; s++)
                {
                    List<List<Player>> playerLists = new List<List<Player>>();
                    List<Team> teamList = new List<Team>();
                    List<Match> matchList = new List<Match>();

                    for (int t = 0; t < 16; t++)
                    {
                        int amount = _Random.Next(24, 31);
                        playerLists.Add(PlayerFactory.CreateListOfPlayerLists(amount, playerCounter));
                        playerCounter += amount;
                    }
                    teamList = TeamFactory.CreateTeamsAndSetPlayersTeamId(playerLists, teamCounter);
                    teamCounter++;

                    matchList = MatchTableFactory.CreateMatchTable(teamList,
                        GetRandomDate(DateTime.Now, DateTime.Now.AddYears(2)));

                    Serie serie = new Serie(new GeneralName($"Serie-{s + 1}"), teamList.Select(team => team.Id).ToList(), matchList.Select(match => match.Id).ToList());

                    foreach (Team team in teamList)
                        team.SerieIds.Add(serie.Id);

                    foreach (List<Player> playerList in playerLists)
                        players.AddRange(playerList);

                    teams.AddRange(teamList);
                    matches.AddRange(matchList);
                    series.Add(serie);
                }

                SaveTestData(players, teams, matches, series);
            }
        }

        private bool DataAlreadyExist(out string message)
        {
            bool dataExist = true;
            if (ServiceLocator.Instance.PlayerService.GetAll().Count() != 0)
            {
                dataExist = true;
                message = "Player data already exist.";
            }
            else
            {
                dataExist = false;
                message = "Player data does not exist.";
            }

            if (ServiceLocator.Instance.TeamService.GetAll().Count() != 0)
            {
                dataExist = true;
                message += "\nTeam data already exist.";
            }
            else
            {
                dataExist = false;
                message += "\nTeam data does not exist.";
            }

            if (ServiceLocator.Instance.MatchService.GetAll().Count() != 0)
            {
                dataExist = true;
                message += "\nMatch data already exist.";
            }
            else
            {
                dataExist = false;
                message += "\nMatch data does not exist.";
            }

            if (ServiceLocator.Instance.SerieService.GetAll().Count() != 0)
            {
                dataExist = true;
                message += "\nSerie data already exist.";
            }
            else
            {
                dataExist = false;
                message += "\nSerie data does not exist.";
            }

            return dataExist;
        }

        private void RemoveCurrentData()
        {
            List<Guid> matchIds = ServiceLocator.Instance.MatchService.GetAll().Select(match => match.Id).ToList();
            List<Guid> playerIds = ServiceLocator.Instance.PlayerService.GetAll().Select(player => player.Id).ToList();
            List<Guid> serieIds = ServiceLocator.Instance.SerieService.GetAll().Select(serie => serie.Id).ToList();
            List<Guid> teamIds = ServiceLocator.Instance.TeamService.GetAll().Select(team => team.Id).ToList();

            foreach (Guid matchId in matchIds)
                ServiceLocator.Instance.MatchService.Delete(matchId);
            
            foreach (Guid playerId in playerIds)
                ServiceLocator.Instance.PlayerService.Delete(playerId);
            
            foreach (Guid serieId in serieIds)
                ServiceLocator.Instance.SerieService.Delete(serieId);
            
            foreach (Guid teamId in teamIds)
                ServiceLocator.Instance.TeamService.Delete(teamId);
        }

        private DateTime GetRandomDate(DateTime startDate, DateTime endDate)
        {
            int year = _Random.Next(startDate.Year, endDate.Year + 1);

            int month;
            if (year == startDate.Year)
                month = _Random.Next(startDate.Month, 13);
            else if (year == endDate.Year)
                month = _Random.Next(1, endDate.Month + 1);
            else
                month = _Random.Next(1, 13);

            int day;
            if (year == startDate.Year && month == startDate.Month)
                day = _Random.Next(startDate.Day, DateTime.DaysInMonth(year, month) + 1);
            else if (year == endDate.Year)
                day = _Random.Next(1, endDate.Day + 1);
            else
                day = _Random.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return new DateTime(year, month, day);
        }

        private void SaveTestData(List<Player> players, List<Team> teams, List<Match> matches, List<Serie> series)
        {
            foreach (Player player in players)
                ServiceLocator.Instance.PlayerService.Add(player);

            foreach (Team team in teams)
                ServiceLocator.Instance.TeamService.Add(team);

            foreach (Match match in matches)
                ServiceLocator.Instance.MatchService.Add(match);

            foreach (Serie serie in series)
                ServiceLocator.Instance.SerieService.Add(serie);

            try
            {
                Console.Write("Players: ");
                ServiceLocator.Instance.PlayerService.Save();
                Console.WriteLine("Save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save failed\n{e}");
            }

            try
            {
                Console.Write("Teams: ");
                ServiceLocator.Instance.TeamService.Save();
                Console.WriteLine("Save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save failed\n{e}");
            }

            try
            {
                Console.Write("Matches: ");
                ServiceLocator.Instance.MatchService.Save();
                Console.WriteLine("Save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save failed\n{e}");
            }

            try
            {
                Console.Write("Series: ");
                ServiceLocator.Instance.SerieService.Save();
                Console.WriteLine("Save successful");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Save failed\n{e}");
            }
        }

        public void PrintAllPlayers()
        {
            var allPlayers = ServiceLocator.Instance.PlayerService.GetAll();
            foreach (Player player in allPlayers)
            {
                Team playersTeam = ServiceLocator.Instance.TeamService.GetBy(player.TeamId);
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
            var allTeams = ServiceLocator.Instance.TeamService.GetAll();
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
            var allMatches = ServiceLocator.Instance.MatchService.GetAll();
            foreach (var match in allMatches)
            {
                StringBuilder playerBuilder = new StringBuilder();
                playerBuilder.AppendLine($"Home team: {ServiceLocator.Instance.TeamService.GetBy(match.HomeTeamId).Name} - Goals: {match.HomeGoals.Count}");
                playerBuilder.AppendLine($"Visitor team: {ServiceLocator.Instance.TeamService.GetBy(match.VisitorTeamId).Name} - Goals: {match.VisitorGoals.Count}");
                playerBuilder.AppendLine($"Total match time: {match.MatchTimeInMinutes}");
                playerBuilder.AppendLine($"Assists: Not implemented!");
                //foreach (var assist in match.Assists)
                //{
                //    playerBuilder.AppendLine($"{ServiceLocator.Instance.PlayerService.GetBy(assist.PlayerId)} - {assist.TimeOfEvent}");
                //}
                playerBuilder.AppendLine("Red cards: Not implemented!");
                //foreach (var card in match.RedCards)
                //{
                //    playerBuilder.AppendLine($"{ServiceLocator.Instance.PlayerService.GetBy(card.PlayerId)} - {card.TimeOfEvent}");
                //}
                playerBuilder.AppendLine("Yellow cards: Not implemented!");
                //foreach (var card in match.YellowCards)
                //{
                //    playerBuilder.AppendLine($"{ServiceLocator.Instance.PlayerService.GetBy(card.PlayerId)} - {card.TimeOfEvent}");
                //}
                playerBuilder.AppendLine($"Location: {match.Location.Value}  ");
                playerBuilder.AppendLine($"Date: {match.Date} ");
                playerBuilder.AppendLine($"Home team lineup:");
                foreach (var player in match.HomeLineup)
                {
                    playerBuilder.AppendLine($"{ServiceLocator.Instance.PlayerService.GetBy(player).FullName}");
                }
                playerBuilder.AppendLine($"Visitor team lineup:");
                foreach (var player in match.VisitorLineup)
                {
                    playerBuilder.AppendLine($"{ServiceLocator.Instance.PlayerService.GetBy(player).FullName}");
                }
                Console.WriteLine(playerBuilder.ToString());
                Console.WriteLine("----------------------------------------------------------");
            }
            Console.WriteLine($"Total number matches: {allMatches.Count()}");
        }

        private void PrintAllSeries()
        {
            var allSeries = ServiceLocator.Instance.SerieService.GetAll();
            foreach (var serie in allSeries)
            {
                StringBuilder serieBuilder = new StringBuilder();
                serieBuilder.AppendLine($"Name: {serie.Name.Value}");
                serieBuilder.AppendLine($"Number of teams: {serie.TeamTable.Count}");
                serieBuilder.AppendLine($"Matches:");
                foreach (var match in serie.MatchTable)
                {
                    serieBuilder.Append($"{ServiceLocator.Instance.TeamService.GetBy(ServiceLocator.Instance.MatchService.GetBy(match).HomeTeamId).Name} - ");
                    serieBuilder.Append($"{ServiceLocator.Instance.TeamService.GetBy(ServiceLocator.Instance.MatchService.GetBy(match).VisitorTeamId).Name} - ");
                    serieBuilder.Append($"{ServiceLocator.Instance.MatchService.GetBy(match).Location.ToString()} - ");
                    serieBuilder.AppendLine($"{ServiceLocator.Instance.MatchService.GetBy(match).Date}");
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

            var searchResult = ServiceLocator.Instance.SearchService.Search(userInput, true, true, true, false, true);

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
            Console.WriteLine($"Total number of players: {ServiceLocator.Instance.PlayerService.GetAll().Count()}");
            Console.WriteLine($"Total number of teams: {ServiceLocator.Instance.TeamService.GetAll().Count()}");
            Console.WriteLine($"Total number of matches: {ServiceLocator.Instance.MatchService.GetAll().Count()}");
            Console.WriteLine($"Total number of series: {ServiceLocator.Instance.SerieService.GetAll().Count()}");
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








