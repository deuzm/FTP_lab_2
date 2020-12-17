using System;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace LabAAAAA
{
    internal class JsonParser<T> : IConfigurationParser<T> where T: new()
    {
        private string jsonString = "";
        private List<T> optionsAfterJsonParsing = new List<T>();
        private string PathToJSONFile { get; set; }

        public JsonParser(string pathToJSONfile)
        {
            PathToJSONFile = pathToJSONfile;
            PathToJSONFile += "/" + "appsettings.json";

            if (!File.Exists(pathToJSONfile))
            {
                createConfigurationFile();
            }
        }

        public virtual List<T> Parse()
        {
            using (var jsonStream = new StreamReader(PathToJSONFile))
            {
                jsonString = jsonStream.ReadToEnd();
            }

            T jsonOptions = JsonConvert.DeserializeObject<T>(jsonString);

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
        public virtual void createConfigurationFile(string fileName, string PathToFile, T option)
        {

            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile(fileName, true, true)
            .Build();

            using (var jsonStream = new StreamWriter(PathToFile))
            {
                //TODO

                string serializedOptions = JsonConvert.SerializeObject(option);
                jsonStream.Write(serializedOptions);
            }
        }

        public virtual void createConfigurationFile()
        {

            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            using (var jsonStream = new StreamWriter(PathToJSONFile))
            {
                //TODO
                T options = new T();
                string serializedOptions = JsonConvert.SerializeObject(options);
                jsonStream.Write(serializedOptions);
            }
        }
    }
}
