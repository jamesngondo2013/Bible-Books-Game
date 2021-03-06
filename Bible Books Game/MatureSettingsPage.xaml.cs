﻿using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bible_Books_Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MatureSettingsPage : Page
    {
        public MatureSettingsPage()
        {
            this.InitializeComponent();

            if (GlobalVarMature.Username != null)
            {
                nameBox.Text = GlobalVarMature.Username.ToString();
            }
           
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            GlobalVarMature.Username = nameBox.Text;
        }
       

        private void save_Tapped(object sender, TappedRoutedEventArgs e)
        {
            GlobalVarMature.Username = nameBox.Text;
            this.Frame.Navigate(typeof(MatureLevel));
        }
    }
}
