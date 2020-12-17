using System;
namespace LabAAAAA
{
    public class DBConfigurationOptions
    {
        public string readPersonCommand = "SELECT [SalesPerson].[BusinessEntityID]," +
       "[SalesPerson].[TerritoryID]," +
      " [SalesPerson].[SalesQuota]," +
      " [SalesPerson].[Bonus]," +
       "[SalesPerson].[SalesLastYear]" +
       "FROM[AdventureWorks2019].[Sales].[SalesPerson]";

        public string readStoreCommand = "SELECT [Store].[BusinessEntityID]," +
        "[Store].[Name]," +
        "[Store].[SalesPersonID]" +
        "FROM[AdventureWorks2019].[Sales].[Store]";

        public string readCustomerCommand = "SELECT TOP 1000 [Customer].[CustomerID]," +
                           "[Customer].[StoreID]," +
                           "[Customer].[TerritoryID]" +
                           "FROM[AdventureWorks2019].[Sales].[Customer]";
        public string DataSource = "localhost";
        public string UserID = "sa";
        public string Password = "liza0807TIOL";  
        public string InitialCatalog = "master";

        public DBConfigurationOptions()
        {
        }
    }

    public class Options
    {
        public string TargetPath { get; set; }
        public string SourcePath { get; set; }
        public string ArchivePath { get; set; }
        public string EncryptionPassword { get; set; }

        public Options()
        {
            TargetPath = "/Users/lizamalinovskaa/Projects/LabAAAAA/Target";
            SourcePath = "/Users/lizamalinovskaa/Projects/LabAAAAA/Source";
            ArchivePath = "/Users/lizamalinovskaa/Projects/LabAAAAA/Target/Archive/";
            EncryptionPassword = "naa";
        }
    }
}
