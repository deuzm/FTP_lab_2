using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LabAAAAA
{
    internal class XmlParser<T> : IConfigurationParser<T> where T : new()
    {

        public static string configFileName = "config.xml";
        string PathToXmlFile { get; set; }
        public XmlParser(string pathToXmlFile)
        {
            PathToXmlFile = pathToXmlFile;
            PathToXmlFile += "/" + configFileName;
            if(!File.Exists(pathToXmlFile))
            {
                createConfigurationFile();
            }
        }


        public virtual List<T> Parse()
        {
            List<T> optionsList = new List<T>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T parsedOptions = new T();

            using (var xmlRead = new FileStream(PathToXmlFile, FileMode.OpenOrCreate))
            {
                parsedOptions = (T)xmlSerializer.Deserialize(xmlRead);
            }

            if (parsedOptions != null)
            {
                optionsList.Add(parsedOptions);
            }
            else
            {
                throw new NullReferenceException();
            }

            return optionsList;
        }

        public virtual void createConfigurationFile(string fileName, string PathToFile, T option)
        {

            using (FileStream fs = new FileStream(PathToFile, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));

                xs.Serialize(fs, option);
            }
        }

        public virtual void createConfigurationFile()
        {

            using (FileStream fs = new FileStream(PathToXmlFile, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                T sxml = new T();
                xs.Serialize(fs, sxml);
            }
        }
    }
}
