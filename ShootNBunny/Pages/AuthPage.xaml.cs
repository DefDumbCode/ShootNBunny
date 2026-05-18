using ShootNBunny.Pages.FunctionPages;
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

namespace ShootNBunny.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public static List<User> users = Core.Context.User.ToList();
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LoginTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(LoginTB.Text) && !String.IsNullOrEmpty(PasswordTB.Text))
            {
                AuthBtn.IsEnabled = true;
            }
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            User login_try = users.FirstOrDefault(a => a.Login == LoginTB.Text);
            if (login_try != null)
            {
                if (PasswordTB.Text == login_try.Password)
                {
                    MainWindow.user = login_try;
                    NavigationService.Navigate(new MainPage());
                }
                else
                {
                    MessageBox.Show("Неправильно введен логин или пароль!");
                    PasswordTB.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Неправильно введен логин или пароль!");
                PasswordTB.Text = "";
            }
        }

        private void ToRegBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}
