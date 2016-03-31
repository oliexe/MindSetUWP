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
using MindSetUWA;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageNotFound : Page
    {

        public MindSetConnection connection = new MindSetConnection();


        private void RunIfSelected(UIElement element, Action action)
        {
            action.Invoke();
        }

        public PageNotFound()
        {
            this.InitializeComponent();
            connection.ConnectBluetooth("MinWave Mobile");
            connection.StartRecording(1,true);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            connection.StopRecording();


        }
    }
}
