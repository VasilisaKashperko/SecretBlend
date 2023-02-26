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

            if(GlobalClass.secretKey.ToString() != string.Empty)
            {
                PasswordBox.Password = GlobalClass.secretKey;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalClass.secretKey = PasswordBox.Password.Trim().ToString();
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptChooseWAVPage.xaml", UriKind.Relative);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalClass.secretKey = PasswordBox.Password.Trim().ToString();
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptAddMessagePage.xaml", UriKind.Relative);
        }
    }
}
