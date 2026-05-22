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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            FrozenStatusTB.Text = MainWindow.user.FrozenStatus;
            ContentFrame.Navigate(new CatalogPage());
            RoleCheck();   
        }
        private void BooksCatalogBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new CatalogPage());
        }

        private void BooklistBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate (new BooksListPage());
        }

        private void RoleBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (RoleCheck()) 
            {
                case "Администратор":;
                    ContentFrame.Navigate(new AdminPage());
                    break;
                case "Автор":
                    ContentFrame.Navigate(new AuthorPage());
                    break;
                case "Читатель":
                    break;
            }
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new ProfilePage(MainWindow.user));
        }

        private string RoleCheck()
        {
            switch (MainWindow.user.RoleID)
            {
                // Админ
                case 1:
                    return "Администратор";
                // Автор
                case 2:
                    return "Автор";
                // Читатель
                case 3:
                    RoleBtn.Visibility = Visibility.Hidden;
                    return "Читатель";
                default:
                    return "Читатель";
            }
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

        }
    }
}
