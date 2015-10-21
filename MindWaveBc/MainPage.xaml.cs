// Include the MindSetUWA Library.
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Debug.Write(MyHeadset.RealtimeData.AllToString());
        }
    }
}
