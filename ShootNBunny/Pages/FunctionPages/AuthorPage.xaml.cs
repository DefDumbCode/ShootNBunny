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
    /// Логика взаимодействия для AuthorPage.xaml
    /// </summary>
    public partial class AuthorPage : Page
    {
        public static List<Book> books = Core.Context.Book.Where(b => b.AuthorID == MainWindow.user.ID).OrderBy(b => b.Name).ToList();
        public static List<string> sort = new List<string>() { "Рейтингу", "Названию" };
        public static List<string> freezestatus = new List<string>() { "Все", "Не заморожено", "Заморожено" };
        public AuthorPage()
        {
            InitializeComponent();
            books = Core.Context.Book.Where(b => b.AuthorID == MainWindow.user.ID).OrderBy(b => b.Name).ToList();
            BooksLB.ItemsSource = books;

            SortByCB.ItemsSource = sort;
            SortByCB.SelectedIndex = 0;

            FreezeStatusCB.ItemsSource = freezestatus;
            FreezeStatusCB.SelectedIndex = 0;
        }

        private void RedactBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Book book = button.DataContext as Book;
            NavigationService.Navigate(new RedactionPage(book));
        }

        private void BooksLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Book book = BooksLB.SelectedItem as Book;
            NavigationService.Navigate(new BookPage(book));
        }

        private void SortByCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SortByCB.SelectedItem as string)
            {
                case "Названию":
                    books = books.OrderBy(b => b.Name).ToList();
                    BooksLB.ItemsSource = books;
                    break;
                case "Рейтингу":
                    books = books.OrderByDescending(b => b.Rate).ToList();
                    BooksLB.ItemsSource = books;
                    break;
                default:
                    books = books.OrderByDescending(b => b.Rate).ToList();
                    BooksLB.ItemsSource = books;
                    break;
            }
        }

        private void SearchNameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterChanged();
        }

        private void FreezeStatusCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterChanged();
        }

        private void FreezeComplaintBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Book book = button.DataContext as Book;
            NavigationService.Navigate(new FreezeComplainPage());
        }

        private void CreateNewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.user.Frozen)
            {
                MessageBox.Show("Замороженные пользователи не могут писать книги и отзывы", "Внимание");
            }
            else
            {
                NavigationService.Navigate(new BookCreationPage());
            }
        }

        private void FilterChanged()
        {
            List<Book> filtered = books;
            filtered = filtered.Where(b => b.Name.ToLower()
                .Contains(SearchNameTB.Text.ToLower())).ToList();
            switch (FreezeStatusCB.SelectedItem)
            {
                case "Заморожено":
                    filtered = filtered.Where(b => b.Frozen).ToList();
                    break;
                case "Не заморожено":
                    filtered = filtered.Where(b => !b.Frozen).ToList();
                    break;
                default:
                    break;
            }

            BooksLB.ItemsSource = filtered;
        }
    }
}
