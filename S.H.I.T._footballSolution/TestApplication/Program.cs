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

        public Program()
        {
            players = new List<Player>();
        }
        static void Main(string[] args)
        {
            
        }

        public static void AddAndSavePlayers()
        {
            for (int i = 1; i <= 24; i++)
            {
                var player = new Player(new PlayerName("Jonas"), new PlayerName("hanson"), new DateTime(1992 - 08 - 06));
                players.Add(player);
            }
            try
            {
            Save();
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

        }

        public static void Save()
        {
            string path;
            try
            {
                if (TryGetFilePath.InProjectDirectory("Players.xml", "Resorces", true, out path))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Player>));
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        xmlSerializer.Serialize(stream, players);
                    }
                }
                else
                {
                    throw new Exception("Could not save file");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}

