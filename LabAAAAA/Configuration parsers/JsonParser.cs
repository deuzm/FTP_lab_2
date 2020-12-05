using System;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace LabAAAAA
{
    internal class JSONParser : IConfigurationParser
    {
        private string jsonString = "";
        private List<Options> optionsAfterJsonParsing = new List<Options>();
        private string PathToJSONFile { get; set; }

        public JSONParser(string pathToJSONfile)
        {
            PathToJSONFile = pathToJSONfile;
            PathToJSONFile += "/" + "appsettings.json";

            if (!File.Exists(pathToJSONfile))
            {
                createConfigurationFile();
            }
        }

        public virtual List<Options> Parse()
        {
            using (var jsonStream = new StreamReader(PathToJSONFile))
            {
                jsonString = jsonStream.ReadToEnd();
            }

            Options jsonOptions = JsonConvert.DeserializeObject<Options>(jsonString);

            if (jsonOptions != null)
            {
                optionsAfterJsonParsing.Add(jsonOptions);
            }
            else
            {
                // TODO handle exception
                throw new NullReferenceException();
            }

            return optionsAfterJsonParsing;
        }

        public virtual void createConfigurationFile()
        {

          IConfiguration config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", true, true)
          .Build();


            using (var jsonStream = new StreamWriter(PathToJSONFile))
            {
                //TODO
                Options options = new Options();
                string serializedOptions = JsonConvert.SerializeObject(options);
                jsonStream.Write(serializedOptions);
            }
        }
    }
}
