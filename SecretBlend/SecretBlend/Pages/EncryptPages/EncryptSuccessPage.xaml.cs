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
    public partial class EncryptSuccessPage : Page
    {
        public EncryptSuccessPage()
        {
            InitializeComponent();
        }

        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/MainWindowPages/MainPage.xaml", UriKind.Relative);
            GlobalClass.isEncrypt = false;
            GlobalClass.isRadioButtonChanged = false;
            GlobalClass.whatRadioButton = false;
            GlobalClass.WAVfile = "";
            GlobalClass.secretKey = "";
            GlobalClass.TXTFile = "";
            GlobalClass.TXTMessage = "";
            GlobalClass.EncryptedWAVFile = "";
        }
    }
}
