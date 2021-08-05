using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Anmelde_zu_Text
{
    public static class Controlling_Data
    {

        public static List<string> called_Data = new List<string>();
        public static List<string> pass_Data = new List<string>();
        public static List<string> Textdateien = new List<string>();
        public static bool abfrage;
        public static bool passwort_check = false;

        public static void ControllData()
        {
            OrdnerAbfrage();
            OrdnerAuslesen();
        }

        public static void OrdnerAbfrage()
        {
            DirectoryInfo info = new DirectoryInfo(@"D:\Umschulung_Fachinformatiker\Praktikum\Textdata");
            foreach (var fi in info.GetFiles("*.txt"))
            {
                Textdateien.Add(fi.Name);
            }
        }

        public static void OrdnerAuslesen()
        {
            foreach (var file in Textdateien)
            {
                string[] datei = File.ReadAllLines(@"D:\Umschulung_Fachinformatiker\Praktikum\Textdata\" + file);
                string userdata = datei[2];
                called_Data.Add(userdata);
                string pass = datei[2] + "_" + StringExtension.Reverse(datei[3]);
                pass_Data.Add(pass);
            }
        }

       
    }
}
