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
    /// Логика взаимодействия для ComplainPage.xaml
    /// </summary>
    public partial class ComplainPage : Page
    {
        public static List<Reason> reasons = Core.Context.Reason.ToList();
        public string complaintObj;
        public int objID;
        public ComplainPage(Review review)
        {
            InitializeComponent();
            ComplainReasonCB.ItemsSource = reasons.Select(r => r.Name).ToList();
            complaintObj = "Отзыв";
            objID = review.ID;
            ComplainObjectTB.Text += $"{complaintObj} пользователя {review.User.Name} от {review.Date}";
        }

        public ComplainPage(User user)
        {
            InitializeComponent();
            ComplainReasonCB.ItemsSource = reasons.Select(r => r.Name).ToList();
            complaintObj = "Автор";
            objID = user.ID;
            ComplainObjectTB.Text += $"{complaintObj} {user.Name}";
        }

        public ComplainPage(Book book)
        {
            InitializeComponent();
            ComplainReasonCB.ItemsSource = reasons.Select(r => r.Name).ToList();
            complaintObj = "Книга";
            objID = book.ID;
            ComplainObjectTB.Text += $"{complaintObj} {book.Name} автора {book.User.Name}";
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            Complaint complaint = new Complaint()
            {
                UserID = MainWindow.user.ID,
                AuthorID = null,
                ReviewID = null,
                BookID = null,
                ReasonID = reasons.FirstOrDefault(r => r.Name == ComplainReasonCB.SelectedItem as string).ID,
                Comment = CommentTB.Text
            };

            switch (complaintObj)
            {
                case "Отыв":
                    complaint.ReviewID = objID;
                    break;
                case "Автор":
                    complaint.AuthorID = objID;
                    break;
                case "Книга":
                    complaint.BookID = objID;
                    break;
            }

            Core.Context.Complaint.Add(complaint);
            Core.Context.SaveChanges();

            MessageBox.Show("Жалоба успешно отправлена");
            NavigationService.GoBack();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                var conf = MessageBox.Show("При возвращении назад введеная Вами форма будет сброшена", "Подтвердите действие", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if(conf == MessageBoxResult.OK)
                {
                    NavigationService.GoBack();
                }
            }
        }

        private void ComplainReasonCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SendBtn.IsEnabled = true;
        }
    }
}
