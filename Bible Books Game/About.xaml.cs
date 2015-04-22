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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bible_Books_Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class About : Page
    {
        public About()
        {
            this.InitializeComponent();

            infoText();
        }

        private void booksList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BooksHints));
        }
        private void infoText()
        {
            info.Text = "This is a Bible App Game based on the Old Testament Book."+ "\n" + 
            " This is a graet app teaches kids as well as adults to arrange all the Old Testament Books in order (from 1 - 39)." + "\n" +
            " A score is earned each time your guess is correct. The app also features a quiz game. You can also avail of the bible books list that you can use to see how the books are ordered.";
        }
    }
}
