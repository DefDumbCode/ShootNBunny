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
            
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage(MainWindow.user));
        }

        private void RoleCheck()
        {
            switch (MainWindow.user.RoleID)
            {
                // Админ
                case 1:
                    RoleAct.Content = "Администрирование";
                    break;
                // Автор
                case 2:
                    RoleAct.Content = "Ваши книги";
                    break;
                // Читатель
                case 3:
                    RoleBtn.Visibility = Visibility.Hidden;
                    break;
            }
        }
        
    }
}
