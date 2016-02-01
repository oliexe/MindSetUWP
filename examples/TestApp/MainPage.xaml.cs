// TEST APLIKACE
using MindSetUWA;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;

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
            MyHeadset.Connected += new MindSetConnection.StatusUpdateHandler(OnConnected);
            MyHeadset.NoHeadset += new MindSetConnection.StatusUpdateHandler(OnNoHeadset);
            MyHeadset.Connecting += new MindSetConnection.StatusUpdateHandler(OnConnecting);
            MyHeadset.QualityChange += new MindSetConnection.StatusUpdateHandler(QualityChange);
            MyHeadset.PacketRecieved += new MindSetConnection.StatusUpdateHandler(QualityChange);


            MyHeadset.ConnectBluetooth("MindWave Mobile");
        }

        private void QualityChange(object sender, ProgressEventArgs e)
        {
            EEGToList();
        }


        private void OnConnected(object sender, ProgressEventArgs e)
         {
            EEGlist.Items.Add("connected");
         }

        private void OnNoHeadset(object sender, ProgressEventArgs e)
        {
            EEGlist.Items.Add("Headset not found");
        }

        private void OnConnecting(object sender, ProgressEventArgs e)
        {
            EEGlist.Items.Add("Connecting..");
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

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            MyHeadset.StartRecording(1, true);
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            MyHeadset.StopRecording();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MindsetDataStruct[] MyRecordedData = MyHeadset.RecordingToArray();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            MyHeadset.ClearRecordingData();


            
        }
    }
}
