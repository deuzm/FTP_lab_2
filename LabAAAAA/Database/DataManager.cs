using System;
using System.Collections.Generic;
namespace LabAAAAA
{
    public class DataManager
    {
        DataAccess data;
        Models model;
        XmlGeneratorService xmlGenerator;
        FileTransferService fileTransfer;

        string configurationDirectoryPath = "/Users/lizamalinovskaa/Projects/LabAAAAA/LabAAAAA/ConfigurationFiles/DatabaseConfiguration";

        List<DBConfigurationOptions> optionsList;
        Provider<DBConfigurationOptions> config;

        string customersFileName;
        string peoplesFileName;
        string storesFileName;

        string initialPath;
        string pathCustomers;
        string ftpPath;


        public DataManager()
        {
            config = new Provider<DBConfigurationOptions>(configurationDirectoryPath);
            config.Load();
            optionsList = config.dbOptions;
            customersFileName = optionsList[0].CustomersFileName;
            peoplesFileName = optionsList[0].PeoplesFileName;
            storesFileName = optionsList[0].StoresFileName;

            initialPath = optionsList[0].InitialPath;
            pathCustomers = optionsList[0].PathCustomers;
            ftpPath = optionsList[0].FtpPath;

            data = new DataAccess();
            xmlGenerator = new XmlGeneratorService();
            fileTransfer = new FileTransferService();
            model = new Models(data.ReadPerson(), data.ReadCustomer(), data.ReadStore());

        }

        public void CreateFiles()
        {
            xmlGenerator.GenerateXmlFile(initialPath + "/" + customersFileName, model.Customers);
            xmlGenerator.GenerateXsdFromXml(initialPath + "/" + customersFileName);
            fileTransfer.SendXmlXsdToFtp(initialPath + "/" + customersFileName, initialPath + "/" + customersFileName.Replace(".xml", ".xsd"), ftpPath);

            xmlGenerator.GenerateXmlFile(initialPath + "/" + peoplesFileName, model.Customers);
            xmlGenerator.GenerateXsdFromXml(initialPath + "/" + peoplesFileName);
            fileTransfer.SendXmlXsdToFtp(initialPath + "/" + peoplesFileName, initialPath + "/" + peoplesFileName.Replace(".xml", ".xsd"), ftpPath);

            xmlGenerator.GenerateXmlFile(initialPath + "/" + storesFileName, model.Customers);
            xmlGenerator.GenerateXsdFromXml(initialPath + "/" + storesFileName);
            fileTransfer.SendXmlXsdToFtp(initialPath + "/" + storesFileName, initialPath + "/" + storesFileName.Replace(".xml", ".xsd"), ftpPath);
        }
    }
}
