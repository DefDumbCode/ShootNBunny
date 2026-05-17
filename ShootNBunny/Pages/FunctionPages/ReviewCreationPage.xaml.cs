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
    /// Логика взаимодействия для ReviewCreationPage.xaml
    /// </summary>
    public partial class ReviewCreationPage : Page
    {
        public Review review;
        public Book book_;
        public ReviewCreationPage(Book book)
        {
            InitializeComponent();
            this.book_ = book;
            ComplainObjectTB.Text = book_.Name;
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValid())
            {
                review = new Review()
                {
                    BookID = book_.ID,
                    UserID = MainWindow.user.ID,
                    Text = CommentTB.Text,
                    Rate = byte.Parse(RateTB.Text),
                    Date = DateTime.Now,
                    Frozen = false
                };

                Core.Context.Review.Add(review);
                Core.Context.SaveChanges();

                MessageBox.Show("Отзыв успешно отправлен");
                NavigationService.Navigate(new BookPage(book_));
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if(NavigationService.CanGoBack)
            {
                var conf = MessageBox.Show("При возвращении назад введеная Вами форма будет сброшена", "Подтвердите действие", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (conf == MessageBoxResult.OK)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void RateTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!String.IsNullOrEmpty(RateTB.Text) && !String.IsNullOrEmpty(CommentTB.Text))
            {
                SendBtn.IsEnabled = true;
            }
        }

        private bool CheckValid()
        {

            if (String.IsNullOrEmpty(RateTB.Text) || String.IsNullOrEmpty(CommentTB.Text))
            {
                MessageBox.Show("Введены пустые зачения!", "Неправильный ввод", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            int rate;
            if (!int.TryParse(RateTB.Text, out rate))
            {
                MessageBox.Show("Оценка должна быть целым числом!", "Неправильный ввод", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if(rate < 1 || rate > 10)
            {
                MessageBox.Show("Оценка должна быть между 1 и 10!", "Неправильный ввод", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
