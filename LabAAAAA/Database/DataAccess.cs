using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace LabAAAAA
{
    public class DataAccess: ServiceLayer.IDataAccess
    {
        internal SqlConnection SqlConnection;
        public SqlConnectionStringBuilder Builder { get; set; }
        public List<string> SqlCommandStrings { get; set; }
        string configurationDirectoryPath = "/Users/lizamalinovskaa/Projects/LabAAAAA/LabAAAAA/ConfigurationFiles/DatabaseConfiguration";


        List<DBConfigurationOptions> optionsList;
        Provider<DBConfigurationOptions> config;

        public DataAccess()
        {
            config = new Provider<DBConfigurationOptions>(configurationDirectoryPath);
            config.Load();
            optionsList = config.dbOptions;

            Builder = new SqlConnectionStringBuilder();
            Builder.DataSource = optionsList[0].DataSource;
            Builder.UserID = optionsList[0].UserID;
            Builder.Password = optionsList[0].Password;
            Builder.InitialCatalog = optionsList[0].InitialCatalog;

            SqlCommandStrings = new List<string>();
            SqlCommandStrings.Insert((int)SqlCommandType.ReadPerson, optionsList[0].readPersonCommand);
            SqlCommandStrings.Insert((int)SqlCommandType.ReadStore, optionsList[0].readStoreCommand);
            SqlCommandStrings.Insert((int)SqlCommandType.ReadCustomer, optionsList[0].readCustomerCommand);

            ConnectToDB();
            ReadPerson();
            ReadStore();
            ReadCustomer();
            
        }

        private void ConnectToDB()
        {
            SqlConnection = new SqlConnection(Builder.ConnectionString);

            try
            {
                SqlConnection.Open();
                Console.WriteLine("done");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Disconnect()
        {
            //Disconnecting
            try
            {
                SqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        //TODO

        public List<Person> ReadPerson()
        {
            string xml = "";
            using (var con = new SqlConnection(Builder.ConnectionString))
            using (var c = new SqlCommand(SqlCommandStrings[(int)SqlCommandType.ReadPerson], con))
            {
                //c.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                using (var adapter = new SqlDataAdapter(c))
                {
                    var ds = new DataSet("Person");
                    ds.Tables.Add("Person");
                    adapter.Fill(ds, ds.Tables[0].TableName);
                    xml = ds.GetXml();
                }

                // We need to specify the root element
                var rootAttribute = new XmlRootAttribute();
                
                rootAttribute.ElementName = "Person";

                var xs = new XmlSerializer(typeof(List<Person>), rootAttribute);

                object resul = xs.Deserialize(new StringReader(xml));
                List<Person> results = (List<Person>)xs.Deserialize(new StringReader(xml));

                return results;
            }

        }

        public List<Store> ReadStore()
        {
            string xml = "";
            using (var con = new SqlConnection(Builder.ConnectionString))
            using (var c = new SqlCommand(SqlCommandStrings[(int)SqlCommandType.ReadStore], con))
            {
                con.Open();
                using (var adapter = new SqlDataAdapter(c))
                {
                    var ds = new DataSet("Store");
                    ds.Tables.Add("Store");
                    adapter.Fill(ds, ds.Tables[0].TableName);
                    xml = ds.GetXml();
                }

                // We need to specify the root element
                var rootAttribute = new XmlRootAttribute();

                rootAttribute.ElementName = "Store";

                var xs = new XmlSerializer(typeof(List<Store>), rootAttribute);

                object resul = xs.Deserialize(new StringReader(xml));
                List<Store> results = (List<Store>)xs.Deserialize(new StringReader(xml));

                return results;
            }
        }

        public List<Customer> ReadCustomer()
        {
            string xml = "";
            using (var con = new SqlConnection(Builder.ConnectionString))
            using (var c = new SqlCommand(SqlCommandStrings[(int)SqlCommandType.ReadCustomer], con))
            {
                con.Open();
                using (var adapter = new SqlDataAdapter(c))
                {
                    var ds = new DataSet("Customer");
                    ds.Tables.Add("Customer");
                    adapter.Fill(ds, ds.Tables[0].TableName);
                    xml = ds.GetXml();
                }

                // We need to specify the root element
                var rootAttribute = new XmlRootAttribute();

                rootAttribute.ElementName = "Customer";

                var xs = new XmlSerializer(typeof(List<Customer>), rootAttribute);

                object resul = xs.Deserialize(new StringReader(xml));
                List<Customer> results = (List<Customer>)xs.Deserialize(new StringReader(xml));

                return results;
            }
        }
    }
}
