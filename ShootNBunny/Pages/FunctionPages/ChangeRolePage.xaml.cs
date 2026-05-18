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
    /// Логика взаимодействия для ChangeRolePage.xaml
    /// </summary>
    public partial class ChangeRolePage : Page
    {
        public User user_ { get; set; }
        public static List<Roles> roles = Core.Context.Roles.ToList();
        public ChangeRolePage(User user)
        {
            InitializeComponent();
            this.user_ = user;
            NewRoleCB.ItemsSource = roles.Select(r => r.Name);
            NewRoleCB.SelectedItem = roles.FirstOrDefault(r => r.ID == user.RoleID).Name;
        }

        private void NewRoleCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            user_.RoleID = roles.First(r => r.Name == NewRoleCB.SelectedItem.ToString()).ID;
            Core.Context.SaveChanges();
            NavigationService.Navigate(new AdminPage());
        }
    }
}
