# FTP Worker service
This is a ftp worker service written in asp.net core. Unlike Windows service, worker service is crossplatform, meaning it can run as a daemon(in Linux), or worker service(in Windows)

## Functionality
* Source folder watcher
  * Encrypts and gzips file -- moves it to Target -- unarchives and decrypts to Target/Archive
* Configuration provider
  * Gets options from Xml or JSON file depending on presence in file
