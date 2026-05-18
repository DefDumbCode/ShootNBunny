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
        public FreezeComplainPage()
        {
            InitializeComponent();
        }

        private void CommentTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(CommentTB.Text))
            {
                SendBtn.IsEnabled = true;
            }
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            ComplaintFreeze application = new ComplaintFreeze()
            {
                UserID = MainWindow.user.ID,
                Description = CommentTB.Text
            };

            Core.Context.ComplaintFreeze.Add(application);
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
    }
}
