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
            AddAndSavePlayers();
            Console.WriteLine("Save successful");
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
                var player = new Player(new PlayerName("Pelle"), new PlayerName("Hanson"), new DateTime(1992 - 08 - 06));
                player.TeamId = teamId;
                players.Add(player);
            }
            try
            {
                SaveToXML(players, playerFileName);
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

        }

        public static void SaveToXML(object o, string fileName)
        {
            string path;
            Exception innerException = null;
            if (TryGetFilePath.InProjectDirectory(fileName, directoryName, true, out path))
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(o.GetType());
                    using (Stream stream = File.Open(path, FileMode.Create))
                    {
                        xmlSerializer.Serialize(stream, players);
                    }
                }
                catch (Exception exception)
                {
                    innerException = exception;
                }
            }
            else
            {
                throw new Exception($"Could not save to {fileName}", innerException);
            }
        }

    }

}

