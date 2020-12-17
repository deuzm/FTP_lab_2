using System;
using System.IO;

namespace LabAAAAA
{
    public class FileTransferService: ServiceLayer.IFileTransferService
    {
        public string pathToFtp = "";
        
        public FileTransferService()
        {
        }

        public void SendXmlXsdToFtp(string pathXml, string pathXsd, string ftpPath)
        {
            var xmlFileName = Path.GetFileName(pathXml);
            var xsdFileName = Path.GetFileName(pathXsd);
            if(File.Exists(ftpPath + "/" + xmlFileName))
            {
                File.Delete(pathXml);
            }
            else
            {
                File.Move(pathXml, ftpPath + "/" + xmlFileName);
            }
            
            if(File.Exists(pathXsd + "/" + xsdFileName))
            {
                File.Delete(pathXsd);
            }
            else
            {
                File.Move(pathXsd, ftpPath + "/" + xsdFileName);
            }
            Console.Write("moved to ftp");
        }
    }
}
