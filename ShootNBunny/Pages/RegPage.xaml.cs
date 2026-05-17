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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void LoginTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(LoginTB.Text) && !String.IsNullOrEmpty(PasswordTB.Text)
               && !String.IsNullOrEmpty(Password2TB.Text) && !String.IsNullOrEmpty(NameTB.Text)
               && !String.IsNullOrEmpty(EmailTB.Text))
            {
                RegBtn.IsEnabled = true;
            }
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordTB.Text == Password2TB.Text)
            {
                if (PasswordTB.Text.Length >= 8)
                {
                    MainWindow.user = new User 
                        { Login = LoginTB.Text, 
                        Password = PasswordTB.Text, 
                        Name = NameTB.Text, 
                        Email = EmailTB.Text,
                        RoleID = 3,
                        Frozen = false};
                    Core.Context.User.Add(MainWindow.user);
                    Core.Context.SaveChanges();
                    MessageBox.Show("Регистрация прошла успешно");
                    if (NavigationService.CanGoBack)
                    {
                        NavigationService.Navigate(new MainPage());
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен иметь длину не менее 8 символов");
                }

            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }
        }
    }
}
