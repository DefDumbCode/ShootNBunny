using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShootNBunny.Pages.FunctionPages
{
    /// <summary>
    /// Логика взаимодействия для BooksListPage.xaml
    /// </summary>
    public partial class BooksListPage : Page
    {
        public static List<Book> books = Core.Context.Book.Where(b => !b.Frozen).OrderBy(b => b.Name).ToList();
        public static List<Book> usersbooks;
        public static List<string> genres = new List<string>() { "Все" };
        public static List<string> sort = new List<string>() { "Рейтингу", "Названию" };
        public static List<string> readingStatuses = new List<string> { "Все" };
        public static List<ReadingStatus> readingStatusesSet = Core.Context.ReadingStatus.ToList();
        public static List<Reading> readings = Core.Context.Reading.ToList();
        public BooksListPage()
        {
            InitializeComponent();
            books = Core.Context.Book.Where(b => b.Frozen).OrderBy(b => b.Name).ToList();
            usersbooks = books.Where(b => b.Reading.FirstOrDefault(r => r.User == MainWindow.user) != null).ToList();
            BooksLB.ItemsSource = usersbooks;
            genres = new List<string>() { "Все" };
            foreach (Genre genre in Core.Context.Genre.ToList())
            {
                genres.Add(genre.Name);
            }


            readingStatuses = new List<string>() { "Все" };
            foreach (ReadingStatus readingStatus in Core.Context.ReadingStatus.ToList())
            {
                readingStatuses.Add(readingStatus.Name);
            }

            GenresCB.ItemsSource = genres;
            GenresCB.SelectedIndex = 0;

            ReadingStatusCB.ItemsSource = readingStatuses;
            ReadingStatusCB.SelectedIndex = 0;

            SortByCB.ItemsSource = sort;
            SortByCB.SelectedIndex = 0;

            ReadingCB.ItemsSource = readingStatusesSet.Select(rs => rs.Name);
        }

        private void ReadingStatusCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterChanged();
        }

        private void GenresCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterChanged();
        }

        private void SearchNameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterChanged();
        }

        private void SortByCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SortByCB.SelectedItem as string)
            {
                case "Названию":
                    usersbooks = usersbooks.OrderBy(b => b.Name).ToList();
                    BooksLB.ItemsSource = usersbooks;
                    break;
                case "Рейтингу":
                    usersbooks = usersbooks.OrderByDescending(b => b.Rate).ToList();
                    BooksLB.ItemsSource = usersbooks;
                    break;
                default:
                    usersbooks = usersbooks.OrderByDescending(b => b.Rate).ToList();
                    BooksLB.ItemsSource = usersbooks;
                    break;
            }
        }

        private void BooksLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Book book = BooksLB.SelectedItem as Book;
            NavigationService.Navigate(new BookPage(book));
        }

        private void FilterChanged()
        {
            List<Book> filtered = usersbooks;
            filtered = filtered.Where(b => b.Name.ToLower()
                .Contains(SearchNameTB.Text.ToLower())).ToList();
            filtered = filtered.Where(b => b.User.Name.ToLower()
                .Contains(SearchAuthorTB.Text.ToLower())).ToList();

            if (GenresCB.ItemsSource != null && ReadingStatusCB.ItemsSource != null)
            {
                if (GenresCB.SelectedItem.ToString() != "Все")
                {
                    filtered = filtered.Where(b => b.BookGenre.Select(bg => bg.Genre.Name)
                    .Contains(GenresCB.SelectedItem as string)).ToList();

                }

                if (ReadingStatusCB.SelectedItem.ToString() != "Все")
                {
                    filtered = filtered.Where(b => b.Reading.Where(r => r.UserID == MainWindow.user.ID).Select(bg => bg.ReadingStatus.Name)
                    .Contains(ReadingStatusCB.SelectedItem as string)).ToList();
                }
            }
            BooksLB.ItemsSource = filtered;
        }

        private void BooksLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReadingCB.SelectedItem = null;
            Reading reading = readings.FirstOrDefault(r => r.User == MainWindow.user && r.Book == BooksLB.SelectedItem);
            if (reading != null)
            {
                ReadingCB.SelectedItem = reading.ReadingStatus.Name;
            }
        }

        private void ReadingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reading reading = readings.FirstOrDefault(r => r.User == MainWindow.user && r.Book == BooksLB.SelectedItem);
            if (ReadingCB.SelectedItem != null)
            {
                if (reading != null)
                {
                    reading.StatusID = readingStatusesSet.First(rs => rs.Name == ReadingCB.SelectedItem.ToString()).ID;
                }
                else
                {
                    reading = new Reading()
                    {
                        StatusID = readingStatusesSet.First(rs => rs.Name == ReadingCB.SelectedItem.ToString()).ID,
                        UserID = MainWindow.user.ID,
                        BookID = (BooksLB.SelectedItem as Book).ID,
                    };
                    Core.Context.Reading.Add(reading);
                }
                Core.Context.SaveChanges();
                readings = Core.Context.Reading.ToList();
            }
        }
    }
}
