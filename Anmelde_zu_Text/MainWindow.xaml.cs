using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Anmelde_zu_Text
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<Login_Speicher> Login_Data = new List<Login_Speicher>();
        private List<string> called_Data = new List<string>();
        private List<string> pass_Data = new List<string>();
        List<string> Textdateien = new List<string>();
        bool abfrage;
        bool passwort_check = false;

        public void InitLogindata()
        {

            Login_Speicher myLogin = new Login_Speicher();
            myLogin.Vorname = Vorname.Text;
            myLogin.Nachname = Nachname.Text;
            myLogin.Benutzername = Benutzer.Text;
            myLogin.Passwort = Password.Password;

            Login_Data.Add(myLogin);
        }

        public void Writer()
        {
            foreach (Login_Speicher log in Login_Data)
            {
                log.Write_Data();
            }
        }

        public void ControllData()
        {
            OrdnerAbfrage();
            OrdnerAuslesen();
            Datenabgleich();

        }

        private void Login(object sender, RoutedEventArgs e)
        {
            InitLogindata();
            ControllData();
        }

        public void OrdnerAbfrage()
        {
            DirectoryInfo info = new DirectoryInfo(@"D:\Umschulung_Fachinformatiker\Praktikum\Textdata");
            foreach (var fi in info.GetFiles("*.txt"))
            {
                Textdateien.Add(fi.Name);
            }
        }

        public void OrdnerAuslesen()
        {   
            foreach (var file in Textdateien)
            {   
                string[] datei = File.ReadAllLines(@"D:\Umschulung_Fachinformatiker\Praktikum\Textdata\" + file);
                string userdata = datei[2];
                called_Data.Add(userdata);
                string pass = datei[2] +"_"+ StringExtension.Reverse(datei[3]);
                pass_Data.Add(pass);
            }
        }

        public void Datenabgleich()
        {
            foreach (var datei in called_Data)
            {
                if (Benutzer.Text == datei)
                {
                    abfrage = true;
                }

            }
            if (abfrage == true)
            {
                MessageBoxResult result1 = MessageBox.Show("Benutzer ist bereits vorhanden. Möchten Sie sich einlogen?", "Achtung!!", MessageBoxButton.YesNo, MessageBoxImage.Information);
                switch (result1)
                {
                    case MessageBoxResult.Yes: Vergleich_daten();
                        break;
                    case MessageBoxResult.No: MessageBox.Show("Bitte nehmen Sie einen anderen Benutzername");Login_Data.Clear();Benutzer.Clear();called_Data.Clear(); pass_Data.Clear(); abfrage = false;
                        break;
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Benutzer ist nicht vorhanden.Neuen Benutzer anlegen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Writer(); MessageBox.Show("Neuer Nutzer wurde angelegt"); Window win = new Login_erfolgreich();
                        win.Show(); Login_Data.Clear(); Benutzer.Clear(); Vorname.Clear(); Nachname.Clear(); Password.Clear();
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Es wurde kein neuer Nutzer angelegt");
                        break;
                }

            }

        }
        public void Vergleich_daten()
        { int i = 0;
            do
            {
                foreach (var s in pass_Data)
                {
                    string[] subs = s.Split('_');
                    string B_Name = subs[0];
                    string Pass_data = subs[1];
                    if (B_Name == Benutzer.Text && Pass_data == Password.Password)
                    {
                        passwort_check = true;
                    }
                    i++;
                }
            } while (i != pass_Data.Count);
            if(passwort_check == true)
            {
                Window win = new Login_erfolgreich();
                win.Show();
            }
            else { MessageBox.Show("Das Passwort und/oder der Benutzername ist nicht richtig. Versuchen Sie es erneut"); }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Benutzer.Clear();
            Password.Clear();
            Vorname.Clear();
            Nachname.Clear();
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
