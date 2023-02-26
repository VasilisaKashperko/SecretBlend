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
    /// <summary>
    /// Логика взаимодействия для EncryptChooseSecretKeyPage.xaml
    /// </summary>
    public partial class EncryptChooseSecretKeyPage : Page
    {
        public EncryptChooseSecretKeyPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptChooseWAVPage.xaml", UriKind.Relative);
        }

        private void PasswordView_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
