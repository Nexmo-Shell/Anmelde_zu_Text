using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Web;

namespace Anmelde_zu_Text
{
    public static class Controlling_Data
    {

        public static List<string> called_Data = new List<string>();
        public static List<string> pass_Data = new List<string>();
        public static List<string> Textdateien = new List<string>();
        public static bool abfrage;
        public static bool passwort_check = false;
        public static List<string> keydateien = new List<string>();
        public static List<int> laufendeZahl = new List<int>();
        public static int maxKey;
        public static List<string> passwort_reset = new List<string>();

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
                //var input = datei[3];
                //var output = input.Select(x => (byte)x).ToArray();
                //byte[] array = Encoding.ASCII.GetBytes(input);
                string reset = datei[2] + "_" + datei[3];
                string pass = datei[2] + "_" + CrypToFile.Decrypt(int.Parse(datei[3])); /*Cryptor.Decrypt(array)*/;
                pass_Data.Add(pass);
                passwort_reset.Add(reset);
            }
        }

        public static int NextKeyfile()
        {

            Schlüsseldateiabfrage();
            return NextKeyNumber();
        }

        public static void Schlüsseldateiabfrage()
        {
            DirectoryInfo info1 = new DirectoryInfo(@"D:\Umschulung_Fachinformatiker\Praktikum\Textdata\Keys");
            foreach (var fi in info1.GetFiles("*.txt"))
            {
                keydateien.Add(fi.Name);
            }
        }

        public static int NextKeyNumber()
        {
           
            foreach (var s in keydateien)
            {
                string[] subs = s.Split('.');
                string number = subs[0];
                string anhängsel = subs[1];

                int zahl = int.Parse(subs[0]);
                laufendeZahl.Add(zahl);
                if(zahl == 0)
                { maxKey = 0; }
                else
                {
                  maxKey = laufendeZahl.Max(zahl => zahl);
                }
            }

            return maxKey;
        }



    }
}
