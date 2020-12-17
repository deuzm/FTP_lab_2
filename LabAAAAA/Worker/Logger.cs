using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace LabAAAAA
{
    class Logger
    {
        string configurationDirectoryPath = "/Users/lizamalinovskaa/Projects/LabAAAAA/LabAAAAA/ConfigurationFiles/WatcherConfiguration";
        private Provider<Options> config;
        public List<Options> options;

        FileManager library = new FileManager();

        //Watches source directory, moves files to target, unarchives it to date folders.
        FileSystemWatcher sourceWatcher;
        //Watches target directory, deletes new files, target is a readonly folder.
        FileSystemWatcher targetWatcher;

        //Options
        string archivePath;
        string targetPath;
        string sourcePath;
        string labPath;
        string encryptionPassword;

        object obj = new object();
        bool enabled = true;

        public Logger()
        {
            config = new Provider<Options>(configurationDirectoryPath);
            config.Load();
            options = config.loggerOptions;

            archivePath = options[0].ArchivePath;
            targetPath = options[0].TargetPath;
            sourcePath = options[0].SourcePath;
            encryptionPassword = options[0].EncryptionPassword;


            sourceWatcher = new FileSystemWatcher(sourcePath);
            sourceWatcher.Created += Watcher_Created;

            targetWatcher = new FileSystemWatcher(targetPath);
            targetWatcher.Created += Watcher_Created;
        }

        public void Start()
        {
            sourceWatcher.EnableRaisingEvents = true;
            targetWatcher.EnableRaisingEvents = true;

            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            sourceWatcher.EnableRaisingEvents = false;
            targetWatcher.EnableRaisingEvents = false;
            enabled = false;
        }
       
        // file created
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {

            string filePath = e.FullPath;
            string fileEvent = "создан";

            if (filePath.Contains(sourcePath))
            {
                //If we are in source directory, create folder, unarchive and decrypt file

                String dayPath = DateTime.Today.Year + "/"
                               + DateTime.Today.Month + "/"
                               + DateTime.Today.Day;
                System.IO.Directory.CreateDirectory(archivePath + dayPath);

                //Encrypting and compressing file to target directory
                library.ProcessFile(filePath, encryptionPassword, true, targetPath + "/" + e.Name);
                library.Compress(targetPath + "/" + e.Name, targetPath + "/" + e.Name + ".gz");

                //Decompressing and decrypting file to archive directory
                library.Decompress(targetPath + "/" + e.Name + ".gz", archivePath + dayPath + "/" + e.Name);
                library.ProcessFile(archivePath + dayPath + "/" + e.Name, encryptionPassword, false, archivePath + dayPath + "/" + "Decrypted_" + e.Name);

                File.Delete(archivePath + dayPath + "/" + e.Name);
                File.Delete(targetPath + "/" + e.Name);

            }
            //else if(filePath.Contains(targetPath))
            //{
            //    //If we are in target directory, delete file on creation
            //    File.Delete(targetPath + "/" + e.Name);
            //    fileEvent = "В данной папке файл не может быть создан";
            //}
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
                //Recording actions in templog.txt
                using (StreamWriter writer = new StreamWriter("D:\\templog.txt", true))
                {
                    writer.WriteLine(String.Format("{0} файл {1} был {2}",
                        DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), filePath, fileEvent));
                    writer.Flush();
                }
            }
        }
    }
}
