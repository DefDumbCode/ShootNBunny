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
    /// Логика взаимодействия для BookCreationPage.xaml
    /// </summary>
    public partial class BookCreationPage : Page
    {
        public static List<Genre> genres = Core.Context.Genre.ToList();
        public static List<Genre> selectedGenres = new List<Genre>();
        public BookCreationPage()
        {
            InitializeComponent();
            GenreList.ItemsSource = genres;
        }

        private void GenreBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            selectedGenres.Add(genres.FirstOrDefault(genr => genr.Name == checkBox.Content.ToString()));
        }

        private void GenreBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            selectedGenres.Remove(genres.FirstOrDefault(genr => genr.Name == checkBox.Content.ToString()));
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidAtributes())
            {
                Book book = new Book()
                {
                    Name = BookNameTB.Text,
                    Description = DescTB.Text,
                    CoverPath = CoverPathTB.Text,
                    Text = "Текст книги",
                    AuthorID = MainWindow.user.ID,
                    Frozen = false,
                    Rate = null
                };
                Core.Context.Book.Add(book);

                foreach(Genre genre in selectedGenres)
                {
                    BookGenre bookGenre = new BookGenre()
                    {
                        BookID = book.ID,
                        GenreID = genre.ID,
                    };
                    Core.Context.BookGenre.Add(bookGenre);
                }

                Core.Context.SaveChanges();
                NavigationService.Navigate(new RedactionPage(book));
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                var conf = MessageBox.Show("При возвращении назад ввденные данные будут сброшены", "Подтвердите действие", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (conf == MessageBoxResult.OK)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private bool IsValidAtributes()
        {
            if (String.IsNullOrEmpty(BookNameTB.Text) || String.IsNullOrEmpty(DescTB.Text) || String.IsNullOrEmpty(CoverPathTB.Text) || selectedGenres.Count == 0)
            {
                MessageBox.Show("Все поля обязательны для заполнения");
                return false;
            }

            if (Core.Context.Book.FirstOrDefault(b => b.Name == BookNameTB.Text) != null)
            {
                MessageBox.Show("Название книги не уникально");
                return false;
            }

            return true;
        }

    }
}
