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
    public sealed partial class MatureLevel : Page
    {
        public int userNumber = 1;
        public int bookCounter;
        int guessCount=10, newCount=2;
        public int scoreVal;

       
       DispatcherTimer guessTimer = new DispatcherTimer();
       
        DispatcherTimer newTimer = new DispatcherTimer();

        public Random rand = new Random();

        List<string> booksRandList = new List<string>();

        public List<string> booksList = new List<string>{
                                "01","02","03","04","05","06","07","08","09","10", 
                                "11","12","13","14","15","16","17","18","19","20",
                                "21","22","23","24","25","26","27","28","29","30",
                                "31","32","33","34","35","36","37","38","39"};
        public MatureLevel()
        {
            this.InitializeComponent();

            this.startGame();

            newTimer.Interval = TimeSpan.FromSeconds(1);
            newTimer.Tick += New_Tick;
           
        }
        private void startGame()
        {
            myRandomBooks();
            guessTimer.Interval = new TimeSpan(0, 0, 0, 1, 0); // 1 second
            guessTimer.Tick += Guess_Time;
            guessTimer.Start();
        }

        private void New_Tick(object sender, object e)
        {
            newCount--;
            TimerBlock2.Text = newCount.ToString();

            if (newCount == 2)
            {
                TimerBlock.Text = "";
               QuestionBlock.Text = "";
            }
            if (newCount == 0)
            {
                bookCounter++;
                myRandomBooks();
                guessTimer.Start();
                QuestionBlock2.Text = "";
                newTimer.Stop();
            }
            if (booksList.Count == 0)
            {
                QuestionBlock2.Text = "Game Over";
                QuestionBlock.Text = "";
                TimerBlock.Text = "";
                guessTimer.Stop();
            }
           

           
        }
        private void Guess_Time(object sender, object e)
        {
            guessCount--;
            TimerBlock.Text = guessCount.ToString();
            if (guessCount == 8)
            {
                QuestionBlock2.Text = "";              
            }
            if (guessCount == 0)
            {
                bookCounter++;
                QuestionBlock2.Text = "Slow Guess";
                ScoreBlock.Text = scoreVal.ToString();
                myRandomBooks();
                guessTimer.Start();
               
            }
            if(booksList.Count == 0)
            {
                guessTimer.Stop();
            }

        }

        private void myRandomBooks()
        {
            if (booksList.Count == 0)
            {
                QuestionBlock2.Text = "Game Over";
                QuestionBlock.Text = "";
                TimerBlock.Text = "";
                QuestionBlock2.Text = "Your Final Score is " + scoreVal;
                guessTimer.Stop();
            }
            else
            {
                guessCount = 10;
                int books;
                books = rand.Next(0, booksList.Count());
                booksRandList.Add(booksList[books]);
                booksList.RemoveAt(books);

                QuestionBlock.Text = booksRandList[bookCounter];
            }
        }
        private void Path_Tapped(object sender, TappedRoutedEventArgs e)
        {

            Canvas current = sender as Canvas;

            if (QuestionBlock.Text == current.Tag.ToString())
            {
                //have matched
                scoreVal++;
                bookCounter++;

                if (booksList.Count == 0)
                {
                    QuestionBlock2.Text = "Your Final Score is " + scoreVal;
                    QuestionBlock.Text = "";
                    TimerBlock.Text = "";
                    QuestionBlock_Copy.Text = "Game Over";
                    guessCount=0;
                    guessTimer.Stop();


                    GlobalVar2.newHighScore = scoreVal;
                    if (GlobalVar2.newHighScore > GlobalVar2.HighScoreOne)
                    {
                        GlobalVar2.HighScoreFive = GlobalVar2.HighScoreFour;
                        GlobalVar2.HighScoreNameFive = GlobalVar2.HighScoreNameFour;
                        GlobalVar2.HighScoreFour = GlobalVar2.HighScoreThree;
                        GlobalVar2.HighScoreNameFour = GlobalVar2.HighScoreNameThree;
                        GlobalVar2.HighScoreThree = GlobalVar2.HighScoreTwo;
                        GlobalVar2.HighScoreNameThree = GlobalVar2.HighScoreNameTwo;
                        GlobalVar2.HighScoreTwo = GlobalVar2.HighScoreOne;
                        GlobalVar2.HighScoreNameTwo = GlobalVar2.HighScoreNameOne;
                        GlobalVar2.HighScoreOne = GlobalVar2.newHighScore;
                        GlobalVar2.HighScoreNameOne = GlobalVarMature.Username;
                    }
                    else if (GlobalVar2.newHighScore > GlobalVar2.HighScoreTwo)
                    {
                        GlobalVar2.HighScoreFive = GlobalVar2.HighScoreFour;
                        GlobalVar2.HighScoreNameFive = GlobalVar2.HighScoreNameFour;
                        GlobalVar2.HighScoreFour = GlobalVar2.HighScoreThree;
                        GlobalVar2.HighScoreNameFour = GlobalVar2.HighScoreNameThree;
                        GlobalVar2.HighScoreThree = GlobalVar2.HighScoreTwo;
                        GlobalVar2.HighScoreNameThree = GlobalVar2.HighScoreNameTwo;
                        GlobalVar2.HighScoreTwo = GlobalVar2.newHighScore;
                        GlobalVar2.HighScoreNameTwo = GlobalVarMature.Username;
                    }
                    else if (GlobalVar2.newHighScore > GlobalVar2.HighScoreThree)
                    {
                        GlobalVar2.HighScoreFive = GlobalVar2.HighScoreFour;
                        GlobalVar2.HighScoreNameFive = GlobalVar2.HighScoreNameFour;
                        GlobalVar2.HighScoreFour = GlobalVar2.HighScoreThree;
                        GlobalVar2.HighScoreNameFour = GlobalVar2.HighScoreNameThree;
                        GlobalVar2.HighScoreThree = GlobalVar2.newHighScore;
                        GlobalVar2.HighScoreNameThree = GlobalVarMature.Username;
                    }
                    else if (GlobalVar2.newHighScore > GlobalVar2.HighScoreFour)
                    {
                        GlobalVar2.HighScoreFive = GlobalVar2.HighScoreFour;
                        GlobalVar2.HighScoreNameFive = GlobalVar2.HighScoreNameFour;
                        GlobalVar2.HighScoreFour = GlobalVar2.newHighScore;
                        GlobalVar2.HighScoreNameFour = GlobalVarMature.Username;
                    }
                    else if (GlobalVar2.newHighScore > GlobalVar2.HighScoreFive)
                    {
                        GlobalVar2.HighScoreFive = GlobalVar2.newHighScore;
                        GlobalVar2.HighScoreNameFive = GlobalVarMature.Username;
                    }

                }
                else
                {
                    QuestionBlock2.Text = "Correct";
                    ScoreBlock.Text = scoreVal.ToString();
                    current.Opacity = 1;
                    myRandomBooks();
                }

            }
            else
            {

                QuestionBlock2.Text = "Wrong Guess";
                guessCount = 10;
                guessTimer.Stop();
                QuestionBlock.Text = "";
                TimerBlock.Text = "";
                ScoreBlock.Text = scoreVal.ToString();
                newCount =3;
                newTimer.Start();
            }
            if(newCount ==0)
            {
                bookCounter++;
                myRandomBooks();
                guessTimer.Start();
                QuestionBlock2.Text = "";
                newTimer.Stop();
            }
            
            //if(booksList.Count == 0)
            //{
            //    QuestionBlock2.Text = "Your Final Score is " + scoreVal;
            //    QuestionBlock.Text = "";
            //    QuestionBlock_Copy.Text = "Game Over";
            //    TimerBlock.Text = "";
            //    guessTimer.Stop();

            //    ScoreBlock.Text = scoreVal.ToString();

               
            //}
             
           
        }
  
        private void checkScore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ScoreMature));
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.SizeChanged += Current_SizeChanged;

        } 

       
       
    }
}
