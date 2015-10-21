using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Windows.ApplicationModel.Background;
using System.ComponentModel;

// Include the MindSetUWA Library.
using MindSetUWA;

namespace MindWaveBc
{
    public sealed partial class MainPage : Page
    {
        //Create a new instance of MindSetConnection class.
        MindSetConnection MyHeadset = new MindSetConnection();

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
