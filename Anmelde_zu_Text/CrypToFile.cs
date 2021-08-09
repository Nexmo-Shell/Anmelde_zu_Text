using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Anmelde_zu_Text
{
    public static class CrypToFile
    {
        static int i;
        public static string PassWord { get; set; }

        public static int Encrypt(string password)
        {
            i = Controlling_Data.NextKeyfile();
            i++;
            string path = @"D:\Umschulung_Fachinformatiker\Praktikum\Textdata\Keys\";
            string filename = i + ".txt";

            string safepath = path + filename;
            try
            {
                using (FileStream fileStream = new FileStream(safepath, FileMode.OpenOrCreate))
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] key =
                        {
                            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                        };
                        aes.Key = key;

                        byte[] iv = aes.IV;
                        fileStream.Write(iv, 0, iv.Length);

                        using (CryptoStream cryptoStream = new CryptoStream(
                            fileStream,
                            aes.CreateEncryptor(),
                            CryptoStreamMode.Write))
                        {
                            using (StreamWriter encryptWriter = new StreamWriter(cryptoStream))
                            {
                                encryptWriter.WriteLine(password);
                            }
                        }
                    }
                }
                MessageBox.Show("Passwort erfolgreich gespeichert!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"The encryption failed. {ex}");
            }

            return i;
        }

        public static string Decrypt(int übergabezahl)
        {
            string path = @"D:\Umschulung_Fachinformatiker\Praktikum\Textdata\Keys\";
            string filepath = übergabezahl + ".txt";

            string safepath = path + filepath;


            try
            {
                using (FileStream fileStream = new FileStream(safepath, FileMode.Open))
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] iv = new byte[aes.IV.Length];
                        int numBytesToRead = aes.IV.Length;
                        int numBytesRead = 0;
                        while (numBytesToRead > 0)
                        {
                            int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                            if (n == 0) break;

                            numBytesRead += n;
                            numBytesToRead -= n;
                        }

                        byte[] key =
                        {
                             0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                             0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                        };

                        using (CryptoStream cryptoStream = new CryptoStream(
                           fileStream,
                           aes.CreateDecryptor(key, iv),
                           CryptoStreamMode.Read))
                        {
                            using (StreamReader decryptReader = new StreamReader(cryptoStream))
                            {
                                string decryptedMessage =  decryptReader.ReadToEnd();
                                string s = decryptedMessage;
                                PassWord = s[0..^2]; return PassWord;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The decryption failed. {ex}"); return null;
            }
            
        }


    }
}
