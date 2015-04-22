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
    public sealed partial class BeginnersGame : Page
    {
        public static bool _timerOn = true;
        public int scoreVal = 0, booksNum, negativeScore=0, totalScore=0;
        public int bookCounter = 0, guessCount=3;
        public List<string> booksList = new List<string>{
                                "Genesis","Exodus","Leviticus","Numbers","Deuteronomy","Joshua","Judges","Ruth","_1Samuel","_2Samuel", 
                                "_1Kings","_2Kings","_1Chronicles","_2Chronicles","Ezra","Nehemiah","Esther","Job","Psalms","Proverbs",
                                "Ecclesiastes","SongsSolomon","Isaiah","Jeremiah","Lamentations","Ezekiel","Daniel","Hosea","Joel","Amos",
                                "Obadiah","Jonah","Micah","Nahum","Habakkuk","Zephaniah","Haggai","Zechariah","Malachi"};

        public List<Canvas> canvasList = new List<Canvas>();

        DispatcherTimer guessTimer = new DispatcherTimer();
        DispatcherTimer newTimer = new DispatcherTimer();
        

        List<string> booksRandList = new List<string>();
        public int userNumber = 1;



        public BeginnersGame()
        {
            this.InitializeComponent();

            //DispatcherTimer newTimer = new DispatcherTimer();
            // timer interval specified as 1 second
            newTimer.Interval = TimeSpan.FromSeconds(1);
            // Sub-routine OnTimerTick will be called at every 1 second
            newTimer.Tick += Guess_Tick;
            // starting the timer
           // newTimer.Start();

            
        }

        private void OnTimerTick(object sender, object e)
        {
            //throw new NotImplementedException();
            // text box property is set to current system date.
            // ToString() converts the datetime value into text
            //correctAnswerBlock.Text = DateTime.Now.ToString();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.SizeChanged += Current_SizeChanged;

            Canvas tmp = BibleBooks;
            int index = tmp.Children.Count;
            booksNum = index;

            QuestionBlock.Text = "Select Book: " + userNumber.ToString();
            txtBookNum.Text = "/ "+ booksNum.ToString();

         }//onNavigatedTo

        public int guessed = 0;
        public bool isRight=true;

        private void Path_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Canvas tmp = BibleBooks;
            int index = tmp.Children.Count;
            int score = 0;

            Canvas current = sender as Canvas;
            //ask user to click the n book    

            if (current.Tag.ToString() == booksList[userNumber - 1].ToString())
            {
                //have matched
                     userNumber++;
                     if (!isRight)
                     {
                         score=score+0;
                         isRight = true;
                     }
                     else
                     {
                         score = score + 1;
                         current.Opacity = 1;
                         newTimer.Stop();
                     }
                    guessed = 0;
                

                if (userNumber < 40)
                {
                    QuestionBlock.Text = "Select Book: " + userNumber.ToString();
                }
                else
                {
                    QuestionBlock.Text = "Game Over! ";
                    userNumber = 1;

                    if (scoreVal == 39)
                    {
                        QuestionBlock.Text = "Congrats... You Won!! ";
                    }
                    
                }
               
            }
            else
            {

                correctAnswerBlock.Text = "Wrong!!  Correct is : "  + booksList[userNumber - 1].ToString();
                score = score + 0;
                isRight = false;
                newTimer.Start();

                if (!isRight)
                {
                    score = score + 0;
                    isRight = true;

                    userNumber++;
                    if (userNumber < 40)
                    {
                        QuestionBlock.Text = "Select Book: " + userNumber.ToString();
                    }
                   
                }
                guessCount = 3;

              }

            if (guessCount == 0)
            {
                correctAnswerBlock.Text = "";
                newTimer.Stop();

            }

            totalScore = scoreVal - negativeScore;
            scoreVal += score;
            if (isRight)
            {
                ScoreBlock.Text = scoreVal.ToString();
            }
            else
            {

            }


            if (scoreVal == 39)
            {
                QuestionBlock.Text = "Congrats... You Won!! ";
            }


            GlobalVar1.newHighScore = scoreVal;

            if (GlobalVar1.newHighScore > GlobalVar1.HighScoreOne)
            {
                GlobalVar1.HighScoreFive = GlobalVar1.HighScoreFour;
                GlobalVar1.HighScoreNameFive = GlobalVar1.HighScoreNameFour;
                GlobalVar1.HighScoreFour = GlobalVar1.HighScoreThree;
                GlobalVar1.HighScoreNameFour = GlobalVar1.HighScoreNameThree;
                GlobalVar1.HighScoreThree = GlobalVar1.HighScoreTwo;
                GlobalVar1.HighScoreNameThree = GlobalVar1.HighScoreNameTwo;
                GlobalVar1.HighScoreTwo = GlobalVar1.HighScoreOne;
                GlobalVar1.HighScoreNameTwo = GlobalVar1.HighScoreNameOne;
                GlobalVar1.HighScoreOne = GlobalVar1.newHighScore;
                GlobalVar1.HighScoreNameOne = GlobalVarBeginner.Username;
            }
            else if (GlobalVar1.newHighScore > GlobalVar1.HighScoreTwo)
            {
                GlobalVar1.HighScoreFive = GlobalVar1.HighScoreFour;
                GlobalVar1.HighScoreNameFive = GlobalVar1.HighScoreNameFour;
                GlobalVar1.HighScoreFour = GlobalVar1.HighScoreThree;
                GlobalVar1.HighScoreNameFour = GlobalVar1.HighScoreNameThree;
                GlobalVar1.HighScoreThree = GlobalVar1.HighScoreTwo;
                GlobalVar1.HighScoreNameThree = GlobalVar1.HighScoreNameTwo;
                GlobalVar1.HighScoreTwo = GlobalVar1.newHighScore;
                GlobalVar1.HighScoreNameTwo = GlobalVarBeginner.Username;
            }
            else if (GlobalVar1.newHighScore > GlobalVar1.HighScoreThree)
            {
                GlobalVar1.HighScoreFive = GlobalVar1.HighScoreFour;
                GlobalVar1.HighScoreNameFive = GlobalVar1.HighScoreNameFour;
                GlobalVar1.HighScoreFour = GlobalVar1.HighScoreThree;
                GlobalVar1.HighScoreNameFour = GlobalVar1.HighScoreNameThree;
                GlobalVar1.HighScoreThree = GlobalVar1.newHighScore;
                GlobalVar1.HighScoreNameThree = GlobalVarBeginner.Username;
            }
            else if (GlobalVar1.newHighScore > GlobalVar1.HighScoreFour)
            {
                GlobalVar1.HighScoreFive = GlobalVar1.HighScoreFour;
                GlobalVar1.HighScoreNameFive = GlobalVar1.HighScoreNameFour;
                GlobalVar1.HighScoreFour = GlobalVar1.newHighScore;
                GlobalVar1.HighScoreNameFour = GlobalVarBeginner.Username;
            }
            else if (GlobalVar1.newHighScore > GlobalVar1.HighScoreFive)
            {
                GlobalVar1.HighScoreFive = GlobalVar1.newHighScore;
                GlobalVar1.HighScoreNameFive = GlobalVarBeginner.Username;
            }
                
        }
        private void Guess_Tick(object sender, object e)
        {
            guessCount--;
            TimerBlock.Text = guessCount.ToString();
            if (guessCount == 3)
            {

               
            }
            if (guessCount == 0)
            {
               bookCounter++;
                correctAnswerBlock.Text = "";
                newTimer.Stop();
            }

        }

        private void checkScore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ScoreBeginner));
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
