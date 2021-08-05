using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Anmelde_zu_Text
{
    /// <summary>
    /// Interaktionslogik für Registrierung.xaml
    /// </summary>
    public partial class Registrierung : Window
    {
        private List<Login_Speicher> Login_Data = new List<Login_Speicher>();


        public Registrierung()
        {
            InitializeComponent();
        }

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
                MessageBox.Show("Benutzer ist bereits vorhanden. Bitte wählen Sie einen anderen Benutzername."); Controlling_Data.Textdateien.Clear(); Controlling_Data.abfrage = false; Benutzer.Clear(); 
 
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Benutzer ist nicht vorhanden.Neuen Benutzer anlegen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Question);

                switch (result)
                {
                    case MessageBoxResult.Yes:   Writer(); MessageBox.Show("Datensatz wurde Angelegt. Sie können sich jetzt Einloggen."); this.Close();
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Es wurde kein neuer Nutzer angelegt");
                        break;
                }

            }

        }
        

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Safe_Click(object sender, RoutedEventArgs e)
        {
            InitLogindata();
            Controlling_Data.ControllData();
            Datenabgleich();
        }
    }
}
