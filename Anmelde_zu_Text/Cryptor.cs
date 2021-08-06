using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Windows;

namespace Anmelde_zu_Text
{
    public static class Cryptor
    {   
        
        
        public static string Encryptor(this string s)
        {   string c =s;
            try
            {
                
                using (FileStream fileStream = new FileStream(c, FileMode.OpenOrCreate))
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
                            using(StreamWriter encryptWriter = new StreamWriter(cryptoStream))
                            {
                                encryptWriter.WriteLine(c);
                            }
                        }
                    }
                }
                return new string(c);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The encryption failed. {ex}");
                 return null;
            }
        }
    }
}
