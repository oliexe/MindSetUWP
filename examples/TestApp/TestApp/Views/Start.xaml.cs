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

        public Start()
        {
            this.InitializeComponent();

            var items = new List<NameValueItem>();

            for (int i = 0; i < 10; i++)
            {
                items.Add(new NameValueItem { Name = "Test" + i, Value = _random.Next(10, 100) });
            }

            RunIfSelected(this.AreaChart, () => ((AreaSeries)this.AreaChart.Series[0]).ItemsSource = items); ;

        }
    }
}
