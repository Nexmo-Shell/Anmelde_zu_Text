﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Anmelde_zu_Text
{
    public class Login_Speicher
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Benutzername { get; set; }
        public string Passwort { get; set; }
        

        string path = @"D:\Umschulung_Fachinformatiker\Praktikum\Textdata\";

        public void Write_Data()
        {     
            int i = CrypToFile.Encrypt(Passwort);
            string[] write = { Vorname, Nachname, Benutzername, i.ToString() };
            string pathing = path + Vorname+"_"+Nachname + ".txt";

            File.WriteAllLines(pathing, write);
        }

       
    }
}
