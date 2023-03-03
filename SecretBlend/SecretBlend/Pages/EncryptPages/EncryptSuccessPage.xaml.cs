using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
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
        public SoundPlayer wavBeforePlayer = new SoundPlayer();
        public SoundPlayer wavAfterPlayer = new SoundPlayer();

        public EncryptSuccessPage()
        {
            InitializeComponent();

            FileInfo wavBefore = new FileInfo(GlobalClass.WAVfile);
            BeforeLabel.Content = wavBefore.Name;
            SizeBeforeLabel.Content = wavBefore.Length + " байт";

            FileInfo wavAfter = new FileInfo(GlobalClass.EncryptedWAVFile);
            AfterLable.Content = wavAfter.Name;
            SizeAfterLabel.Content = wavAfter.Length + " байт";
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

        private void PlayBeforeButton_Click(object sender, RoutedEventArgs e)
        {
            wavBeforePlayer.SoundLocation = GlobalClass.WAVfile;
            wavBeforePlayer.LoadAsync();
            wavBeforePlayer.Play();
        }

        private void PlayAfterButton_Click(object sender, RoutedEventArgs e)
        {
            wavAfterPlayer.SoundLocation = GlobalClass.EncryptedWAVFile;
            wavBeforePlayer.LoadAsync();
            wavAfterPlayer.Play();
        }

        private void PauseBeforeButton_Click(object sender, RoutedEventArgs e)
        {
            wavBeforePlayer.Stop();
        }

        private void PauseAfterButton_Click(object sender, RoutedEventArgs e)
        {
            wavAfterPlayer.Stop();
        }
    }
}
