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
    public sealed partial class QuizGame : Page
    {
        private String lastText = "";
        private Boolean isTextFinished = false;
        private Boolean hasStarted_Text = false;
        private Boolean gameStart = false;
        private int corrected_Text = 0;
        private int totalTimeElapsed_Text = 0;
        private int seconds_Text = 0;
        private int totalQuestions_Text = 0;
        private int timeLeft;
        private  int guessCount =3;

        private List<Question> questions_Text = new List<Question>();
        private Question currentQuestion = new Question();

        DispatcherTimer timer_Text = new DispatcherTimer();
        DispatcherTimer newTimer = new DispatcherTimer();

        public QuizGame()
        {
            this.InitializeComponent();
            Start();

            newTimer.Interval = TimeSpan.FromSeconds(1);
            // Sub-routine OnTimerTick will be called at every 1 second
            newTimer.Tick += TxtChange_Tick;
        }

        private void TxtChange_Tick(object sender, object e)
        {
            guessCount--;
            //txTimerQuestions.Text = guessCount.ToString();
           
            if (guessCount == 0)
            {
                AnswersCopy.Text = "";
                newTimer.Stop();
                Answers.Visibility = Visibility.Visible;
            }

        }

        void messagePromptQuestions_Completed(object sender, EventArgs e)
        {
            Questions_Completed();
        }

        private void Questions_Completed()
        {
            if (questions_Text.Count > 0)
            {
                seconds_Text = 0;
                questions_Text.RemoveAt(0);
                if (questions_Text.Count > 0)
                {
                    this.nextQuestion_Text();
                    timer_Text.Start();

                }
                else
                {
                    this.stopQuestions();
                }
            }
            else
            {
                this.stopQuestions();
            }
        }
        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

        private void pauseQusetions()
        {
            lastText = currentQuestion.question;
            timer_Text.Stop();
           
        }

        private void resumeTimer()
        {
            timer_Text.Start();
           
        }
        void showGameFinished_Completed(object sender, EventArgs e)
        {
            
            this.restart();
        }


        private void btnAnswerA_Click(object sender, RoutedEventArgs e)
        {
            this.compareQuestions(btnAnswerA.Content.ToString());
        }

        private void btnAnswerB_Click(object sender, RoutedEventArgs e)
        {
            this.compareQuestions(btnAnswerB.Content.ToString());
        }

        private void btnAnswerC_Click(object sender, RoutedEventArgs e)
        {
            this.compareQuestions(btnAnswerC.Content.ToString());
        }

        private void btnAnswerD_Click(object sender, RoutedEventArgs e)
        {
            this.compareQuestions(btnAnswerD.Content.ToString());
        }

        private void nextQuestion_Text()
        {
            questions_Text = ShuffleList<Question>(questions_Text);
            currentQuestion = questions_Text.ElementAt<Question>(0);
            txtQuestionSongs.Text = currentQuestion.text;
            txtTimerQuestions.Text = "Time Left: " + currentQuestion.timer;
           
            btnAnswerA.Content = currentQuestion.a;
            btnAnswerB.Content = currentQuestion.b;
            btnAnswerC.Content = currentQuestion.c;
            btnAnswerD.Content = currentQuestion.d;
        }

        private void compareQuestions(String answer)
        {
            timer_Text.Stop();

            if (answer.Equals(currentQuestion.answer))
            {   
                corrected_Text++;
                txtCorrectQuestions.Text = "" + corrected_Text + "/" + totalQuestions_Text;

                Answers.Text = "Correct";
                Questions_Completed();

            }
            else
            {

              // Answers.Text = "Wrong Answer";
                Answers.Visibility = Visibility.Collapsed;
                AnswersCopy.Text = "Correct Answer is: " + currentQuestion.answer;
                Questions_Completed();

                newTimer.Start();
                guessCount = 3;
                if (guessCount == 0)
                {
                    AnswersCopy.Text = "";
                    newTimer.Stop();
                    Answers.Visibility = Visibility.Visible;
                }

            }

            //if (questions_Text.Count <= 0)
            //{
            //    startGame.IsTapEnabled = false;
            //}
        }

        void timer_Tick_Songs(object sender, object e)
        {
            seconds_Text++;
            timeLeft = currentQuestion.timer - seconds_Text;
            if (timeLeft > 0)
            {
                txtTimerQuestions.Text = "Time Left: " + timeLeft;
            }
            else
            {
                if (questions_Text.Count > 0)
                {
                    seconds_Text = 0;
                    questions_Text.RemoveAt(0);
                    if (questions_Text.Count > 0)
                    {
                        this.nextQuestion_Text();
                    }
                    else
                    {
                        this.stopQuestions();
                    }
                }
                else
                {
                    this.stopQuestions();
                }
            }
        }

        private void stopQuestions()
        {
            seconds_Text = 0;
            timer_Text.Stop();

            isTextFinished = true;

            btnAnswerA.IsEnabled = false;
            btnAnswerB.IsEnabled = false;
            btnAnswerC.IsEnabled = false;
            btnAnswerD.IsEnabled = false;
            txtQuestionSongs.Text = "";

            if (this.isEverythingFinished())
            {
                this.showGameFinished();
            }
        }
        // bool isGameFinished = false;
        private void showGameFinished()
        {
            int totalScore = corrected_Text;
            int totalQuestions = totalQuestions_Text;
            int totalSecondsElapsed = totalTimeElapsed_Text;
            txtTimerQuestions.Text = "";
           
            Answers.Text = "Game Finished with a total score of:";
           // Answers.Text = "Game Finished with a total score of: " + totalScore + "/" + totalQuestions;
            ScoreBlock.Text = totalScore.ToString();

            GlobalVar3.newHighScore = totalScore;
            if (GlobalVar3.newHighScore > GlobalVar3.HighScoreOne)
            {
                GlobalVar3.HighScoreFive = GlobalVar3.HighScoreFour;
                GlobalVar3.HighScoreNameFive = GlobalVar3.HighScoreNameFour;
                GlobalVar3.HighScoreFour = GlobalVar3.HighScoreThree;
                GlobalVar3.HighScoreNameFour = GlobalVar3.HighScoreNameThree;
                GlobalVar3.HighScoreThree = GlobalVar3.HighScoreTwo;
                GlobalVar3.HighScoreNameThree = GlobalVar3.HighScoreNameTwo;
                GlobalVar3.HighScoreTwo = GlobalVar3.HighScoreOne;
                GlobalVar3.HighScoreNameTwo = GlobalVar3.HighScoreNameOne;
                GlobalVar3.HighScoreOne = GlobalVar3.newHighScore;
                GlobalVar3.HighScoreNameOne = GlobalVars.Username;
            }
            else if (GlobalVar3.newHighScore > GlobalVar3.HighScoreTwo)
            {
                GlobalVar3.HighScoreFive = GlobalVar3.HighScoreFour;
                GlobalVar3.HighScoreNameFive = GlobalVar3.HighScoreNameFour;
                GlobalVar3.HighScoreFour = GlobalVar3.HighScoreThree;
                GlobalVar3.HighScoreNameFour = GlobalVar3.HighScoreNameThree;
                GlobalVar3.HighScoreThree = GlobalVar3.HighScoreTwo;
                GlobalVar3.HighScoreNameThree = GlobalVar3.HighScoreNameTwo;
                GlobalVar3.HighScoreTwo = GlobalVar3.newHighScore;
                GlobalVar3.HighScoreNameTwo = GlobalVars.Username;
            }
            else if (GlobalVar3.newHighScore > GlobalVar3.HighScoreThree)
            {
                GlobalVar3.HighScoreFive = GlobalVar3.HighScoreFour;
                GlobalVar3.HighScoreNameFive = GlobalVar3.HighScoreNameFour;
                GlobalVar3.HighScoreFour = GlobalVar3.HighScoreThree;
                GlobalVar3.HighScoreNameFour = GlobalVar3.HighScoreNameThree;
                GlobalVar3.HighScoreThree = GlobalVar3.newHighScore;
                GlobalVar3.HighScoreNameThree = GlobalVars.Username;
            }
            else if (GlobalVar3.newHighScore > GlobalVar3.HighScoreFour)
            {
                GlobalVar3.HighScoreFive = GlobalVar3.HighScoreFour;
                GlobalVar3.HighScoreNameFive = GlobalVar3.HighScoreNameFour;
                GlobalVar3.HighScoreFour = GlobalVar3.newHighScore;
                GlobalVar3.HighScoreNameFour = GlobalVars.Username;
            }
            else if (GlobalVar3.newHighScore > GlobalVar3.HighScoreFive)
            {
                GlobalVar3.HighScoreFive = GlobalVar3.newHighScore;
                GlobalVar3.HighScoreNameFive = GlobalVars.Username;
            }

        }

        private void restart()
        {
            lastText = "";
            isTextFinished = false;
            hasStarted_Text = false;
            corrected_Text = 0;
            totalTimeElapsed_Text = 0;
            seconds_Text = 0;
            totalQuestions_Text = 0;
            txtCorrectQuestions.Text = "";
            txtTimerQuestions.Text = "";
            btnAnswerA.IsEnabled = true;
            btnAnswerB.IsEnabled = true;
            btnAnswerC.IsEnabled = true;
            btnAnswerD.IsEnabled = true;
            timer_Text.Stop();
            //timer_Text.Start();
            questions_Text = new List<Question>();
            currentQuestion = new Question();

            this.initialize_Questions();

        }

        private Boolean isEverythingFinished()
        {
            if (isTextFinished)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //int totalQuestions = 0;
        private void startQuestions()
        {
            // totalQuestions++;
            txtTimerQuestions.Text = "Time Left: " + currentQuestion.timer;
            timer_Text.Start();
            this.nextQuestion_Text();
            hasStarted_Text = true;

        }

        private void initialize_Questions()
        {
            //Q1
            Question question = null;
            question = new Question();
            question.text = "Which one is the sixth book of the Old Testament";
            question.a = "Joshua";
            question.b = "Romans";
            question.c = "Judges";
            question.d = "Smile on me";
            question.answer = "Joshua";
            question.timer = 10;
            questions_Text.Add(question);

            //Q2
            question = null;
            question = new Question();
            question.text = "King Saul consulted a witch to communicate with which Old Testament prophet?";
            question.a = "Isaiah";
            question.b = "Jeremiah";
            question.d = "Samuel";
            question.c = "Nathan";
            question.answer = "Samuel";
            question.timer = 10;
            questions_Text.Add(question);
        
            //Q3
            question = null;
            question = new Question();
            question.text = "What did Jonathan eat not knowing that his father had forbidden his soldiers to eat the rest of the day?";
            question.d = "Meat";
            question.a = "Fruits";
            question.b = "Honey";
            question.c = "Pork";
            question.answer = "Honey";
            question.timer = 10;
            questions_Text.Add(question);

            //Q4
            question = null;
            question = new Question();
            question.text = "Who turned into a pillar of salt in the book of Genesis?";
            question.a = "Isaac's wife";
            question.d = "Jacob's wife";
            question.b = "Abraham's wife";
            question.c = "Lot's Wife";
            question.answer = "Lot's Wife";
            question.timer = 10;
            questions_Text.Add(question);

            //Q5
            question = null;
            question = new Question();
            question.text = "What does Beth-El (or Bethel) in Genesis 28 mean?";
            question.a = "The House of God";
            question.b = "Pillar";
            question.d = "Place of Peace";
            question.c = "Bethlehem";
            question.answer = "The House of God";
            question.timer = 10;
            questions_Text.Add(question);

            //Q6
            question = null;
            question = new Question();
            question.text = "Who was Cain’s first son?";
            question.a = "Seth";
            question.b = "Enoch";
            question.d = "Adah";
            question.c = "Zillah";
            question.answer = "Enoch";
            question.timer = 10;
            questions_Text.Add(question);

            //Q7
            question = null;
            question = new Question();
            question.text = "Which prophet saw God give life to a pile of dry bones?";
            question.a = "Jeremiah";
            question.b = "Elijah";
            question.d = "Micah";
            question.c = "Ezekiel";
            question.answer = "Ezekiel";
            question.timer = 10;
            questions_Text.Add(question);

            //Q8
            question = null;
            question = new Question();
            question.text = "Ravens brought food to which Old Testament prophet?";
            question.a = "Elijah";
            question.b = "Elisha";
            question.d = "Daniel";
            question.c = "Moses";
            question.answer = "Elijah";
            question.timer = 10;
            questions_Text.Add(question);

            //Q9
            question = null;
            question = new Question();
            question.text = "In Genesis 4 a musician is named. Who was he?";
            question.a = "Noah";
            question.b = "Joseph";
            question.d = "Jubal";
            question.c = "David";
            question.answer = "Jubal";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "From which catastrophe does Joseph save Egypt?";
            question.a = "Famine";
            question.b = "A plague";
            question.d = "Military invasion";
            question.c = "A flood";
            question.answer = "Famine";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "Why does the prophet Nathan rebuke David?";
            question.a = "David curses God inadvertently.";
            question.b = "David commits adultery with Bathsheba.";
            question.d = "His sons rape their stepsister.";
            question.c = "David fails to build the Temple to God.";
            question.answer = "David commits adultery with Bathsheba.";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "Which Jewish festival results from the events in Esther?";
            question.a = "Hanukkah";
            question.b = "Rosh Hashanah";
            question.d = "Passover";
            question.c = "Purim";
            question.answer = "Purim";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "Which of the following is not one of Israel’s judges?";
            question.a = "Ahab";
            question.b = " Deborah";
            question.d = "Jephthah";
            question.c = "Gideon";
            question.answer = "Ahab";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "Why does God reject Saul as king of Israel?";
            question.a = "Because Saul kills Samuel";
            question.b = "Saul did not destroy the Amalekites";
            question.d = "Saul refused to fight the Philistines";
            question.c = "Saul has too many concubines";
            question.answer = "Saul did not destroy the Amalekites";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "Why does Cain kill his brother Abel?";
            question.a = "The serpent tells him to";
            question.b = "Because Abel teases Cain";
            question.d = "God is pleased by Abel’s sacrifice.";
            question.c = "Because Adam loves Abel more";
            question.answer = "God is pleased by Abel’s sacrifice.";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "How many sons does Jacob have?";
            question.a = "Twelve";
            question.b = "Three";
            question.d = "Ten";
            question.c = "Four";
            question.answer = "Twelve";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "What do Moses and Joshua forbid the Israelites to do in the promised land?";
            question.a = "Conquer the cities of the region";
            question.b = "Divide the land amongst the 12 tribes";
            question.d = "Bathe in the Jordan River";
            question.c = "Intermarry with the native inhabitants";
            question.answer = "Intermarry with the native inhabitants";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "Who betrays Samson to the Philistines?";
            question.a = "Sarah";
            question.b = "Delilah";
            question.d = "Gideon";
            question.c = "Deborah";
            question.answer = "Delilah";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "What does David bring to Jerusalem to bless the religious city?";
            question.a = "The body of Moses";
            question.b = "Animals to be sacrificed to God";
            question.d = "The Ark of the Covenant";
            question.c = "The prophet Samuel";
            question.answer = "The Ark of the Covenant";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "What is one of the main criteria in Leviticus for living in the Israelite camp?";
            question.a = "Not to shave";
            question.b = "To be ceremonially clean";
            question.d = "To be a religious priest";
            question.c = "To remain sexually abstinent";
            question.answer = "To be ceremonially clean";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "What did the Israelite people ask Samuel for at Ramah?";
            question.a = "A god to worship";
            question.b = "Food";
            question.d = "Intermarriages";
            question.c = "A King";
            question.answer = "A King";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "Whose hair grew like feathers, and nails like bird claws? ";
            question.a = "Nebuchadnezzar";
            question.b = "Samson";
            question.d = "David";
            question.c = "Goliath";
            question.answer = "Nebuchadnezzar";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "What did Samson bet thirty men that they could not do? ";
            question.a = "Fight him";
            question.b = "Kill the Philistines";
            question.d = "Solve his riddles";
            question.c = "Pray for rain";
            question.answer = "Solve his riddles";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "What did God show to Moses just before he died? ";
            question.a = "The Red Sea";
            question.b = "The city of Jerusalem";
            question.d = "The Land of Egypt";
            question.c = "The Promised Land";
            question.answer = "The Promised Land";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "Who said, They have ascribed unto David ten thousands, and to me they have ascribed but a thousands.?" ;
            question.a = "Jonathan";
            question.b = "Saul";
            question.d = "Samuel";
            question.c = "Goliath";
            question.answer = "Saul";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "What prominent individual was the first to be cremated in the Bible?";
            question.a = "Pharaoh";
            question.b = "Samson";
            question.d = "Saul";
            question.c = "David";
            question.answer = "Saul";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "How was the presence of the Lord indicated at the door of the tabernacle during the time of Moses?";
            question.a = "A cloud";
            question.b = "Smoke";
            question.d = "Pillar of fire";
            question.c = "Wind";
            question.answer = "A cloud";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "Who cursed the day he was born and wished he could return to eternal darkness?";
            question.a = "King Solomon";
            question.b = "King David";
            question.d = "Nebuchadnezzar";
            question.c = "Job";
            question.answer = "Job";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "What two men of God were at different times in history assissted by a raven or ravens? ";
            question.a = "Elijah and Elishah";
            question.b = "Abraham and Job";
            question.d = "Noah and Elijah";
            question.c = "Samuel and Elijah";
            question.answer = "Noah and Elijah";
            question.timer = 10;
            questions_Text.Add(question);

            question = null;
            question = new Question();
            question.text = "At what did Ishmael become adapt while living in the desert?";
            question.a = "Hunting";
            question.b = "Archery";
            question.d = "Sword";
            question.c = "Fruits";
            question.answer = "Archery";
            question.timer = 10;
            questions_Text.Add(question);

            totalQuestions_Text = questions_Text.Count;
            txtCorrectQuestions.Text = "Correct: " + corrected_Text + "/" + totalQuestions_Text;


            timer_Text.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer_Text.Tick += timer_Tick_Songs;
            hasStarted_Text = false;
            //  questions_Text.Clear();
        }

        private void Start()
        {
            initialize_Questions();
            timer_Text.Stop();
            // totalQuestions = 0;
            seconds_Text = 0;

            if (!hasStarted_Text)
            {
                this.startQuestions();
            }
            else if (!isTextFinished)
            {
                timer_Text.Start();
                //this.resumeTimer();
            }
        }

        private void OnNavigatedTO(object sender, EventArgs e)
        {
            questions_Text.Clear();
        }

       

        private void chkScores_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SoreQuiz));
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
