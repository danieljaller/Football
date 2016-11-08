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
                var player = new Player(new PlayerName("Peter"), new PlayerName("Hanson"), new DateTime(1992 - 08 - 06));
                player.Team = teamId;
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
                Exception e = null;

                if (TryGetFilePath.InProjectDirectory("Players.xml", "Resources", true, out path))
                {
                
                    try
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Player>));
                        using (Stream stream = File.Open(path, FileMode.Create))
                        {
                            xmlSerializer.Serialize(stream, players);
                        }
                    }
                    catch (Exception f)
                    {
                        e = f;
                    }
                }
                else
                {
                    throw new Exception("Could not save file", e);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }

}

