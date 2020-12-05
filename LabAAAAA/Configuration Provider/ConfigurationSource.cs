using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace LabAAAAA.ConfigurationProvider
{
    public class ConfigurationSource : IConfigurationSource
    {
        internal string TargetPath { get; set; }
        internal string SourcePath { get; set; }
        internal string ArchivePath { get; set; }
        internal string EncryptionPassword { get; set; }

        public IFileProvider FileProvider { get; private set; }

        public ConfigurationSource(Options options)
        {
            TargetPath = options.TargetPath;
            SourcePath = options.SourcePath;
            ArchivePath = options.ArchivePath;
            EncryptionPassword = options.EncryptionPassword;

        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            FileProvider = FileProvider ?? builder.GetFileProvider();
            //return new Provider(this);
            throw new NotImplementedException("DDD");
        }

        //public IConfigurationProvider Build(IConfigurationBuilder builder)
        //{
        //    return new Provider();
        //}
    }

    public class MyConfigurationOptions
    {
        public string ConnectionString { get; set; }
        public string Query { get; set; }
    }
}
