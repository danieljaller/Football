using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FootballEngine.Helper
{
    public static class LoadAndSaveToXml
    {
        public static object LoadFrom(string filePath, Type typeToLoad)
        {
            try
            {
                object loadedObject;
                using (Stream stream = File.Open(filePath, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeToLoad.GetType());
                    loadedObject = xmlSerializer.Deserialize(stream);
                }

                return loadedObject;
            }
            catch (Exception e)
            {
                throw new Exception($"Could not load {filePath}", e);
            }
        }

        public static void SaveTo(string filePath, object objectToSave)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(objectToSave.GetType());
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
