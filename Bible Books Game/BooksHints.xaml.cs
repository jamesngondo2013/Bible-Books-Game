using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
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
    public sealed partial class BooksHints : Page
    {

        public BooksHints()
        {
            this.InitializeComponent();

            List<BibleBooks> booksList = new List<BibleBooks>();
            BibleBooks newBook;

            // get the data from the xml file.
            XElement myFile = XElement.Load("BibleBooks.xml");

            // parses the data to the individual nodes
            IEnumerable<XElement> books = myFile.Elements();

            foreach (var oneBook in books)
            {
                newBook = new BibleBooks();
                // map the variables.


            
                    newBook.myBook = oneBook.Element("bookName").Value;
              

                booksList.Add(newBook);

                displayMsg.ItemsSource = booksList;
            
            }

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //List<BibleBooks> booksList = new List<BibleBooks>(); 
            //BibleBooks newBook;
            //// get the data from the xml file.
            //XElement myFile = XElement.Load("BibleBooks.xml");

            //// parses the data to the individual nodes
            //IEnumerable<XElement> books = myFile.Elements();

            //foreach (var oneBook in books)
            //{
            //    newBook = new BibleBooks();
            //    // map the variables.
               

            //    if (newBook.myBook == null)
            //    {
            //        newBook.myBook = oneBook.Element("bookName").Value;
            //    }

            //    booksList.Add(newBook);

            //    displayMsg.ItemsSource = booksList;
            //    base.OnNavigatedTo(e);
            //}


        }

        private void back_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
