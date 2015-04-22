using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
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
    public sealed partial class SoreQuiz : Page
    {
        public SoreQuiz()
        {
            this.InitializeComponent();       
        }
       
        public void CheckHighScores()
        {
            nameScoreOne.Text = GlobalVar3.HighScoreNameOne;
            highscoreBlockOne.Text = GlobalVar3.HighScoreOne.ToString();

            nameScoreTwo.Text = GlobalVar3.HighScoreNameTwo;
            highscoreBlockTwo.Text = GlobalVar3.HighScoreTwo.ToString();

            nameScoreThree.Text = GlobalVar3.HighScoreNameThree;
            highscoreBlockThree.Text = GlobalVar3.HighScoreThree.ToString();

            nameScoreFour.Text = GlobalVar3.HighScoreNameFour;
            highscoreBlockFour.Text = GlobalVar3.HighScoreFour.ToString();

            nameScoreFive.Text = GlobalVar3.HighScoreNameFive;
            highscoreBlockFive.Text = GlobalVar3.HighScoreFive.ToString();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CheckHighScores();
            Window.Current.SizeChanged += Current_SizeChanged;
        }
        
        private void back_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            ViewHandler();
        }

        private void ViewHandler()
        {
            ApplicationView current = ApplicationView.GetForCurrentView();
            if (current.IsFullScreen)
            {
                Snap.Visibility = Visibility.Collapsed;

            }

            else
            {
                Snap.Visibility = Visibility.Visible;

            }
        }

       

    }
}
