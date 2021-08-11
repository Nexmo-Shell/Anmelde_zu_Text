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
using System.Linq;
using System.IO;


namespace Anmelde_zu_Text
{
    /// <summary>
    /// Interaktionslogik für PasswortReset.xaml
    /// </summary>
    public partial class PasswortReset : Window
    {
        string Password { set; get; }
        string Benutzer { set; get; }
        int Übergabezahl;
        public PasswortReset()
        {
            InitializeComponent();
        }

        private void Bestätigung(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Sind Sie sicher, dass Sie das Passwort für den Benuter " + Benutzername.Text + " zurücksetzen möchten?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            switch(result)
            {
                case MessageBoxResult.Yes: MessageBox.Show("Ein neues Passwort wird angelegt."); Bestätigen.Visibility = Visibility.Hidden; Benutzerabfrage.Visibility = Visibility.Hidden; Ok.Visibility = Visibility.Visible; Passwortabfrage.Visibility = Visibility.Visible; Benutzername.Visibility = Visibility.Hidden; PasswortNeu.Visibility = Visibility.Visible;
                    break;
                case MessageBoxResult.No: MessageBox.Show("Es wird kein neues Passwort angelegt"); this.Close();
                    break;
            }
        }

        private void DeletePasswort()
        {
            string sourceDir = @"D:\Umschulung_Fachinformatiker\Praktikum\Textdata\Keys";
            Benutzer = Benutzername.Text;
            string[] txtList = Directory.GetFiles(sourceDir, "*.txt");
            Controlling_Data.ControllData();
            int i = 0;
            do
            {
                foreach (var s in Controlling_Data.passwort_reset)
                {
                    string[] subs = s.Split('_');
                    string B_Name = subs[0];
                    string Pass_data = subs[1];
                    string check_data = @"D:\Umschulung_Fachinformatiker\Praktikum\Textdata\Keys\" + Pass_data + ".txt";

                    if (B_Name == Benutzer)
                    {
                        foreach (string fi in txtList)
                        {
                            if (check_data == fi)
                            { File.Delete(fi);
                                Übergabezahl = int.Parse(Pass_data);
                            }
                        }
                    }
                    i++;
                }
            } while (i != Controlling_Data.pass_Data.Count);
        }
        
        private string SetnewPasswort()
        {
            string Passwort = PasswortNeu.Text;

            CrypToFile.ResetEncrypt(Übergabezahl, Passwort);
            Password = CrypToFile.Decrypt(Übergabezahl);

            return Password;
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            DeletePasswort();
            string anzeige = SetnewPasswort();

            MessageBox.Show("Ihr Passwort wurde erfolgreich geändert. Das neue Passwort lautet " + anzeige);

            this.Close();
        }
    }
}
