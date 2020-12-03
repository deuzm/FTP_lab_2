using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace LabAAAAA
{
    public class Worker : BackgroundService
    {
        Logger logger;

        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger = new Logger();
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
            //DO YOUR STUFF HERE 
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.Stop();
            Thread.Sleep(1000);
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }

    class Logger
    {
        Library library = new Library();
        FileSystemWatcher watcher;
        String archivePath = "/Users/lizamalinovskaa/Projects/LabAAAAA/Target/Archive/";
        string targetPath = "/Users/lizamalinovskaa/Projects/LabAAAAA/Target/";
        object obj = new object();
        bool enabled = true;
        public Logger()
        {
            // TODO
            watcher = new FileSystemWatcher("/Users/lizamalinovskaa/Projects/LabAAAAA/Source");
            watcher.Deleted += Watcher_Deleted;
            watcher.Created += Watcher_Created;
            watcher.Changed += Watcher_Changed;
            watcher.Renamed += Watcher_Renamed;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }
        // переименование файлов
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string fileEvent = "переименован в " + e.FullPath;
            string filePath = e.OldFullPath;
            RecordEntry(fileEvent, filePath);
        }
        // изменение файлов
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "изменен";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }
        // создание файлов
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "создан";
            string filePath = e.FullPath;
            String dayPath = DateTime.Today.Year + "/"
                           + DateTime.Today.Month + "/"
                           + DateTime.Today.Day;
            System.IO.Directory.CreateDirectory(archivePath + dayPath);

            //Encrypting and compressing file to target directory
            library.ProcessFile(filePath, "naa", true, targetPath + e.Name);
            library.Compress(targetPath + e.Name, targetPath + "/" + e.Name + ".gz");

            //Decompressing and decrypting file to archive directory
            library.Decompress(targetPath + "/" + e.Name + ".gz", archivePath + dayPath + "/" + e.Name);
            library.ProcessFile(archivePath + dayPath + "/" + e.Name, "naa", false, archivePath + dayPath + "/" + "Decrypted_" + e.Name );

            File.Delete(archivePath + dayPath + "/" + e.Name);
            File.Delete(targetPath + e.Name);

            RecordEntry(fileEvent, filePath);
        }
        // удаление файлов
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string fileEvent = "удален";
            string filePath = e.FullPath;
            RecordEntry(fileEvent, filePath);
        }

        private void RecordEntry(string fileEvent, string filePath)
        {
            lock (obj)
            {
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
