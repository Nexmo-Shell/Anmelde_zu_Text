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


        private void Login(object sender, RoutedEventArgs e)
        {
            Controlling_Data.ControllData();
            Datenabgleich();
        }



        public void Datenabgleich()
        {
            foreach (var datei in Controlling_Data.called_Data)
            {
                if (Benutzer.Text == datei)
                {
                    Controlling_Data.abfrage = true;
                }

            }
            if (Controlling_Data.abfrage == true)
            {
                MessageBoxResult result1 = MessageBox.Show("Benutzer ist bereits vorhanden. Möchten Sie sich einlogen?", "Achtung!!", MessageBoxButton.YesNo, MessageBoxImage.Information);
                switch (result1)
                {
                    case MessageBoxResult.Yes:
                        Vergleich_daten();
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Bitte Registrieren Sie sich"); Benutzer.Clear(); Controlling_Data.called_Data.Clear(); Controlling_Data.pass_Data.Clear(); Controlling_Data.abfrage = false;
                        break;
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Benutzer ist nicht vorhanden.Neuen Benutzer anlegen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Yes: Window win = new Registrierung();win.Show();
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Es wurde kein neuer Nutzer angelegt");
                        break;
                }

            }

        }
        public void Vergleich_daten()
        {
            int i = 0;
            do
            {
                foreach (var s in Controlling_Data.pass_Data)
                {
                    string[] subs = s.Split('_');
                    string B_Name = subs[0];
                    string Pass_data = subs[1];
                    if (B_Name == Benutzer.Text && Pass_data == Password.Password)
                    {
                        Controlling_Data.passwort_check = true;
                    }
                    i++;
                }
            } while (i != Controlling_Data.pass_Data.Count);
            if (Controlling_Data.passwort_check == true)
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
            
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private void Registrieren_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Registrierung();
            win.Show();
        }
    }
}
