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

namespace TestApplication
{
    class Program
    {
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

