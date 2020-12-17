using System;
namespace LabAAAAA
{
    public class DataManager
    {
        DataAccess data;
        Models model;
        XmlGeneratorService xmlGenerator;
        FileTransferService fileTransfer;

        string customersFileName = "Customers.xml";
        string peoplesFileName = "People.xml";
        string storesFileName = "Stores.xml";

        string initialPath = "/Users/lizamalinovskaa/Projects/LabAAAAA/LabAAAAA";
        string pathCustomers = "/Users/lizamalinovskaa/Projects/LabAAAAA/LabAAAAA";
        string ftpPath = "/Users/lizamalinovskaa/Projects/LabAAAAA/Source";
        string pathPeople = "";
        string pathStores = "";


        public DataManager()
        {
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
