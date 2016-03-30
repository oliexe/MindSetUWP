using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Tools;
using MindSetUWA;
using System.Threading;
using Windows.System.Threading;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Start : Page
    {
        private Random _random = new Random();

        public class NameValueItem
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        private void RunIfSelected(UIElement element, Action action)
        {
                action.Invoke();
        }

        MindSetConnection connection = new MindSetConnection();

        public int enlapsedTime;
        private DispatcherTimer dispatch;

        public delegate void MyCallback();
        public delegate void MyCallback2(int value);
        public MyCallback OnStartTime;
        public MyCallback OnStopTime;
        public MyCallback OnEndTime;
        public MyCallback2 OnCountTime;
        public Boolean started = false;
        private double _value;

        public Start()
        {
            this.InitializeComponent();
            enlapsedTime = 0;
            dispatch = new DispatcherTimer();
            dispatch.Interval = new TimeSpan(0, 0, 0, 0, 200) ;
            dispatch.Tick += timer_Tick;
            dispatch.Start();

        }

       

        private void timer_Tick(object sender, object e)
        {

            if (!started)
                  {
                connection.ConnectBluetooth("MindWave Mobile");
                started = true;
            }

            var items = new List<NameValueItem>();
            items.Clear();
            items.Add(new NameValueItem { Name = "Delta", Value = connection.RealtimeData.Delta });
            items.Add(new NameValueItem { Name = "Theta", Value = connection.RealtimeData.Theta });
            items.Add(new NameValueItem { Name = "Alpha(Low)", Value = connection.RealtimeData.AlphaLow });
            items.Add(new NameValueItem { Name = "Alpha(High)", Value = connection.RealtimeData.AlphaHigh });
            items.Add(new NameValueItem { Name = "Theta", Value = connection.RealtimeData.Theta });
            items.Add(new NameValueItem { Name = "Beta(High)", Value = connection.RealtimeData.BetaHigh });
            items.Add(new NameValueItem { Name = "Beta(Low)", Value = connection.RealtimeData.BetaLow });
            items.Add(new NameValueItem { Name = "Gamma(Low)", Value = connection.RealtimeData.GammaLow });
            items.Add(new NameValueItem { Name = "Gamma(Mid)", Value = connection.RealtimeData.GammaMid });
            this.SignalQual.Value = connection.RealtimeData.Quality;
            this.AttenGauge.Value = connection.RealtimeData.Attention;
            this.MeditGauge.Value = connection.RealtimeData.Meditation;
            RunIfSelected(this.AreaChart, () => ((AreaSeries)this.AreaChart.Series[0]).ItemsSource = items); ;
        }


    }
}
