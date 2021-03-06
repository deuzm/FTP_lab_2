# FTP Worker service
This is a ftp worker service written in asp.net core. Unlike Windows service, worker service is crossplatform, meaning it can run as a daemon(in Linux), or worker service(in Windows)

## Functionality
* Source folder watcher
  * Encrypts and gzips file -- moves it to Target -- unarchives and decrypts to Target/Archive
* Configuration provider
  * Gets options from Xml or JSON file depending on presence in file

## Lab 2
* **Library.cs**
  * Contains Compress, Decompress, ProcessFile(encrypt) functions. I used 256 aes coding for encryption.
* **Logger.cs**
  * Archive folder watcher. Moves files from archive directory to Target directory, unarchives and decrypts files and moves them to Target/Archive
* **Worker.cs**
  * Workers Service is an alternative to Windows Service. It is broader because allows you to run it as Windows or Linux service(daemon)

## Lab 3
Look to *Configuration Provider*, *Configuration Files* and *Configuration parsers* folders.
* **Provider.cs** (configuration provider)
  * Calls xml or json parser depending on presence in ConfigurationFiles directory. 
* **IConfigurationParser.cs**
  * Interface for xml and json parser
    * **XmlParser.cs** 
    * **JsonParser.cs**

## Lab 4
In this lab I fetch data from AdventureWorks2019 server. Create xml, xsd files from this data and relocate it to ftp server.
Look to *Database* and *Models* folders.
* *Database* folder
  * ServiceLayer.cs -- provides a set of interfaces for data access, file transfer and xmlGeneratorService functiionality
  * DataAccess.cs -- provides connection and fetching data from AdventureWorks2019 database
  * XmlGeneratorService.cs -- provides xml and xsd file generation from data from the server
  * FileTransferService.cs -- sends xml and xsd files to ftp server
  * DataManager -- connects every service;
* *Models* folder
  * Contains three models used to store fetched data from server
  
  DataAccess and DataManagers configurations are located at ConfigurationFiles/DatabaseConfiguration folders;
  
