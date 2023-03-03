using System;
using System.Collections.Generic;
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

namespace SecretBlend
{
    public partial class EncryptChooseSecretKeyPage : Page
    {
        public EncryptChooseSecretKeyPage()
        {
            InitializeComponent();

            NextButton.IsEnabled = false;

            if (GlobalClass.secretKey.ToString() != string.Empty)
            {
                PasswordBox.Password = GlobalClass.secretKey;
                NextButton.IsEnabled = true;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalClass.secretKey = PasswordBox.Password.Trim().ToString();
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptChooseWAVPage.xaml", UriKind.Relative);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Length < 3)
            {
                NextButton.IsEnabled = false;
            }
            else
            {
                GlobalClass.secretKey = PasswordBox.Password.Trim().ToString();
                this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptAddMessagePage.xaml", UriKind.Relative);
            }
        }

        private void PasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(PasswordBox.Password.Length > 1)
            {
                GlobalClass.secretKey = PasswordBox.Password.Trim().ToString();

                if (GlobalClass.secretKey.ToString() != string.Empty)
                {
                    NextButton.IsEnabled = true;
                }
            }
        }

        private void Page_MouseMove(object sender, MouseEventArgs e)
        {
            if (PasswordBox.Password.Length < 3)
            {
                NextButton.IsEnabled = false;
            }
        }
    }
}
