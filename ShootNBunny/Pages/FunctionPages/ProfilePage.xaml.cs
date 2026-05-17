using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public User user_ {  get; set; }
        public ProfilePage(User user)
        {
            InitializeComponent();
            this.user_ = user;
            DataContext = user_;
            if(user_.FrozenStatus == "")
            {
                FreezeComplaintBtn.Visibility = Visibility.Hidden;
            }

            if(user.Roles.Name != "Пользователь")
            {
                AuthorBtn.Visibility = Visibility.Hidden;
            }

        }

        private void AuthorBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
