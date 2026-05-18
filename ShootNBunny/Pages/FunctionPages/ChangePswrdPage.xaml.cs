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
    /// Логика взаимодействия для ChangePswrdPage.xaml
    /// </summary>
    public partial class ChangePswrdPage : Page
    {
        public User user_ { get; set; }
        public ChangePswrdPage(User user)
        {
            InitializeComponent();
            this.user_ = user;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewPsswrd.Text.Length >= 8)
            {
                user_.Password = NewPsswrd.Text;
                Core.Context.SaveChanges();
                NavigationService.Navigate(new AdminPage());

            }
            else
            {
                MessageBox.Show("Пароль должен быть не менее 8 символов");
            }
        }
    }
}
