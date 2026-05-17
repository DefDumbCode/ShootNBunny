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
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        public static List<Book> books = Core.Context.Book.OrderBy(b => b.Name).ToList();
        public static List<string> genres = new List<string>() { "Все" };
        public static List<string> sort = new List<string>() {"Рейтингу", "Названию" };
        public CatalogPage()
        {
            InitializeComponent();
            BooksLB.ItemsSource = books;
            genres = new List<string>() { "Все" };
            foreach(Genre genre in Core.Context.Genre.ToList())
            {
                genres.Add(genre.Name);
            }
            GenresCB.ItemsSource = genres;
            GenresCB.SelectedIndex = 0;

            SortByCB.ItemsSource = sort;
            SortByCB.SelectedIndex = 0;
        }

        private void SortByCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(SortByCB.SelectedItem as string)
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

        private void BooksLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Book book = BooksLB.SelectedItem as Book;
            NavigationService.Navigate(new BookPage(book));
        }

        private void GenresCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterChanged();
        }

        private void SearchNameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterChanged();
        }

        private void FilterChanged()
        {
            List<Book> filtered = books;
            filtered = filtered.Where(b => b.Name.ToLower()
                .Contains(SearchNameTB.Text.ToLower())).ToList();
            filtered = filtered.Where(b => b.User.Name.ToLower()
                .Contains(SearchAuthorTB.Text.ToLower())).ToList();

            if (GenresCB.SelectedItem.ToString() != "Все")
            {
                filtered = filtered.Where(b => b.BookGenre.Select(bg => bg.Genre.Name)
                .Contains(GenresCB.SelectedItem as string)).ToList();
                
            }
            BooksLB.ItemsSource = filtered;
        }
    }
}
