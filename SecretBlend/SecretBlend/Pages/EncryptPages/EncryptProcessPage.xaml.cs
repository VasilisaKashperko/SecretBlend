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
    public partial class EncryptProcessPage : Page
    {
        public EncryptProcessPage()
        {
            InitializeComponent();

            if (GlobalClass.WAVfile.Length > 45)
            {
                string briefPATH = "";

                foreach (char chars in GlobalClass.WAVfile)
                {
                    briefPATH += chars;
                }

                string cropedResult = briefPATH.Substring(0, 44) + "...";

                LabelNameWAV.Content = cropedResult;
            }
            else
            {
                LabelNameWAV.Content = GlobalClass.WAVfile;
            }

            LabelSecretKey.Content = GlobalClass.secretKey;

            if (GlobalClass.TXTMessage.Length > 45)
            {
                string briefPATH = "";

                foreach (char chars in GlobalClass.TXTMessage)
                {
                    briefPATH += chars;
                }

                string cropedResult = briefPATH.Substring(0,44) + "...";

                LabelMessage.Content = cropedResult;
            }
            else
            {
                LabelMessage.Content = GlobalClass.TXTMessage;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = new Uri("/Pages/EncryptPages/EncryptAddMessagePage.xaml", UriKind.Relative);
        }
    }
}
