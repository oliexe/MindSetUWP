// TEST APLIKACE
using MindSetUWA;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MindWaveBc
{
    public sealed partial class MainPage : Page
    {
        //Create a new instance of MindSetConnection class.
        private MindSetConnection MyHeadset = new MindSetConnection();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Initiate bluetooth connection to a headset named "MindWave Mobile"
            MyHeadset.ConnectBluetooth("MindWave Mobile");
        }

        private void EEGToList()
        {
            EEGlist.Items.Clear();
            EEGlist.Items.Add("QUALITY: " + MyHeadset.RealtimeData.Quality);
            EEGlist.Items.Add("Alpha High: " + MyHeadset.RealtimeData.AlphaHigh);
            EEGlist.Items.Add("Alpha Low: " + MyHeadset.RealtimeData.AlphaLow);
            EEGlist.Items.Add("Beta High: " + MyHeadset.RealtimeData.BetaHigh);
            EEGlist.Items.Add("Beta Low: " + MyHeadset.RealtimeData.BetaLow);
            EEGlist.Items.Add("Delta: " + MyHeadset.RealtimeData.Delta);
            EEGlist.Items.Add("Theta: " + MyHeadset.RealtimeData.Theta);
            EEGlist.Items.Add("Gamma Low: " + MyHeadset.RealtimeData.GammaLow);
            EEGlist.Items.Add("Gama Mid: " + MyHeadset.RealtimeData.GammaMid);
            EEGlist.Items.Add("Meditation: " + MyHeadset.RealtimeData.Meditation);
            EEGlist.Items.Add("Attention: " + MyHeadset.RealtimeData.Attention);
            EEGlist.Items.Add("Time: " + MyHeadset.RealtimeData.Timestamp);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
         EEGToList();

        }
    }
}
