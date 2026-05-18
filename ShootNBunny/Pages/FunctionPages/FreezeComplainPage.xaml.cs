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
    /// Логика взаимодействия для FreezeComplainPage.xaml
    /// </summary>
    public partial class FreezeComplainPage : Page
    {
        public string complaintObj;
        public int objID;
        public FreezeComplainPage(Review review)
        {
            InitializeComponent();
            complaintObj = "Отзыв";
            objID = review.ID;
        }

        public FreezeComplainPage(User user)
        {
            InitializeComponent();
            complaintObj = "Автор";
            objID = user.ID;
        }

        public FreezeComplainPage(Book book)
        {
            InitializeComponent();
            complaintObj = "Книга";
            objID = book.ID;
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            UnfreezeApplication complaint = new UnfreezeApplication()
            {
                UserID = MainWindow.user.ID,
                AuthorID = null,
                ReviewID = null,
                BookID = null,
                Comment = CommentTB.Text
            };

            switch (complaintObj)
            {
                case "Отзыв":
                    complaint.ReviewID = objID;
                    break;
                case "Автор":
                    complaint.AuthorID = objID;
                    break;
                case "Книга":
                    complaint.BookID = objID;
                    break;
            }

            Core.Context.UnfreezeApplication.Add(complaint);
            Core.Context.SaveChanges();

            MessageBox.Show("Жалоба успешно отправлена");
            NavigationService.GoBack();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                var conf = MessageBox.Show("При возвращении назад введеная Вами форма будет сброшена", "Подтвердите действие", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (conf == MessageBoxResult.OK)
                {
                    NavigationService.GoBack();
                }
            }
        }


        private void CommentTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            SendBtn.IsEnabled = true;

        }
    }
}
