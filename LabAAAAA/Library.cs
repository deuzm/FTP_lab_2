using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace LabAAAAA
{
    public class Library
    {
        public Library()
        {
        }

        public void Compress(string sourceFile, string compressedFile)
        {
            // поток для чтения исходного файла
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
        }

        public void Decompress(string compressedFile, string targetFile)
        {
            // поток для чтения из сжатого файла
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                // поток для записи восстановленного файла
                using (FileStream targetStream = File.Create(targetFile))
                {
                    // поток разархивации
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                    }
                }
            }
        }

        public void ProcessFile(string inputPath, string password, bool encryptMode, string outputPath)
        {
            using (var cypher = new AesManaged())
            using (var fsIn = new FileStream(inputPath, FileMode.Open))
            using (var fsOut = new FileStream(outputPath, FileMode.Create))
            {
                const int saltLength = 256;
                var salt = new byte[saltLength];
                var iv = new byte[cypher.BlockSize / 8];

                if (encryptMode)
                {
                    // Generate random salt and IV, then write them to file
                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(salt);
                        rng.GetBytes(iv);
                    }
                    fsOut.Write(salt, 0, salt.Length);
                    fsOut.Write(iv, 0, iv.Length);
                }
                else
                {
                    // Read the salt and IV from the file
                    fsIn.Read(salt, 0, saltLength);
                    fsIn.Read(iv, 0, iv.Length);
                }

                // Generate a secure password, based on the password and salt provided
                var pdb = new Rfc2898DeriveBytes(password, salt);
                var key = pdb.GetBytes(cypher.KeySize / 8);

                // Encrypt or decrypt the file
                using (var cryptoTransform = encryptMode
                    ? cypher.CreateEncryptor(key, iv)
                    : cypher.CreateDecryptor(key, iv))
                using (var cs = new CryptoStream(fsOut, cryptoTransform, CryptoStreamMode.Write))
                {
                    fsIn.CopyTo(cs);
                }
            }
        }
    }
}

