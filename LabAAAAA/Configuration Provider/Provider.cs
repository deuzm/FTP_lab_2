using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

using LabAAAAA.ConfigurationProvider;

namespace LabAAAAA
{

    public class Provider<T> where T: new()
    {
        private readonly IConfiguration Configuration;
        public List<Options> loggerOptions { get; private set; }
        public List<DBConfigurationOptions> dbOptions { get; private set; }

        private ConfigurationSource configurationSource;
        private string directoryPath = "/Users/lizamalinovskaa/Projects/LabAAAAA/LabAAAAA/ConfigurationFiles";
         
        JsonParser<Options> jsonObject;
        XmlParser<Options> xmlObject;

        JsonParser<DBConfigurationOptions> jsonDb;
        XmlParser<DBConfigurationOptions> xmlDb;

        public Provider(string directoryPath)
        {
            this.directoryPath = directoryPath;
        }

        public Provider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Provider(ConfigurationSource configurationSource)
        {
            this.configurationSource = configurationSource;
        }

        public void Load()
        {
            //TODO
            if (directoryPath.Contains("WatcherConfiguration"))
            {
                var xmlParser = new XmlParser<Options>(directoryPath);
                var JsonParser = new JsonParser<Options>(directoryPath);
                string[] Files = new string[100];
                Files = Directory.GetFiles(directoryPath);

                foreach (string s in Files)
                {

                    if (Path.GetExtension(s) == ".json")
                    {
                        loggerOptions = JsonParser.Parse();
                    }
                    else
                    {
                        if (Path.GetExtension(s) == ".xml")
                        {
                            loggerOptions = xmlParser.Parse();
                        }
                    }
                }
            }
           else if(directoryPath.Contains("DatabaseConfiguration"))
            {
                xmlDb = new XmlParser<DBConfigurationOptions>(directoryPath);
                jsonDb = new JsonParser<DBConfigurationOptions>(directoryPath);
                string[] Files = new string[100];
                Files = Directory.GetFiles(directoryPath);

                foreach (string s in Files)
                {

                    if (Path.GetExtension(s) == ".json")
                    {
                        dbOptions = jsonDb.Parse();
                    }
                    else
                    {
                        if (Path.GetExtension(s) == ".xml")
                        {
                            dbOptions = xmlDb.Parse();
                        }
                    }
                }
            }
        }
    }
}


