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
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        public static List<Review> reviews = Core.Context.Review.Where(r => !r.Frozen).ToList();
        public static List<ReadingStatus> readingStatuses = Core.Context.ReadingStatus.ToList();
        public Book books {  get; set; }
        public BookPage(Book book)
        {
            InitializeComponent();
            this.books = book;
            DataContext = books;

            if(MainWindow.user.Roles.Name != "Администратор")
            {
                FreezeBookBtn.Visibility = Visibility.Hidden;
                FreezeReviewBtn.Visibility = Visibility.Hidden;
            }

            reviews = Core.Context.Review.Where(r => !r.Frozen).ToList();
            ReviewsLB.ItemsSource = reviews.Where(r => r.BookID == book.ID).ToList();

            ReadStatusCB.ItemsSource = readingStatuses.Select(rs => rs.Name).ToList();
            Reading bookuser = Core.Context.Reading.ToList().FirstOrDefault(r =>
                    r.BookID == books.ID && r.UserID == MainWindow.user.ID);
            if (bookuser != null)
            {
                ReadStatusCB.SelectedItem = bookuser.ReadingStatus.Name;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
                
        private void ReadBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReadingPage(books));
        }

        private void ReadStatusCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int statusID = readingStatuses.FirstOrDefault(rs => 
            rs.Name == ReadStatusCB.SelectedItem.ToString()).ID;

            Reading bookuser = Core.Context.Reading.ToList().FirstOrDefault(r =>
            r.BookID == books.ID && r.UserID == MainWindow.user.ID);

            if (bookuser != null)
            {
                bookuser.StatusID = statusID;
                Core.Context.SaveChanges();
            }
            else
            {
                Reading reading = new Reading()
                {
                    UserID = MainWindow.user.ID,
                    BookID = books.ID,
                    StatusID = statusID,
                };
                Core.Context.Reading.Add(reading);
                Core.Context.SaveChanges();
            }
        }

        private void FreezeBookBtn_Click(object sender, RoutedEventArgs e)
        {
            books.Frozen = true;
            Core.Context.SaveChanges();
        }

        private void FreezeReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            Review review = ReviewsLB.SelectedItem as Review;
            if (review != null)
            {
                review.Frozen = true;
                Core.Context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Выберите отзыв для заморозки");
            }
        }

        private void ComplianBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Complaint> complaints = Core.Context.Complaint.ToList();
            Button btn = sender as Button;
            Review review = btn.DataContext as Review;
            if (complaints.FirstOrDefault(c => c.UserID == MainWindow.user.ID && c.ReviewID == review.ID) == null)
            {
                NavigationService.Navigate(new ComplainPage(review));
            }
            else
            {
                MessageBox.Show("Жалоба уже отправлена на рассмотрение");
            }
        }

        private void ComplainAuthorBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Complaint> complaints = Core.Context.Complaint.ToList();
            User author = books.User;
            if (complaints.FirstOrDefault(c => c.UserID == MainWindow.user.ID && c.AuthorID == author.ID) == null)
            {
                NavigationService.Navigate(new ComplainPage(author));
            }
            else
            {
                MessageBox.Show("Жалоба уже отправлена на рассмотрение");
            }
        }

        private void ComplainBookBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Complaint> complaints = Core.Context.Complaint.ToList();
            if (complaints.FirstOrDefault(c => c.UserID == MainWindow.user.ID && c.BookID == books.ID) == null)
            {
                NavigationService.Navigate(new ComplainPage(books));
            }
            else
            {
                MessageBox.Show("Жалоба уже отправлена на рассмотрение");
            }
        }

        private void CreateReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.user.Frozen == true)
            {
                MessageBox.Show("Замороженные пользователи не могут писать книги и отзывы", "Внимание");
            }
            else
            {
                if (reviews.FirstOrDefault(r => r.UserID == MainWindow.user.ID && r.BookID == books.ID) == null)
                {
                    NavigationService.Navigate(new ReviewCreationPage(books));
                }
                else
                {
                    MessageBox.Show("Вы уже написали отзыв!");
                }
            }
            
        }
    }
}
