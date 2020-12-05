using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LabAAAAA
{
    internal class XMLParser : IConfigurationParser
    {

        public static string configFileName = "config.xml";
        string PathToXmlFile { get; set; }
        public XMLParser(string pathToXmlFile)
        {
            PathToXmlFile = pathToXmlFile;
            PathToXmlFile += "/" + configFileName;
            if(!File.Exists(pathToXmlFile))
            {
                createConfigurationFile();
            }
        }


        public virtual List<Options> Parse()
        {
            List<Options> optionsList = new List<Options>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Options));
            Options parsedOptions = new Options();

            using (var xmlRead = new FileStream(PathToXmlFile, FileMode.OpenOrCreate))
            {
                parsedOptions = (Options)xmlSerializer.Deserialize(xmlRead);
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

        public virtual void createConfigurationFile()
        {

            using (FileStream fs = new FileStream(PathToXmlFile, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Options));
                Options sxml = new Options();
                xs.Serialize(fs, sxml);
            }
        }
    }
}
