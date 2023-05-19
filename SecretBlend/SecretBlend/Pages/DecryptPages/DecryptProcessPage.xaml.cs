using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class DecryptProcessPage : Page
    {
        public DecryptProcessPage()
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

            EncryptProgressBar.Visibility = Visibility.Hidden;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/DecryptPages/DecryptWayToExtractPage.xaml", UriKind.Relative);
        }

        private void ChooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EncryptProgressBar.Visibility = Visibility.Visible;

                if (!GlobalClass.whatRadioButton)
                {
                    EncryptProgressBar.IsIndeterminate = true;
                    Algorithm.Extract(GlobalClass.WAVfile, GlobalClass.secretKey);
                    EncryptProgressBar.IsIndeterminate = false;
                }

                if (GlobalClass.whatRadioButton)
                {
                    EncryptProgressBar.IsIndeterminate = true;
                    Algorithm.Extract(GlobalClass.WAVfile, GlobalClass.secretKey);
                    EncryptProgressBar.IsIndeterminate = false;
                }

                EncryptProgressBar.Value = 100;
                Thread.Sleep(100);
                ReadyLabel.Content = "Готово!";

                NextButton.IsEnabled = true;
                BackButton.IsEnabled = false;
            }
            catch (Exception){
                Console.WriteLine("fld");
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/DecryptPages/DecryptSuccessPage.xaml", UriKind.Relative);
        }
    }
}
