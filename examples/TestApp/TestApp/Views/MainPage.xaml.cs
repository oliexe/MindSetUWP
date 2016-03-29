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
using TestApp.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame current = NavStrip.Content as Frame;
            current.Navigate(((HamburgerItem)NavMenu.SelectedItem).Page);
            NavStrip.IsPaneOpen = false;
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            NavStrip.IsPaneOpen = !NavStrip.IsPaneOpen;
        }

        private void NavMenu_Loaded(object sender, RoutedEventArgs e)
        {
            NavMenu.SelectedIndex = 0;
            Frame current = NavStrip.Content as Frame;
            current.Navigate(((HamburgerItem)NavMenu.SelectedItem).Page);
        }
    }
}
