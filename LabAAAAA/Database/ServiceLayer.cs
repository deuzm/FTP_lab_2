using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace LabAAAAA
{
    enum SqlCommandType
    {
        ReadPerson = 0,
        ReadStore,
        ReadCustomer
    }

    public class ServiceLayer
    {
        public interface IDataAccess
        {
            List<string> SqlCommandStrings { get; set; }
            SqlConnectionStringBuilder Builder { get; set; }

            List<Person> ReadPerson();
            List<Store> ReadStore();
            List<Customer> ReadCustomer();
        }

        public interface IXmlGeneratorService
        {
            public void GenerateXsdFromXml(string path);
            public void GenerateXmlFile(string path, List<Customer> obj);
            public void GenerateXmlFile(string path, List<Person> obj);
            public void GenerateXmlFile(string path, List<Store> obj);

        }

        public interface IFileTransferService
        {
            public void SendXmlXsdToFtp(string pathXml, string pathXsd, string ftpPath);
        }


        public ServiceLayer()
        {
        }
    }
}
