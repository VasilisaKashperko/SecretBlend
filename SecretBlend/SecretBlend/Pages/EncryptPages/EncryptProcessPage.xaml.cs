using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
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

namespace SecretBlend
{
    public partial class EncryptProcessPage : Page
    {
        public EncryptProcessPage()
        {
            InitializeComponent();

            NextButton.IsEnabled = false;

            if (GlobalClass.WAVfile.Length > 50)
            {
                string briefPATH = "";

                foreach (char chars in GlobalClass.WAVfile)
                {
                    briefPATH += chars;
                }

                string cropedResult = briefPATH.Substring(0, 49) + "...";

                LabelNameWAV.Content = cropedResult;
            }
            else
            {
                LabelNameWAV.Content = GlobalClass.WAVfile;
            }

            PasswordBox.Password = GlobalClass.secretKey;
            PasswordBox.IsEnabled = false;

            if (!GlobalClass.whatRadioButton)
            {

                if (GlobalClass.TXTMessage.Length > 42)
                {
                    string briefPATH = "";

                    foreach (char chars in GlobalClass.TXTMessage)
                    {
                        briefPATH += chars;
                    }

                    string cropedResult = briefPATH.Substring(0, 41) + "...";

                    LabelMessage.Content = cropedResult;
                }
                else
                {
                    LabelMessage.Content = GlobalClass.TXTMessage;
                }
            }

            if (GlobalClass.whatRadioButton)
            {

                if (GlobalClass.TXTFile.Length > 45)
                {
                    string briefPATH = "";

                    foreach (char chars in GlobalClass.TXTFile)
                    {
                        briefPATH += chars;
                    }

                    string cropedResult = briefPATH.Substring(0, 44) + "...";

                    LabelMessage.Content = cropedResult;
                }
                else
                {
                    LabelMessage.Content = GlobalClass.TXTFile;
                }
            }

            EncryptProgressBar.Visibility = Visibility.Hidden;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptAddMessagePage.xaml", UriKind.Relative);
        }

        private void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EncryptProgressBar.Visibility = Visibility.Visible;

                if (!GlobalClass.whatRadioButton)
                {
                    EncryptProgressBar.IsIndeterminate = true;
                    Algorithm.Hide(GlobalClass.WAVfile, GlobalClass.secretKey, GlobalClass.TXTMessage);
                    EncryptProgressBar.IsIndeterminate = false;
                }

                if (GlobalClass.whatRadioButton)
                {
                    EncryptProgressBar.IsIndeterminate = true;

                    string txtFile = File.ReadAllText(GlobalClass.TXTFile, Encoding.UTF8);

                    Algorithm.Hide(GlobalClass.WAVfile, GlobalClass.secretKey, txtFile);
                    EncryptProgressBar.IsIndeterminate = false;
                }

                EncryptProgressBar.Value = 100;
                Thread.Sleep(100);
                ReadyLabel.Content = "Готово!";

                NextButton.IsEnabled = true;
                BackButton.IsEnabled = false;
            }
            catch
            {

            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptSuccessPage.xaml", UriKind.Relative);
        }
    }
}
