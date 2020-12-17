using System;
namespace LabAAAAA
{
    public class DBConfigurationOptions
    {
        public string readPersonCommand;

        public string readStoreCommand;

        public string readCustomerCommand;
        public string DataSource;
        public string UserID;
        public string Password;  
        public string InitialCatalog;

        public string CustomersFileName;
        public string PeoplesFileName;
        public string StoresFileName;

        public string InitialPath;
        public string PathCustomers;
        public string FtpPath;

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
