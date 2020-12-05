using System;
namespace LabAAAAA
{
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
