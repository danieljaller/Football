using FootballEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FootballEngine.Helper
{
    public static class XmlHandler
    {
        public static object LoadFrom(string filePath, Type typeToLoad)
        {
            try
            {
                object loadedObject;
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeToLoad);
                    loadedObject = xmlSerializer.Deserialize(stream);
                }

                return loadedObject;
            }
            catch (Exception innerException)
            {
                throw new Exception($"Could not load {filePath}", innerException);
            }
        }

        public static void SaveTo(string filePath, object objectToSave)
        {
            try
            {
                Type typeToSave;

                if (objectToSave.GetType() == typeof(List<Match>))
                {
                    objectToSave = objectToSave as List<Match>;
                    typeToSave = objectToSave.GetType();
                }

                else if (objectToSave.GetType() == typeof(List<Player>))
                {
                    objectToSave = objectToSave as List<Player>;
                    typeToSave = objectToSave.GetType();
                }

                else if (objectToSave.GetType() == typeof(List<Serie>))
                {
                    objectToSave = objectToSave as List<Serie>;
                    typeToSave = objectToSave.GetType();
                }

                else if (objectToSave.GetType() == typeof(List<Team>))
                {
                    objectToSave = objectToSave as List<Team>;
                    typeToSave = objectToSave.GetType();
                }
                else
                {
                    throw new Exception("Not a valid type to save");
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeToSave);
                using (Stream stream = File.Open(filePath, FileMode.Create))
                {
                    xmlSerializer.Serialize(stream, objectToSave);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Could not save file", e);
            }
        }
    }
}
