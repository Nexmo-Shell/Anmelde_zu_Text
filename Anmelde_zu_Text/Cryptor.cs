using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Windows;

namespace Anmelde_zu_Text
{
    public class Cryptor
    {
        string cipherData;
        byte[] chipherBytes;
        byte[] plainBytes;
        byte[] plainBytes2;
        byte[] plainKey;

        SymmetricAlgorithm desObj;
        

        public string Encrypt(string s)
        {
            desObj = Rijndael.Create();

            cipherData = s;
            plainBytes = Encoding.ASCII.GetBytes(cipherData);
            plainKey = Encoding.ASCII.GetBytes("0123456789abcdef");
            desObj.Key = plainKey;
            desObj.Mode = CipherMode.CBC;
            desObj.Padding = PaddingMode.PKCS7;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desObj.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(plainBytes, 0, plainBytes.Length);
            cs.Close();
            chipherBytes = ms.ToArray();
            ms.Close();
            return Encoding.ASCII.GetString(chipherBytes);

        }

        public string Decrypt()
        {
            
            MemoryStream ms1 = new MemoryStream(chipherBytes);
            CryptoStream cs1 = new CryptoStream(ms1, desObj.CreateDecryptor(), CryptoStreamMode.Write);

            cs1.Read(chipherBytes, 0, chipherBytes.Length);
            plainBytes2 = ms1.ToArray();
            cs1.Close();
            ms1.Close();

            return Encoding.ASCII.GetString(plainBytes2);
        }

        //public static string Encryptor(this string s)
        //{   string c =s;
        //    try
        //    {

        //        using (FileStream fileStream = new FileStream(c, FileMode.OpenOrCreate))
        //        {
        //            using (Aes aes = Aes.Create())
        //            {
        //                byte[] key =
        //                {
        //                     0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
        //                     0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
        //                };
        //                aes.Key = key;

        //                byte[] iv = aes.IV;
        //                fileStream.Write(iv, 0, iv.Length);

        //                using (CryptoStream cryptoStream = new CryptoStream(
        //                    fileStream,
        //                    aes.CreateEncryptor(),
        //                    CryptoStreamMode.Write))
        //                {
        //                    using(StreamWriter encryptWriter = new StreamWriter(cryptoStream))
        //                    {
        //                        encryptWriter.WriteLine(c);
        //                    }
        //                }
        //            }
        //        }
        //        return new string(c);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"The encryption failed. {ex}");
        //         return null;
        //    }
        //}
    }
}
