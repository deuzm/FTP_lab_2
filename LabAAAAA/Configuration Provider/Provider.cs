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

    public class Provider
    {
        private readonly IConfiguration Configuration;
        public List<Options> option { get; private set; }
        private ConfigurationSource configurationSource;
        private string directoryPath = "/Users/lizamalinovskaa/Projects/LabAAAAA/LabAAAAA/ConfigurationFiles";
         
        JSONParser jsonObject;
        XMLParser xmlObject;

        public Provider()
        {
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
            var xmlParser = new XMLParser(directoryPath);
            var JsonParser = new JSONParser(directoryPath);

            string[] Files = new string[100];
            Files = Directory.GetFiles(directoryPath);
            foreach (string s in Files)
            {

                if (Path.GetExtension(s) == ".json")
                {
                    option = JsonParser.Parse();
                }
                else
                {
                    if (Path.GetExtension(s) == ".xml")
                    {
                        option = xmlParser.Parse();
                    }
                }
            }
        }
    }
}


